using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Caching.Memory;

namespace FormulariosUAF.Services;

/// <summary>Vehículo de la cotización Netcar asociada a una operación RegCheq.</summary>
public class VehiculoCotizacion
{
    public int Cotizacion { get; set; }
    /// <summary>"Siglo 21" o "VyD": base Netcar de donde salió.</summary>
    public string Empresa { get; set; } = "";
    public string? Marca { get; set; }
    public string? Modelo { get; set; }
    public string? Version { get; set; }
    public int? Anio { get; set; }
    public string? Color { get; set; }
    public string? Patente { get; set; }
    public string? Chasis { get; set; }
    public bool? EsNuevo { get; set; }
    public decimal? Precio { get; set; }
    public string? EstadoCotizacion { get; set; }
    public string? Vendedor { get; set; }
    public DateTime? FechaCotizacion { get; set; }

    /// <summary>"Marca Modelo" para listados.</summary>
    public string Resumen => string.Join(' ', new[] { Marca, Modelo }.Where(s => !string.IsNullOrWhiteSpace(s)));
}

/// <summary>Resultado por operación: el vehículo, o por qué no se pudo obtener.</summary>
public record VehiculoOperacion(int? Cotizacion, string? Empresa, VehiculoCotizacion? Vehiculo, string? Motivo);

public interface IVehiculoOperacionService
{
    /// <summary>
    /// Vehículo de cada operación (referencia RegCheq → cotización Netcar). Nunca lanza: si Netcar o RegCheq
    /// fallan, la operación queda con <see cref="VehiculoOperacion.Motivo"/>.
    /// </summary>
    Task<Dictionary<int, VehiculoOperacion>> ObtenerAsync(RegcheqTipo tipo, IReadOnlyCollection<int> operacionIds, CancellationToken ct = default);
}

/// <summary>
/// Cruza RegCheq con Netcar como la integración (RegcheqController.TraerEstadoFirmasPep):
/// ST_REFERENCENUMBER = T_VEH_COTIZACIONVEHICULO.IN_COD_COTIZACIONVEHICULO, y EMPRESA de
/// DB_REGCHEQ.T_OUT_CLIENTES_* elige la base ("Siglo 21" → NetcarSiglo21Connection; "VYD" u otra → NetcarVyDConnection).
/// En Netcar se ejecuta dbo.sp_UAF_VehiculoCotizacion (solo lectura; SQL/Netcar_01_sp_UAF_VehiculoCotizacion.sql),
/// que replica las uniones de tareasprogramadas/query/cotlstnegocio.sql.
/// </summary>
public class VehiculoOperacionService : IVehiculoOperacionService
{
    private const int TimeoutSegundos = 10;
    private static readonly TimeSpan DuracionCache = TimeSpan.FromMinutes(10);
    /// <summary>Tras un fallo, no se reintenta esa base durante este tiempo (evita esperar el timeout en cada página).</summary>
    private static readonly TimeSpan PausaTrasFallo = TimeSpan.FromMinutes(2);
    private const string Siglo21 = "Siglo 21";
    private const string VyD = "VyD";

    private readonly IRegcheqService _regcheq;
    private readonly IMemoryCache _cache;
    private readonly ILogger<VehiculoOperacionService> _logger;
    private readonly Dictionary<string, string?> _conexiones;

    public VehiculoOperacionService(IRegcheqService regcheq, IMemoryCache cache, IConfiguration config, ILogger<VehiculoOperacionService> logger)
    {
        _regcheq = regcheq;
        _cache = cache;
        _logger = logger;
        _conexiones = new()
        {
            // Siglo 21 ya tenía conexión de solo lectura (ProduccionConnection, usuario uaf_lectura).
            [Siglo21] = config.GetConnectionString("NetcarSiglo21Connection") ?? config.GetConnectionString("ProduccionConnection"),
            [VyD] = config.GetConnectionString("NetcarVyDConnection")
        };
    }

    public async Task<Dictionary<int, VehiculoOperacion>> ObtenerAsync(RegcheqTipo tipo, IReadOnlyCollection<int> operacionIds, CancellationToken ct = default)
    {
        var resultado = new Dictionary<int, VehiculoOperacion>();
        if (operacionIds.Count == 0) return resultado;

        var cotizaciones = await _regcheq.ObtenerCotizacionesAsync(tipo, operacionIds, ct);

        // Agrupar por base Netcar; las que no tienen cotización o empresa se resuelven sin consultar.
        var porBase = new Dictionary<string, List<RegcheqCotizacion>>();
        foreach (var id in operacionIds.Distinct())
        {
            if (!cotizaciones.TryGetValue(id, out var c) || c.Cotizacion is null)
            {
                resultado[id] = new(null, null, null, "La operación no tiene un N° de referencia de cotización válido.");
                continue;
            }
            if (string.IsNullOrWhiteSpace(c.Empresa))
            {
                resultado[id] = new(c.Cotizacion, null, null, "No se encontró la empresa (Siglo 21 / VyD) de esta cotización en RegCheq.");
                continue;
            }
            var baseNetcar = EsSiglo21(c.Empresa) ? Siglo21 : VyD;
            if (!porBase.TryGetValue(baseNetcar, out var lista)) porBase[baseNetcar] = lista = [];
            lista.Add(c);
        }

        foreach (var (baseNetcar, lista) in porBase)
        {
            var vehiculos = await VehiculosAsync(baseNetcar, lista.Select(c => c.Cotizacion!.Value).Distinct().ToList(), ct);
            foreach (var c in lista)
            {
                if (vehiculos is null)
                    resultado[c.OperacionId] = new(c.Cotizacion, baseNetcar, null, $"No se pudo consultar Netcar {baseNetcar}.");
                else if (vehiculos.TryGetValue(c.Cotizacion!.Value, out var v))
                    resultado[c.OperacionId] = new(c.Cotizacion, baseNetcar, v, null);
                else
                    resultado[c.OperacionId] = new(c.Cotizacion, baseNetcar, null, $"La cotización {c.Cotizacion} no existe en Netcar {baseNetcar}.");
            }
        }
        return resultado;
    }

