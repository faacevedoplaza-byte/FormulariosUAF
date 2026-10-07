using System.Text.RegularExpressions;
using Microsoft.Data.SqlClient;

namespace FormulariosUAF.Services;

/// <summary>
/// Consulta las operaciones RegCheq del esquema DB_REGCHEQ (base IntegracionesNetCar)
/// usando la cadena 'RegCheqConnection'. Solo lectura: únicamente hace SELECT.
/// Relaciones (no declaradas como FK en la base):
///   Operación → Asociados (IN_COD_OPERACION_*) → Ficha (IN_COD_ASOCIADO_*) → Listas (IN_COD_FICHA_*).
/// Algunas columnas se crearon con un espacio al final del nombre (ver SQL/RegCheq_QuitarEspaciosColumnas.sql);
/// SQL Server ignora ese espacio al comparar nombres, así que estas consultas funcionan en ambos casos.
/// </summary>
public class RegcheqService : IRegcheqService
{
    private const int CommandTimeoutSeconds = 30;

    private readonly string? _connectionString;
    private readonly ILogger<RegcheqService> _logger;

    public RegcheqService(IConfiguration config, ILogger<RegcheqService> logger)
    {
        _connectionString = config.GetConnectionString("RegCheqConnection");
        _logger = logger;
    }

    /// <summary>Nombres de tablas/columnas por tipo. Solo constantes: nunca se arma SQL con datos del usuario.</summary>
    private sealed record Esquema(
        string Operacion, string OperacionId,
        string Asociados, string AsociadoId,
        string Ficha, string FichaId,
        string Listas, string ListasFichaId,
        string NombreFicha,
        string ColumnasSoloNatural,
        string ActualizadoPor);

    private const string NombrePersona = "LTRIM(RTRIM(CONCAT(f.ST_NAME, ' ', f.ST_FATHERNAME, ' ', f.ST_MOTHERNAME)))";

    private static readonly Esquema Natural = new(
        "DB_REGCHEQ.T_OPERACION_NATURAL", "IN_COD_OPERACION_NATURAL",
        "DB_REGCHEQ.T_OPERACION_ASOCIADOS_NATURAL", "IN_COD_ASOCIADO_NATURAL",
        "DB_REGCHEQ.T_OPERACION_ASOCIADOS_FICHA_NATURAL", "IN_COD_FICHA_NATURAL",
        "DB_REGCHEQ.T_OPERACION_ASOCIADOS_FICHA_LISTAS_NATURAL", "IN_COD_FICHA_NATURAL",
        NombrePersona,
        "o.BT_FIRMADOPEP, o.DT_FIRMADOPEP, o.BT_FIRMADODOF, o.DT_FIRMADODOF, o.ST_FORMPEP, o.ST_FORMDOF",
        "CAST(NULL AS varchar(100)) AS ST_UPDATED_BY");

    private static readonly Esquema Empresa = new(
        "DB_REGCHEQ.T_OPERACION_EMPRESA", "IN_COD_OPERACION_EMPRESA",
        "DB_REGCHEQ.T_OPERACION_ASOCIADOS_EMPRESA", "IN_COD_ASOCIADO_EMPRESA",
        "DB_REGCHEQ.T_OPERACION_ASOCIADOS_FICHA_EMPRESA", "IN_COD_FICHA_EMPRESA",
        "DB_REGCHEQ.T_OPERACION_ASOCIADOS_FICHA_LISTAS_EMPRESA", "IN_COD_FICHA_EMPRESA",
        $"COALESCE(NULLIF(LTRIM(RTRIM(f.ST_SOCIALREASON)), ''), {NombrePersona})",
        "CAST(NULL AS bit) AS BT_FIRMADOPEP, CAST(NULL AS datetime) AS DT_FIRMADOPEP, " +
        "CAST(NULL AS bit) AS BT_FIRMADODOF, CAST(NULL AS datetime) AS DT_FIRMADODOF, " +
        "CAST(NULL AS varchar(300)) AS ST_FORMPEP, CAST(NULL AS varchar(300)) AS ST_FORMDOF",
        "o.ST_UPDATED_BY");

    private static Esquema Para(RegcheqTipo tipo) => tipo == RegcheqTipo.Empresa ? Empresa : Natural;

    // Columnas comunes del listado y del detalle
    private static string ColumnasResumen(Esquema e) => $@"
        o.{e.OperacionId} AS Id, o.IN_CODIGO, o.ST_REFERENCENUMBER, o.ST_STATUS, o.ST_TRANSACTIONTYPE,
        o.IN_TOTALAMOUNT, o.IN_TOTALEFECTIVE, o.ST_AMOUNTFORMATED, o.ST_DIVISA, o.BT_EFECTIVO,
        o.BT_HASPEP, o.BT_FORMSREQUIRED, o.BT_FORMSEND, o.BT_FORMSIGNED, o.BT_FINISHED,
        o.DT_CREATED_AT, {e.ColumnasSoloNatural}";

    public async Task<RegcheqListado> ListarAsync(RegcheqFiltro filtro, CancellationToken ct = default)
    {
        var resultado = new RegcheqListado();
        if (string.IsNullOrWhiteSpace(_connectionString))
        {
            _logger.LogWarning("RegCheqConnection no está configurada. Listado RegCheq omitido.");
            resultado.Configurado = false;
            return resultado;
        }

        var e = Para(filtro.Tipo);
        var esNatural = filtro.Tipo == RegcheqTipo.Natural;
        var where = new List<string>();
        var parametros = new List<SqlParameter>();

        if (!string.IsNullOrWhiteSpace(filtro.Texto))
        {
            // Las tablas solo tienen índice en su PK. Para no recorrer asociados/fichas por cada
            // operación, se detecta qué se busca y se arma el conjunto de IDs en una sola pasada
            // (o.Id IN (... UNION ...) → semi-join por hash).
            var texto = filtro.Texto.Trim();
            var busqueda = ClasificarBusqueda(texto);
            switch (busqueda)
            {
                case TipoBusqueda.Codigo:
                    where.Add($@"o.{e.OperacionId} IN (
                        SELECT x.{e.OperacionId} FROM {e.Operacion} x
                        WHERE x.IN_CODIGO = @codigo OR x.ST_REFERENCENUMBER = @textoExacto)");
                    parametros.Add(new SqlParameter("@codigo", int.Parse(texto.Replace(".", "").Replace(" ", ""))));
                    parametros.Add(new SqlParameter("@textoExacto", texto));
                    break;

                case TipoBusqueda.Rut:
                    // Con guion → RUT exacto. Sin guion es ambiguo (¿trae DV pegado o no?):
                    // se acepta el RUT exacto o el mismo número + un dígito verificador.
                    where.Add($@"o.{e.OperacionId} IN (
                        SELECT a.{e.OperacionId} FROM {e.Asociados} a
                        CROSS APPLY (SELECT REPLACE(REPLACE(REPLACE(UPPER(a.ST_DNI), '.', ''), '-', ''), ' ', '') AS Rut) n
                        WHERE n.Rut = @rut OR (@conGuion = 0 AND n.Rut LIKE @rut + '_')
                        UNION
                        SELECT x.{e.OperacionId} FROM {e.Operacion} x WHERE x.ST_REFERENCENUMBER = @textoExacto)");
                    parametros.Add(new SqlParameter("@rut", NormalizarRut(texto)));
                    parametros.Add(new SqlParameter("@conGuion", texto.Contains('-')));
                    parametros.Add(new SqlParameter("@textoExacto", texto));
                    break;

                default:
                    where.Add($@"o.{e.OperacionId} IN (
                        SELECT x.{e.OperacionId} FROM {e.Operacion} x
                        WHERE x.ST_REFERENCENUMBER LIKE @texto OR x.ST_ID = @textoExacto
                        UNION
                        SELECT a.{e.OperacionId} FROM {e.Asociados} a
                        JOIN {e.Ficha} f ON f.{e.AsociadoId} = a.{e.AsociadoId}
                        WHERE {e.NombreFicha} LIKE @texto)");
                    parametros.Add(new SqlParameter("@texto", $"%{EscaparLike(texto)}%"));
                    parametros.Add(new SqlParameter("@textoExacto", texto));
                    break;
            }
        }
        if (filtro.Desde.HasValue)
        {
            where.Add("o.DT_CREATED_AT >= @desde");
            parametros.Add(new SqlParameter("@desde", filtro.Desde.Value.Date));
        }
        if (filtro.Hasta.HasValue)
        {
            where.Add("o.DT_CREATED_AT < @hasta");
            parametros.Add(new SqlParameter("@hasta", filtro.Hasta.Value.Date.AddDays(1)));
        }
        AgregarBandera(where, parametros, "o.BT_HASPEP", "@pep", filtro.TienePep);
        AgregarBandera(where, parametros, "o.BT_FINISHED", "@fin", filtro.Finalizada);
        if (filtro.CoincidenciaListaPep.HasValue)
            where.Add($"o.{e.OperacionId} {(filtro.CoincidenciaListaPep.Value ? "IN" : "NOT IN")} (SELECT Id FROM @cp)");
        if (esNatural)
        {
            AgregarBandera(where, parametros, "o.BT_FIRMADOPEP", "@firmoPep", filtro.FirmoPep);
            AgregarBandera(where, parametros, "o.BT_FIRMADODOF", "@firmoDof", filtro.FirmoDof);
            if (filtro.Firmas.HasValue) where.Add(CondicionFirmas(filtro.Firmas.Value));
        }
        parametros.Add(new SqlParameter("@requerido", RegcheqFirmas.Requerido));
        parametros.Add(new SqlParameter("@inicioSeguimiento", RegcheqFirmas.InicioSeguimiento));

        // Requerimiento PEP/DOF del primer asociado (alias fa): para el indicador y el filtro de firmas.
        var firmasJoin = esNatural ? FirmasJoin(e) : "";

        // Orden: solo expresiones de esta lista (nunca texto del usuario en el SQL).
        var orden = (filtro.Orden ?? "").ToLowerInvariant();
        var dir = filtro.Ascendente ? "ASC" : "DESC";
        var expresionOrden = orden switch
        {
            "codigo" => "o.IN_CODIGO",
            "cliente" => "nm.Nombre",
            "monto" => "o.IN_TOTALAMOUNT",
            "efectivo" => "ISNULL(CAST(o.BT_EFECTIVO AS int), 0)",
            "pep" => "ISNULL(CAST(o.BT_HASPEP AS int), 0)",
            "listapep" => "CASE WHEN cp.Id IS NOT NULL THEN 1 ELSE 0 END",
            "firmapep" when esNatural => "ISNULL(CAST(o.BT_FIRMADOPEP AS int), 0)",
            "firmadof" when esNatural => "ISNULL(CAST(o.BT_FIRMADODOF AS int), 0)",
            "formularios" => "ISNULL(CAST(o.BT_FORMSIGNED AS int), 0)",
            "estado" => "o.ST_STATUS",
            "finalizada" => "ISNULL(CAST(o.BT_FINISHED AS int), 0)",
            // Pendientes primero (caso 1, 2, 3), luego sin seguimiento, no requiere y completas.
            "firmas" when esNatural => $@"CASE WHEN {CondicionFirmas(EstadoFirmas.Pep)} THEN 1 WHEN {CondicionFirmas(EstadoFirmas.Dof)} THEN 2
                WHEN {CondicionFirmas(EstadoFirmas.PepYDof)} THEN 3 WHEN {CondicionFirmas(EstadoFirmas.SinSeguimiento)} THEN 4
                WHEN {CondicionFirmas(EstadoFirmas.NoRequiere)} THEN 5 WHEN {CondicionFirmas(EstadoFirmas.Completa)} THEN 6 ELSE 7 END",
            _ => "o.DT_CREATED_AT"
        };
        // Desempate estable: más nuevas primero. (SQL Server no admite repetir una columna en el ORDER BY.)
        var orderBySql = expresionOrden == "o.DT_CREATED_AT"
            ? $"o.DT_CREATED_AT {dir}, o.{e.OperacionId} {dir}"
            : $"{expresionOrden} {dir}, o.DT_CREATED_AT DESC, o.{e.OperacionId} DESC";

        // Joins de la consulta de página: solo los que piden el filtro o el orden.
        var necesitaCp = filtro.CoincidenciaListaPep.HasValue || orden == "listapep";
        var joinsPagina = string.Join("\n",
            orden == "listapep" ? $"LEFT JOIN @cp cp ON cp.Id = o.{e.OperacionId}" : "",
            esNatural && (filtro.Firmas.HasValue || orden == "firmas") ? firmasJoin : "",
            orden == "cliente" ? NombreJoin(e) : "");

        var whereSql = where.Count > 0 ? "WHERE " + string.Join(" AND ", where) : "";
        var pagina = Math.Max(1, filtro.Pagina);
        var tamano = Math.Clamp(filtro.TamanoPagina, 1, 200);

        // Operaciones que coinciden en la lista pepChile: se calculan UNA vez y se guardan en @cp
        // (cruce asociados → fichas → listas). Como subconsulta, un plan con mala estimación podía
        // repetir ese cruce por cada operación.
        var prefijoCp = $@"
SET NOCOUNT ON;
DECLARE @cp TABLE (Id int PRIMARY KEY);
INSERT @cp (Id) SELECT DISTINCT x.Id FROM ({OperacionesConCoincidenciaPep(e)}) x (Id);
";

        var sqlTotales = prefijoCp + $@"
SELECT COUNT(*) AS Total,
       SUM(CASE WHEN o.BT_HASPEP = 1 THEN 1 ELSE 0 END) AS ConPep,
       {(esNatural ? "SUM(CASE WHEN o.BT_FIRMADOPEP = 1 THEN 1 ELSE 0 END)" : "0")} AS FirmoPep,
       {(esNatural ? "SUM(CASE WHEN o.BT_FIRMADODOF = 1 THEN 1 ELSE 0 END)" : "0")} AS FirmoDof,
       SUM(CASE WHEN o.BT_FORMSIGNED = 1 THEN 1 ELSE 0 END) AS FormulariosFirmados,
       SUM(CASE WHEN o.BT_FINISHED = 1 THEN 1 ELSE 0 END) AS Finalizadas,
       {(esNatural ? $"SUM(CASE WHEN {CondicionFirmas(EstadoFirmas.Completa)} THEN 1 ELSE 0 END)" : "0")} AS FirmasCompletas,
       SUM(CASE WHEN cp.Id IS NOT NULL THEN 1 ELSE 0 END) AS CoincidenciaListaPep
FROM {e.Operacion} o
LEFT JOIN @cp cp ON cp.Id = o.{e.OperacionId}
{firmasJoin}
{whereSql}
OPTION (RECOMPILE);";

        // Primero se elige la página (solo la tabla de operaciones); después se buscan asociados y
        // fichas únicamente para esas filas, en una pasada (sin índices, un APPLY por fila recorre
        // la tabla completa de asociados por cada operación).
        //
        // Los IDs de la página se guardan primero en @pagina. Si se usara una CTE, SQL Server la recalcula
        // en cada referencia, y con estadísticas desactualizadas (p. ej. BT_FIRMADODOF estimado en 1 fila)
        // elige bucles anidados que recorren la tabla de operaciones miles de veces.
        // OPTION (RECOMPILE) le permite ver que @pagina tiene ~25 filas y elegir un plan acorde.
        // Pos guarda el orden elegido para devolver la página en ese mismo orden.
        var sqlPagina = (necesitaCp ? prefijoCp : "SET NOCOUNT ON;") + $@"
DECLARE @pagina TABLE (Id int PRIMARY KEY, Pos int NOT NULL);

INSERT @pagina (Id, Pos)
SELECT o.{e.OperacionId}, ROW_NUMBER() OVER (ORDER BY {orderBySql})
FROM {e.Operacion} o
{joinsPagina}
{whereSql}
ORDER BY {orderBySql}
OFFSET @skip ROWS FETCH NEXT @take ROWS ONLY
OPTION (RECOMPILE);

WITH pagina AS (
    SELECT {ColumnasResumen(e)}, pg.Pos
    FROM {e.Operacion} o
    JOIN @pagina pg ON pg.Id = o.{e.OperacionId}
),
asociados AS (
    SELECT a.{e.OperacionId} AS OperacionId, a.{e.AsociadoId} AS AsociadoId, a.ST_DNI,
           {RequiereSql("a")},
           ROW_NUMBER() OVER (PARTITION BY a.{e.OperacionId} ORDER BY a.{e.AsociadoId}) AS Orden,
           COUNT(*) OVER (PARTITION BY a.{e.OperacionId}) AS Cantidad
    FROM {e.Asociados} a
    WHERE a.{e.OperacionId} IN (SELECT Id FROM @pagina)
),
fichas AS (
    SELECT f.{e.AsociadoId} AS AsociadoId, {e.NombreFicha} AS Nombre,
           ROW_NUMBER() OVER (PARTITION BY f.{e.AsociadoId} ORDER BY f.{e.FichaId} DESC) AS Orden
    FROM {e.Ficha} f
    WHERE f.{e.AsociadoId} IN (SELECT AsociadoId FROM asociados WHERE Orden = 1)
),
listapep AS (
    -- 1 = algún asociado coincide en pepChile, 0 = revisado sin coincidencia, NULL = sin registro pepChile
    SELECT a.{e.OperacionId} AS OperacionId, MAX(CAST(l.BT_COINCIDENCE AS int)) AS Coincide
    FROM {e.Asociados} a
    JOIN {e.Ficha} f ON f.{e.AsociadoId} = a.{e.AsociadoId}
    JOIN {e.Listas} l ON l.{e.ListasFichaId} = f.{e.FichaId}
    WHERE l.ST_LISTATIPO = '{ListaPep}' AND a.{e.OperacionId} IN (SELECT Id FROM @pagina)
    GROUP BY a.{e.OperacionId}
)
SELECT p.*, pa.ST_DNI AS AsociadoRut, fi.Nombre AS AsociadoNombre, ISNULL(pa.Cantidad, 0) AS CantidadAsociados,
       lp.Coincide AS CoincidenciaListaPep, pa.RequierePep, pa.RequiereDof
FROM pagina p
LEFT JOIN asociados pa ON pa.OperacionId = p.Id AND pa.Orden = 1
LEFT JOIN fichas fi ON fi.AsociadoId = pa.AsociadoId AND fi.Orden = 1
LEFT JOIN listapep lp ON lp.OperacionId = p.Id
ORDER BY p.Pos
OPTION (RECOMPILE);";

        try
        {
            await using var conn = new SqlConnection(_connectionString);
            await conn.OpenAsync(ct);

            await using (var cmd = Comando(conn, sqlTotales, parametros))
            await using (var r = await cmd.ExecuteReaderAsync(ct))
            {
                if (await r.ReadAsync(ct))
                {
                    var f = new Fila(r);
                    resultado.Totales = new RegcheqTotales
                    {
                        Total = f.Int("Total") ?? 0,
                        ConPep = f.Int("ConPep") ?? 0,
                        FirmoPep = f.Int("FirmoPep") ?? 0,
                        FirmoDof = f.Int("FirmoDof") ?? 0,
                        FormulariosFirmados = f.Int("FormulariosFirmados") ?? 0,
                        Finalizadas = f.Int("Finalizadas") ?? 0,
                        FirmasCompletas = f.Int("FirmasCompletas") ?? 0,
                        CoincidenciaListaPep = f.Int("CoincidenciaListaPep") ?? 0
                    };
                }
            }

            var paginacion = new List<SqlParameter>(parametros)
            {
                new("@skip", (pagina - 1) * tamano),
                new("@take", tamano)
            };
            await using (var cmd = Comando(conn, sqlPagina, paginacion))
            await using (var r = await cmd.ExecuteReaderAsync(ct))
            {
                while (await r.ReadAsync(ct))
                {
                    var f = new Fila(r);
                    var op = new RegcheqOperacionResumen();
                    LlenarResumen(op, f);
                    op.AsociadoRut = f.Str("AsociadoRut");
                    op.AsociadoNombre = f.Str("AsociadoNombre");
                    op.CantidadAsociados = f.Int("CantidadAsociados") ?? 0;
                    op.CoincidenciaListaPep = f.Bool("CoincidenciaListaPep");
                    if (esNatural)
                    {
                        op.RequierePep = f.Int("RequierePep") is { } rp ? rp == 1 : null;
                        op.RequiereDof = f.Int("RequiereDof") is { } rd ? rd == 1 : null;
                    }
                    resultado.Operaciones.Add(op);
                }
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error consultando operaciones RegCheq ({Tipo})", filtro.Tipo);
            resultado.Error = "No se pudo consultar RegCheq. Intente nuevamente o revise el log.";
        }

        return resultado;
    }

    public async Task<RegcheqOperacionDetalle?> ObtenerAsync(RegcheqTipo tipo, int id, CancellationToken ct = default)
    {
        if (string.IsNullOrWhiteSpace(_connectionString))
        {
            _logger.LogWarning("RegCheqConnection no está configurada. Detalle RegCheq omitido.");
            return null;
        }

        var e = Para(tipo);
        var sqlOperacion = $@"
SELECT {ColumnasResumen(e)},
       o.ST_ID, o.ST_COMPANYID, o.ST_CREATED_BY, {e.ActualizadoPor},
       CAST(o.ST_OPERATION AS varchar(max)) AS ST_OPERATION, o.BT_LEIDO, o.BT_REQUIREDFILE, o.ST_FILESTATUS,
       CAST(o.ST_SIGNATURES AS varchar(max)) AS ST_SIGNATURES, o.ST_ROS,
       CAST(o.ST_TRANSACTIONCOMMENTS AS varchar(max)) AS ST_TRANSACTIONCOMMENTS, o.ST_TRANSACTIONDATE,
       o.ST_VALORDOLAR, o.ST_VALORUF, o.DT_UPDATED_AT, o.DT_FECHARECIBIDO,
       CAST(o.ST_RESPUESTAJSON AS varchar(max)) AS ST_RESPUESTAJSON
FROM {e.Operacion} o
WHERE o.{e.OperacionId} = @id;";

        var sqlAsociados = $@"
SELECT a.* FROM {e.Asociados} a
WHERE a.{e.OperacionId} = @id
ORDER BY a.{e.AsociadoId};";

        var sqlFichas = $@"
SELECT f.* FROM {e.Ficha} f
WHERE f.{e.AsociadoId} IN (SELECT a.{e.AsociadoId} FROM {e.Asociados} a WHERE a.{e.OperacionId} = @id)
ORDER BY f.{e.FichaId} DESC;";

        var sqlListas = $@"
SELECT l.{e.ListasFichaId} AS FichaId, l.ST_LISTATIPO, l.BT_COINCIDENCE, l.BT_LISTRESULT, l.ST_RISK, l.IN_PEPLEVEL,
       CAST(l.ST_POSITION AS varchar(max)) AS ST_POSITION, CAST(l.ST_INFOGENERAL AS varchar(max)) AS ST_INFOGENERAL,
       CAST(l.ST_INFODATA AS varchar(max)) AS ST_INFODATA, CAST(l.ST_ADDITIONALDATA AS varchar(max)) AS ST_ADDITIONALDATA, l.DT_LASTCHECKED
FROM {e.Listas} l
WHERE l.{e.ListasFichaId} IN (SELECT f.{e.FichaId} FROM {e.Ficha} f
                              JOIN {e.Asociados} a ON a.{e.AsociadoId} = f.{e.AsociadoId}
                              WHERE a.{e.OperacionId} = @id)
ORDER BY l.ST_LISTATIPO;";

        try
        {
            await using var conn = new SqlConnection(_connectionString);
            await conn.OpenAsync(ct);
            var pId = () => new List<SqlParameter> { new("@id", id) };

            RegcheqOperacionDetalle op;
            await using (var cmd = Comando(conn, sqlOperacion, pId()))
            await using (var r = await cmd.ExecuteReaderAsync(ct))
            {
                if (!await r.ReadAsync(ct)) return null;
                var f = new Fila(r);
                op = new RegcheqOperacionDetalle { Tipo = tipo };
                LlenarResumen(op, f);
                op.RegcheqId = f.Str("ST_ID");
                op.CompanyId = f.Str("ST_COMPANYID");
                op.CreadoPor = f.Str("ST_CREATED_BY");
                op.ActualizadoPor = f.Str("ST_UPDATED_BY");
                op.Operacion = f.Str("ST_OPERATION");
                op.Leido = f.Bool("BT_LEIDO");
                op.ArchivoRequerido = f.Bool("BT_REQUIREDFILE");
                op.EstadoArchivo = f.Str("ST_FILESTATUS");
                op.Firmas = f.Str("ST_SIGNATURES");
                op.Ros = f.Str("ST_ROS");
                op.ComentariosTransaccion = f.Str("ST_TRANSACTIONCOMMENTS");
                op.FechaTransaccion = f.Str("ST_TRANSACTIONDATE");
                op.ValorDolar = f.Str("ST_VALORDOLAR");
                op.ValorUf = f.Str("ST_VALORUF");
                op.FormularioPep = f.Str("ST_FORMPEP");
                op.FormularioDof = f.Str("ST_FORMDOF");
                op.FechaActualizacion = f.Fecha("DT_UPDATED_AT");
                op.FechaRecibido = f.Fecha("DT_FECHARECIBIDO");
                op.RespuestaJson = f.Str("ST_RESPUESTAJSON");
            }

            await using (var cmd = Comando(conn, sqlAsociados, pId()))
            await using (var r = await cmd.ExecuteReaderAsync(ct))
            {
                while (await r.ReadAsync(ct))
                {
                    var f = new Fila(r);
                    op.Asociados.Add(new RegcheqAsociado
                    {
                        Id = f.Int(e.AsociadoId) ?? 0,
                        Rut = f.Str("ST_DNI"),
                        Tipo = f.Str("ST_TYPE"),
                        Monto = f.Int("IN_MONTO"),
                        MontoFormateado = f.Str("ST_MONTOFORMAT"),
                        Moneda = f.Str("ST_CURRENCY"),
                        Efectivo = f.Bool("BT_EFECTIVO"),
                        MontoEfectivo = f.Int("IN_EFECTIVEAMOUNT"),
                        FichaId = f.Str("ST_FICHAID"),
                        Pep = f.Bool("BT_PEP"),
                        NivelPep = f.Int("IN_PEPLEVEL"),
                        RiesgoCalculado = f.Str("ST_CALCULATEDRISK"),
                        EstadoFicha = f.Str("ST_ASSOCIATEFILESTATUS"),
                        EstadoFormularioPep = f.Str("ST_PEPDEFAULTFORMSTATUS"),
                        EstadoFormularioDof = f.Str("ST_DOFFORMSTATUS"),
                        EstadoFormularioBf = f.Str("ST_BFFORMSTATUS"),
                        Roe = f.Bool("BT_ROE"),
                        FechaRecibido = f.Fecha("DT_FECHARECIBIDO")
                    });
                }
            }

            var fichasPorId = new Dictionary<int, RegcheqFicha>();
            await using (var cmd = Comando(conn, sqlFichas, pId()))
            await using (var r = await cmd.ExecuteReaderAsync(ct))
            {
                while (await r.ReadAsync(ct))
                {
                    var f = new Fila(r);
                    var asociadoId = f.Int(e.AsociadoId);
                    // Si un asociado tuviera más de una ficha, se usa la más reciente (vienen ordenadas DESC).
                    var asociado = op.Asociados.FirstOrDefault(a => a.Id == asociadoId);
                    if (asociado is null || asociado.Ficha is not null) continue;

                    var ficha = new RegcheqFicha
                    {
                        Id = f.Int(e.FichaId) ?? 0,
                        Nombre = f.Str("ST_NAME"),
                        ApellidoPaterno = f.Str("ST_FATHERNAME"),
                        ApellidoMaterno = f.Str("ST_MOTHERNAME"),
                        RazonSocial = f.Str("ST_SOCIALREASON"),
                        NombreFantasia = f.Str("ST_FANTASYNAME"),
                        Dni = f.Str("ST_DNI"),
                        Rut = f.Str("ST_RUT"),
                        PaisDni = f.Str("ST_DNICOUNTRY"),
                        TipoPersona = f.Str("ST_PERSONTYPE"),
                        Email = f.Str("ST_EMAIL"),
                        Telefono = f.Str("ST_PHONE"),
                        Nacionalidad = f.Str("ST_NATIONALITY"),
                        Pais = f.Str("ST_COUNTRY"),
                        Region = f.Str("ST_REGION"),
                        Ciudad = f.Str("ST_CITY"),
                        Direccion = f.Str("ST_ADDRESS"),
                        Cargo = f.Str("ST_POSITION"),
                        Empleador = f.Str("ST_EMPLOYER"),
                        Ingresos = f.Str("ST_INCOME"),
                        EstadoCivil = f.Str("ST_MARITALSTATUS"),
                        Genero = f.Str("ST_GENDER"),
                        FechaNacimiento = f.Fecha("DT_BIRTHDATE"),
                        Estado = f.Str("ST_STATUS"),
                        Riesgo = f.Str("ST_RISK"),
                        RiesgoCalculado = f.Str("ST_CALCULATEDRISK"),
                        RiesgoEfectivo = f.Str("ST_EFFECTIVERISK"),
                        RiesgoSobrescrito = f.Str("ST_OVERWRITTENRISK"),
                        NivelPep = f.Int("IN_PEPLEVEL"),
                        UltimaFechaDof = f.Str("ST_LASTDOFDATE"),
                        UltimaFechaPep = f.Str("ST_LASTPEPDATE"),
                        Comentarios = f.Str("ST_COMMENTS"),
                        FechaCreacion = f.Fecha("DT_CREATED_AT"),
                        FechaActualizacion = f.Fecha("DT_UPDATED_AT")
                    };
                    asociado.Ficha = ficha;
                    fichasPorId[ficha.Id] = ficha;
                }
            }

            if (fichasPorId.Count > 0)
            {
                await using var cmd = Comando(conn, sqlListas, pId());
                await using var r = await cmd.ExecuteReaderAsync(ct);
                while (await r.ReadAsync(ct))
                {
                    var f = new Fila(r);
                    if (!fichasPorId.TryGetValue(f.Int("FichaId") ?? 0, out var ficha)) continue;
                    ficha.Listas.Add(new RegcheqLista
                    {
                        Tipo = f.Str("ST_LISTATIPO"),
                        Coincidencia = f.Bool("BT_COINCIDENCE"),
                        Resultado = f.Bool("BT_LISTRESULT"),
                        Riesgo = f.Str("ST_RISK"),
                        NivelPep = f.Int("IN_PEPLEVEL"),
                        Cargo = f.Str("ST_POSITION"),
                        InfoGeneral = f.Str("ST_INFOGENERAL"),
                        InfoData = f.Str("ST_INFODATA"),
                        DatosAdicionales = f.Str("ST_ADDITIONALDATA"),
                        UltimaRevision = f.Fecha("DT_LASTCHECKED")
                    });
                }
            }

            // Mismo criterio que el listado: requerimiento PEP/DOF del primer asociado (solo naturales).
            if (tipo == RegcheqTipo.Natural && op.Asociados.FirstOrDefault() is { } primero)
            {
                static bool Requiere(string? estado) =>
                    string.Equals(estado?.Trim(), RegcheqFirmas.Requerido, StringComparison.OrdinalIgnoreCase);
                op.RequierePep = Requiere(primero.EstadoFormularioPep);
                op.RequiereDof = Requiere(primero.EstadoFormularioDof);
            }

            var listasPep = op.Asociados
                .SelectMany(a => a.Ficha?.Listas ?? [])
                .Where(l => string.Equals(l.Tipo, ListaPep, StringComparison.OrdinalIgnoreCase))
                .ToList();
            op.CoincidenciaListaPep = listasPep.Count == 0 ? null : listasPep.Any(l => l.Coincidencia == true);

            return op;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error consultando detalle RegCheq {Tipo} {Id}", tipo, id);
            throw;
        }
    }

    public async Task<List<RegcheqPendienteGestion>?> ListarPendientesGestionAsync(DateTime desde, CancellationToken ct = default)
    {
        if (string.IsNullOrWhiteSpace(_connectionString))
        {
            _logger.LogWarning("RegCheqConnection no está configurada. Gestión RegCheq omitida.");
            return null;
        }

        // Solo personas naturales: las operaciones de empresa no tienen columnas de firma PEP/DOF.
        // "Requiere" viene del asociado (estado con que RegCheq creó la operación) y "firmado" de la
        // operación (BT_FIRMADO*, que actualiza el job TraerEstadoFirmasPep de integracionesNetcar).
        var e = Natural;
        var sql = $@"
SET NOCOUNT ON;
DECLARE @ops TABLE (Id int PRIMARY KEY);
INSERT @ops (Id) SELECT o.{e.OperacionId} FROM {e.Operacion} o WHERE o.DT_CREATED_AT >= @desde OPTION (RECOMPILE);

WITH asociados AS (
    SELECT a.{e.OperacionId} AS OperacionId, a.{e.AsociadoId} AS AsociadoId, a.ST_DNI,
           {RequiereSql("a")},
           ROW_NUMBER() OVER (PARTITION BY a.{e.OperacionId} ORDER BY a.{e.AsociadoId}) AS Orden
    FROM {e.Asociados} a
    WHERE a.{e.OperacionId} IN (SELECT Id FROM @ops)
),
pendientes AS (
    SELECT o.{e.OperacionId} AS Id, o.IN_CODIGO, o.ST_REFERENCENUMBER, o.DT_CREATED_AT,
           ISNULL(o.BT_FIRMADOPEP, 0) AS FirmoPep, ISNULL(o.BT_FIRMADODOF, 0) AS FirmoDof,
           a.AsociadoId, a.ST_DNI, a.RequierePep, a.RequiereDof
    FROM {e.Operacion} o
    JOIN @ops x ON x.Id = o.{e.OperacionId}
    JOIN asociados a ON a.OperacionId = o.{e.OperacionId} AND a.Orden = 1
    WHERE (a.RequierePep = 1 AND ISNULL(o.BT_FIRMADOPEP, 0) = 0)
       OR (a.RequiereDof = 1 AND ISNULL(o.BT_FIRMADODOF, 0) = 0)
),
fichas AS (
    SELECT f.{e.AsociadoId} AS AsociadoId, {e.NombreFicha} AS Nombre,
           ROW_NUMBER() OVER (PARTITION BY f.{e.AsociadoId} ORDER BY f.{e.FichaId} DESC) AS Orden
    FROM {e.Ficha} f
    WHERE f.{e.AsociadoId} IN (SELECT AsociadoId FROM pendientes)
)
SELECT p.*, fi.Nombre
FROM pendientes p
LEFT JOIN fichas fi ON fi.AsociadoId = p.AsociadoId AND fi.Orden = 1
ORDER BY p.DT_CREATED_AT DESC, p.Id DESC
OPTION (RECOMPILE);";

        try
        {
            var lista = new List<RegcheqPendienteGestion>();
            await using var conn = new SqlConnection(_connectionString);
            await conn.OpenAsync(ct);
            await using var cmd = Comando(conn, sql, [new SqlParameter("@desde", desde.Date), new SqlParameter("@requerido", RegcheqFirmas.Requerido)]);
            await using var r = await cmd.ExecuteReaderAsync(ct);
            while (await r.ReadAsync(ct))
            {
                var f = new Fila(r);
                lista.Add(new RegcheqPendienteGestion
                {
                    Id = f.Int("Id") ?? 0,
                    Codigo = f.Int("IN_CODIGO"),
                    Referencia = f.Str("ST_REFERENCENUMBER"),
                    FechaCreacion = f.Fecha("DT_CREATED_AT"),
                    Rut = f.Str("ST_DNI"),
                    Nombre = f.Str("Nombre"),
                    RequierePep = f.Int("RequierePep") == 1,
                    RequiereDof = f.Int("RequiereDof") == 1,
                    FirmoPep = f.Bool("FirmoPep") == true,
                    FirmoDof = f.Bool("FirmoDof") == true
                });
            }
            return lista;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error consultando operaciones RegCheq pendientes de gestión");
            throw;
        }
    }

    public async Task<Dictionary<int, RegcheqCotizacion>> ObtenerCotizacionesAsync(RegcheqTipo tipo, IReadOnlyCollection<int> operacionIds, CancellationToken ct = default)
    {
        var resultado = new Dictionary<int, RegcheqCotizacion>();
        if (string.IsNullOrWhiteSpace(_connectionString) || operacionIds.Count == 0) return resultado;

        var e = Para(tipo);
        var clientes = tipo == RegcheqTipo.Empresa ? "DB_REGCHEQ.T_OUT_CLIENTES_EMPRESAS" : "DB_REGCHEQ.T_OUT_CLIENTES_NATURALES";
        // Los IDs son enteros propios (no texto del usuario): se escriben en la consulta.
        var ids = string.Join(",", operacionIds.Distinct());

        // TRY_CONVERT: una referencia no numérica no corta la consulta (NEGOCIO es int).
        var sql = $@"
SELECT o.{e.OperacionId} AS Id, TRY_CONVERT(int, o.ST_REFERENCENUMBER) AS Cotizacion, c.EMPRESA
FROM {e.Operacion} o
OUTER APPLY (SELECT TOP 1 a.ST_DNI FROM {e.Asociados} a
             WHERE a.{e.OperacionId} = o.{e.OperacionId} ORDER BY a.{e.AsociadoId}) a
OUTER APPLY (SELECT TOP 1 c.EMPRESA FROM {clientes} c
             WHERE c.NEGOCIO = TRY_CONVERT(int, o.ST_REFERENCENUMBER) AND c.RUT = a.ST_DNI) c
WHERE o.{e.OperacionId} IN ({ids});";

        try
        {
            await using var conn = new SqlConnection(_connectionString);
            await conn.OpenAsync(ct);
            await using var cmd = Comando(conn, sql, []);
            await using var r = await cmd.ExecuteReaderAsync(ct);
            while (await r.ReadAsync(ct))
            {
                var f = new Fila(r);
                var id = f.Int("Id") ?? 0;
                resultado[id] = new RegcheqCotizacion(id, f.Int("Cotizacion"), f.Str("EMPRESA"));
            }
        }
        catch (Exception ex)
        {
            // El vehículo es información complementaria: si falla, la vista sigue sin él.
            _logger.LogError(ex, "Error obteniendo cotizaciones RegCheq ({Tipo})", tipo);
        }
        return resultado;
    }

    private static void LlenarResumen(RegcheqOperacionResumen op, Fila f)
    {
        op.Id = f.Int("Id") ?? 0;
        op.Codigo = f.Int("IN_CODIGO");
        op.Referencia = f.Str("ST_REFERENCENUMBER");
        op.Estado = f.Str("ST_STATUS");
        op.TipoTransaccion = f.Str("ST_TRANSACTIONTYPE");
        op.MontoTotal = f.Int("IN_TOTALAMOUNT");
        op.MontoEfectivo = f.Int("IN_TOTALEFECTIVE");
        op.MontoFormateado = f.Str("ST_AMOUNTFORMATED");
        op.Divisa = f.Str("ST_DIVISA");
        op.Efectivo = f.Bool("BT_EFECTIVO");
        op.TienePep = f.Bool("BT_HASPEP");
        op.FormulariosRequeridos = f.Bool("BT_FORMSREQUIRED");
        op.FormulariosEnviados = f.Bool("BT_FORMSEND");
        op.FormulariosFirmados = f.Bool("BT_FORMSIGNED");
        op.Finalizada = f.Bool("BT_FINISHED");
        op.FirmoPep = f.Bool("BT_FIRMADOPEP");
        op.FechaFirmaPep = f.Fecha("DT_FIRMADOPEP");
        op.FirmoDof = f.Bool("BT_FIRMADODOF");
        op.FechaFirmaDof = f.Fecha("DT_FIRMADODOF");
        op.FechaCreacion = f.Fecha("DT_CREATED_AT");
    }

    private static void AgregarBandera(List<string> where, List<SqlParameter> parametros, string columna, string nombre, bool? valor)
    {
        if (!valor.HasValue) return;
        where.Add($"ISNULL({columna}, 0) = {nombre}");
        parametros.Add(new SqlParameter(nombre, valor.Value));
    }

    private static SqlCommand Comando(SqlConnection conn, string sql, IEnumerable<SqlParameter> parametros)
    {
        var cmd = new SqlCommand(sql, conn) { CommandTimeout = CommandTimeoutSeconds };
        // Un SqlParameter no puede pertenecer a dos comandos: se clonan.
        foreach (var p in parametros) cmd.Parameters.Add(new SqlParameter(p.ParameterName, p.Value));
        return cmd;
    }

    /// <summary>Tipo de lista PEP de Chile, tal como lo guarda la integración RegCheq (RegcheqController).</summary>
    private const string ListaPep = "pepChile";

    /// <summary>IDs de operaciones con al menos un asociado que coincide en la lista pepChile (BT_COINCIDENCE = 1).</summary>
    private static string OperacionesConCoincidenciaPep(Esquema e) => $@"
        SELECT a.{e.OperacionId} FROM {e.Asociados} a
        JOIN {e.Ficha} f ON f.{e.AsociadoId} = a.{e.AsociadoId}
        JOIN {e.Listas} l ON l.{e.ListasFichaId} = f.{e.FichaId}
        WHERE l.ST_LISTATIPO = '{ListaPep}' AND l.BT_COINCIDENCE = 1
          AND a.{e.OperacionId} IS NOT NULL";

    /// <summary>Columnas RequierePep / RequiereDof (1/0) de un asociado. Usa el parámetro @requerido.</summary>
    private static string RequiereSql(string alias) => $@"
           CASE WHEN UPPER(LTRIM(RTRIM(CAST({alias}.ST_PEPDEFAULTFORMSTATUS AS varchar(50))))) = @requerido THEN 1 ELSE 0 END AS RequierePep,
           CASE WHEN UPPER(LTRIM(RTRIM(CAST({alias}.ST_DOFFORMSTATUS AS varchar(50))))) = @requerido THEN 1 ELSE 0 END AS RequiereDof";

    /// <summary>LEFT JOIN alias fa: requerimiento PEP/DOF del primer asociado de cada operación.</summary>
    private static string FirmasJoin(Esquema e) => $@"
LEFT JOIN (
    SELECT x.OpId, x.RequierePep, x.RequiereDof
    FROM (SELECT a.{e.OperacionId} AS OpId, {RequiereSql("a")},
                 ROW_NUMBER() OVER (PARTITION BY a.{e.OperacionId} ORDER BY a.{e.AsociadoId}) AS Orden
          FROM {e.Asociados} a) x
    WHERE x.Orden = 1
) fa ON fa.OpId = o.{e.OperacionId}";

    /// <summary>LEFT JOIN alias nm: nombre (ficha más reciente del primer asociado) para ordenar por cliente.</summary>
    private static string NombreJoin(Esquema e) => $@"
LEFT JOIN (
    SELECT a.OpId, f.Nombre
    FROM (SELECT a.{e.OperacionId} AS OpId, a.{e.AsociadoId} AS AsociadoId,
                 ROW_NUMBER() OVER (PARTITION BY a.{e.OperacionId} ORDER BY a.{e.AsociadoId}) AS Orden
          FROM {e.Asociados} a) a
    JOIN (SELECT f.{e.AsociadoId} AS AsociadoId, {e.NombreFicha} AS Nombre,
                 ROW_NUMBER() OVER (PARTITION BY f.{e.AsociadoId} ORDER BY f.{e.FichaId} DESC) AS Orden
          FROM {e.Ficha} f) f ON f.AsociadoId = a.AsociadoId AND f.Orden = 1
    WHERE a.Orden = 1
) nm ON nm.OpId = o.{e.OperacionId}";

    /// <summary>
    /// Condición SQL equivalente a <see cref="RegcheqFirmas.Estado"/> (requiere el join fa y @inicioSeguimiento).
    /// Los casos se definen por lo que se REQUIERE: caso 3 = requiere ambos y falta al menos uno.
    /// </summary>
    private static string CondicionFirmas(EstadoFirmas estado)
    {
        const string rp = "ISNULL(fa.RequierePep, 0) = 1", nrp = "ISNULL(fa.RequierePep, 0) = 0";
        const string rd = "ISNULL(fa.RequiereDof, 0) = 1", nrd = "ISNULL(fa.RequiereDof, 0) = 0";
        const string fp = "ISNULL(o.BT_FIRMADOPEP, 0) = 1", nfp = "ISNULL(o.BT_FIRMADOPEP, 0) = 0";
        const string fd = "ISNULL(o.BT_FIRMADODOF, 0) = 1", nfd = "ISNULL(o.BT_FIRMADODOF, 0) = 0";
        const string seg = "o.DT_CREATED_AT >= @inicioSeguimiento";
        return estado switch
        {
            EstadoFirmas.Completa => $"(fa.OpId IS NOT NULL AND ({rp} OR {rd}) AND ({nrp} OR {fp}) AND ({nrd} OR {fd}))",
            EstadoFirmas.Pep => $"({rp} AND {nrd} AND {nfp} AND {seg})",
            EstadoFirmas.Dof => $"({rd} AND {nrp} AND {nfd} AND {seg})",
            EstadoFirmas.PepYDof => $"({rp} AND {rd} AND ({nfp} OR {nfd}) AND {seg})",
            EstadoFirmas.NoRequiere => $"(fa.OpId IS NOT NULL AND {nrp} AND {nrd})",
            EstadoFirmas.SinSeguimiento => $"(NOT ({seg}) AND (({rp} AND {nfp}) OR ({rd} AND {nfd})))",
            _ => "1 = 1"
        };
    }

    private enum TipoBusqueda { Codigo, Rut, Texto }

    /// <summary>
    /// Número de hasta 6 dígitos → código/referencia. 7-9 dígitos (con o sin puntos, guion y DV) → RUT.
    /// Cualquier otra cosa → nombre, N° de referencia o ID RegCheq.
    /// </summary>
    private static TipoBusqueda ClasificarBusqueda(string texto)
    {
        var limpio = texto.Replace(".", "").Replace(" ", "");
        if (Regex.IsMatch(limpio, @"^\d{1,6}$")) return TipoBusqueda.Codigo;
        if (Regex.IsMatch(limpio, @"^\d{7,8}-[0-9kK]$") || Regex.IsMatch(limpio, @"^\d{7,9}[kK]?$")) return TipoBusqueda.Rut;
        return TipoBusqueda.Texto;
    }

    private static string NormalizarRut(string texto) =>
        Regex.Replace(texto.ToUpperInvariant(), "[^0-9K]", "");

    private static string EscaparLike(string s) =>
        s.Replace("[", "[[]").Replace("%", "[%]").Replace("_", "[_]");

    /// <summary>
    /// Lectura tolerante por nombre de columna: ignora espacios al final del nombre
    /// y devuelve null si la columna no existe en esa tabla (natural vs empresa).
    /// </summary>
    private sealed class Fila
    {
        private readonly SqlDataReader _r;
        private readonly Dictionary<string, int> _ordinales = new(StringComparer.OrdinalIgnoreCase);

        public Fila(SqlDataReader r)
        {
            _r = r;
            for (var i = 0; i < r.FieldCount; i++)
                _ordinales.TryAdd(r.GetName(i).Trim(), i);
        }

        private object? Valor(string columna) =>
            _ordinales.TryGetValue(columna.Trim('[', ']', ' '), out var i) && !_r.IsDBNull(i) ? _r.GetValue(i) : null;

        public string? Str(string columna)
        {
            var s = Valor(columna)?.ToString()?.Trim();
            return string.IsNullOrEmpty(s) ? null : s;
        }

        public int? Int(string columna) => Valor(columna) is { } v ? Convert.ToInt32(v) : null;
        public bool? Bool(string columna) => Valor(columna) is { } v ? Convert.ToBoolean(v) : null;
        public DateTime? Fecha(string columna) => Valor(columna) is DateTime d ? d : null;
    }
}