    private static bool EsSiglo21(string empresa) =>
        string.Equals(empresa.Trim(), Siglo21, StringComparison.OrdinalIgnoreCase);

    /// <summary>Vehículos por N° de cotización en una base Netcar, con caché. Null si la consulta falla.</summary>
    private async Task<Dictionary<int, VehiculoCotizacion>?> VehiculosAsync(string baseNetcar, List<int> cotizaciones, CancellationToken ct)
    {
        var encontrados = new Dictionary<int, VehiculoCotizacion>();
        var faltantes = new List<int>();
        foreach (var cot in cotizaciones)
        {
            if (_cache.TryGetValue(ClaveCache(baseNetcar, cot), out VehiculoCotizacion? v))
            {
                if (v is not null) encontrados[cot] = v;
            }
            else faltantes.Add(cot);
        }
        if (faltantes.Count == 0) return encontrados;

        if (_cache.TryGetValue(ClaveCaida(baseNetcar), out _)) return null;

        var conexion = _conexiones[baseNetcar];
        if (string.IsNullOrWhiteSpace(conexion))
        {
            _logger.LogWarning("Conexión a Netcar {Base} no configurada (NetcarSiglo21Connection / NetcarVyDConnection)", baseNetcar);
            return null;
        }

        try
        {
            // uaf_lectura solo tiene EXECUTE sobre este SP (no SELECT sobre las tablas). Ver SQL/Netcar_01_*.sql.
            await using var conn = new SqlConnection(conexion);
            await conn.OpenAsync(ct);
            await using var cmd = new SqlCommand("dbo.sp_UAF_VehiculoCotizacion", conn)
            {
                CommandType = System.Data.CommandType.StoredProcedure,
                CommandTimeout = TimeoutSegundos
            };
            cmd.Parameters.Add(new SqlParameter("@Cotizaciones", System.Data.SqlDbType.VarChar, 4000) { Value = string.Join(",", faltantes) });
            await using var r = await cmd.ExecuteReaderAsync(ct);
            while (await r.ReadAsync(ct))
            {
                var v = new VehiculoCotizacion
                {
                    Cotizacion = Convert.ToInt32(r["Cotizacion"]),
                    Empresa = baseNetcar,
                    Marca = Texto(r["Marca"]),
                    Modelo = Texto(r["Modelo"]),
                    Version = Texto(r["Version"]),
                    Anio = Positivo(r["Anio"]) is { } anio ? (int)anio : null,
                    Color = Texto(r["Color"]),
                    Patente = Texto(r["Patente"]),
                    Chasis = Texto(r["Chasis"]),
                    EsNuevo = r["EsNuevo"] is DBNull ? null : Convert.ToInt32(r["EsNuevo"]) == 1,
                    Precio = Positivo(r["Precio"]),
                    EstadoCotizacion = Texto(r["Estado"]),
                    Vendedor = Texto(r["Vendedor"]),
                    FechaCotizacion = r["Fecha"] as DateTime?
                };
                encontrados[v.Cotizacion] = v;
            }

            // También se guardan en caché las que no existen, para no reconsultarlas en cada página.
            foreach (var cot in faltantes)
                _cache.Set(ClaveCache(baseNetcar, cot), encontrados.GetValueOrDefault(cot), DuracionCache);
            return encontrados;
        }
        catch (Exception ex) when (!ct.IsCancellationRequested)
        {
            _logger.LogError(ex, "Error consultando vehículos en Netcar {Base}; no se reintenta por {Minutos} min", baseNetcar, PausaTrasFallo.TotalMinutes);
            _cache.Set(ClaveCaida(baseNetcar), true, PausaTrasFallo);
            return null;
        }
    }

    private static string ClaveCache(string baseNetcar, int cotizacion) => $"netcar-vehiculo:{baseNetcar}:{cotizacion}";
    private static string ClaveCaida(string baseNetcar) => $"netcar-caida:{baseNetcar}";

    /// <summary>Netcar usa 0 como "sin dato" en año y precio.</summary>
    private static decimal? Positivo(object valor) =>
        valor is DBNull || valor is null ? null : Convert.ToDecimal(valor) is var n && n > 0 ? n : null;

    private static string? Texto(object valor)
    {
        var s = valor is DBNull ? null : valor?.ToString()?.Trim();
        return string.IsNullOrEmpty(s) || s == "-" ? null : s;
    }
}
