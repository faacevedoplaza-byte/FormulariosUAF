namespace FormulariosUAF.Services;

/// <summary>Tipo de operación RegCheq: persona natural o empresa (tablas separadas en DB_REGCHEQ).</summary>
public enum RegcheqTipo
{
    Natural,
    Empresa
}

public class RegcheqFiltro
{
    public RegcheqTipo Tipo { get; set; } = RegcheqTipo.Natural;
    /// <summary>RUT, nombre, N° de referencia o código de la operación.</summary>
    public string? Texto { get; set; }
    public DateTime? Desde { get; set; }
    public DateTime? Hasta { get; set; }
    public bool? TienePep { get; set; }
    /// <summary>Algún asociado coincide en la lista pepChile (BT_COINCIDENCE). false = no coincide (o sin revisión).</summary>
    public bool? CoincidenciaListaPep { get; set; }
    /// <summary>Solo personas naturales (las operaciones de empresa no tienen estas columnas).</summary>
    public bool? FirmoPep { get; set; }
    /// <summary>Solo personas naturales.</summary>
    public bool? FirmoDof { get; set; }
    public bool? Finalizada { get; set; }
    /// <summary>Estado de firmas PEP/DOF (solo personas naturales).</summary>
    public EstadoFirmas? Firmas { get; set; }
    /// <summary>
    /// Columna de orden: fecha | codigo | cliente | monto | efectivo | pep | listapep | firmapep | firmadof |
    /// formularios | estado | firmas | finalizada. Desconocida o vacía = fecha.
    /// </summary>
    public string? Orden { get; set; }
    public bool Ascendente { get; set; }
    public int Pagina { get; set; } = 1;
    public int TamanoPagina { get; set; } = 25;
}

public class RegcheqOperacionResumen
{
    public int Id { get; set; }
    public int? Codigo { get; set; }
    public string? Referencia { get; set; }
    public string? Estado { get; set; }
    public string? TipoTransaccion { get; set; }
    public int? MontoTotal { get; set; }
    public int? MontoEfectivo { get; set; }
    public string? MontoFormateado { get; set; }
    public string? Divisa { get; set; }
    public bool? Efectivo { get; set; }
    public bool? TienePep { get; set; }
    public bool? FormulariosRequeridos { get; set; }
    public bool? FormulariosEnviados { get; set; }
    public bool? FormulariosFirmados { get; set; }
    public bool? Finalizada { get; set; }
    /// <summary>Lista pepChile: true = algún asociado coincide, false = revisado sin coincidencia, null = sin registro pepChile.</summary>
    public bool? CoincidenciaListaPep { get; set; }
    /// <summary>Firma del formulario PEP (solo personas naturales; null en empresas).</summary>
    public bool? FirmoPep { get; set; }
    public DateTime? FechaFirmaPep { get; set; }
    /// <summary>Firma del formulario DOF (solo personas naturales; null en empresas).</summary>
    public bool? FirmoDof { get; set; }
    public DateTime? FechaFirmaDof { get; set; }
    public DateTime? FechaCreacion { get; set; }
    /// <summary>RUT/DNI del primer asociado de la operación.</summary>
    public string? AsociadoRut { get; set; }
    /// <summary>Nombre (o razón social) del primer asociado, desde su ficha.</summary>
    public string? AsociadoNombre { get; set; }
    public int CantidadAsociados { get; set; }
    /// <summary>Formulario PEP/DOF requerido para el primer asociado (solo naturales; null en empresas o sin asociado).</summary>
    public bool? RequierePep { get; set; }
    public bool? RequiereDof { get; set; }

    /// <summary>Null en empresas (no tienen firma PEP/DOF) o si la operación no tiene asociado.</summary>
    public EstadoFirmas? EstadoFirmas =>
        RequierePep is { } rp && RequiereDof is { } rd
            ? RegcheqFirmas.Estado(rp, rd, FirmoPep == true, FirmoDof == true, FechaCreacion)
            : null;
}

public class RegcheqTotales
{
    public int Total { get; set; }
    public int ConPep { get; set; }
    public int FirmoPep { get; set; }
    public int FirmoDof { get; set; }
    public int FormulariosFirmados { get; set; }
    public int Finalizadas { get; set; }
    /// <summary>Operaciones con todos los formularios requeridos firmados (solo naturales).</summary>
    public int FirmasCompletas { get; set; }
    public int CoincidenciaListaPep { get; set; }
}

public class RegcheqListado
{
    /// <summary>False si falta la cadena RegCheqConnection.</summary>
    public bool Configurado { get; set; } = true;
    /// <summary>Mensaje para el usuario si la consulta falló.</summary>
    public string? Error { get; set; }
    public List<RegcheqOperacionResumen> Operaciones { get; set; } = [];
    public RegcheqTotales Totales { get; set; } = new();
}

public class RegcheqOperacionDetalle : RegcheqOperacionResumen
{
    public RegcheqTipo Tipo { get; set; }
    /// <summary>Identificador de la operación en RegCheq (ST_ID).</summary>
    public string? RegcheqId { get; set; }
    public string? CompanyId { get; set; }
    public string? CreadoPor { get; set; }
    public string? ActualizadoPor { get; set; }
    public string? Operacion { get; set; }
    public bool? Leido { get; set; }
    public bool? ArchivoRequerido { get; set; }
    public string? EstadoArchivo { get; set; }
    public string? Firmas { get; set; }
    public string? Ros { get; set; }
    public string? ComentariosTransaccion { get; set; }
    public string? FechaTransaccion { get; set; }
    public string? ValorDolar { get; set; }
    public string? ValorUf { get; set; }
    public string? FormularioPep { get; set; }
    public string? FormularioDof { get; set; }
    public DateTime? FechaActualizacion { get; set; }
    public DateTime? FechaRecibido { get; set; }
    public string? RespuestaJson { get; set; }
    public List<RegcheqAsociado> Asociados { get; set; } = [];
}

public class RegcheqAsociado
{
    public int Id { get; set; }
    public string? Rut { get; set; }
    public string? Tipo { get; set; }
    public int? Monto { get; set; }
    public string? MontoFormateado { get; set; }
    public string? Moneda { get; set; }
    public bool? Efectivo { get; set; }
    public int? MontoEfectivo { get; set; }
    public string? FichaId { get; set; }
    public bool? Pep { get; set; }
    public int? NivelPep { get; set; }
    public string? RiesgoCalculado { get; set; }
    public string? EstadoFicha { get; set; }
    public string? EstadoFormularioPep { get; set; }
    public string? EstadoFormularioDof { get; set; }
    public string? EstadoFormularioBf { get; set; }
    public bool? Roe { get; set; }
    public DateTime? FechaRecibido { get; set; }
    public RegcheqFicha? Ficha { get; set; }
}

public class RegcheqFicha
{
    public int Id { get; set; }
    public string? Nombre { get; set; }
    public string? ApellidoPaterno { get; set; }
    public string? ApellidoMaterno { get; set; }
    public string? RazonSocial { get; set; }
    public string? NombreFantasia { get; set; }
    public string? Dni { get; set; }
    public string? Rut { get; set; }
    public string? PaisDni { get; set; }
    public string? TipoPersona { get; set; }
    public string? Email { get; set; }
    public string? Telefono { get; set; }
    public string? Nacionalidad { get; set; }
    public string? Pais { get; set; }
    public string? Region { get; set; }
    public string? Ciudad { get; set; }
    public string? Direccion { get; set; }
    public string? Cargo { get; set; }
    public string? Empleador { get; set; }
    public string? Ingresos { get; set; }
    public string? EstadoCivil { get; set; }
    public string? Genero { get; set; }
    public DateTime? FechaNacimiento { get; set; }
    public string? Estado { get; set; }
    public string? Riesgo { get; set; }
    public string? RiesgoCalculado { get; set; }
    public string? RiesgoEfectivo { get; set; }
    public string? RiesgoSobrescrito { get; set; }
    public int? NivelPep { get; set; }
    public string? UltimaFechaDof { get; set; }
    public string? UltimaFechaPep { get; set; }
    public string? Comentarios { get; set; }
    public DateTime? FechaCreacion { get; set; }
    public DateTime? FechaActualizacion { get; set; }
    public List<RegcheqLista> Listas { get; set; } = [];

    public string NombreCompleto =>
        !string.IsNullOrWhiteSpace(RazonSocial)
            ? RazonSocial!
            : string.Join(' ', new[] { Nombre, ApellidoPaterno, ApellidoMaterno }.Where(s => !string.IsNullOrWhiteSpace(s)));
}

public class RegcheqLista
{
    public string? Tipo { get; set; }
    public bool? Coincidencia { get; set; }
    public bool? Resultado { get; set; }
    public string? Riesgo { get; set; }
    public int? NivelPep { get; set; }
    public string? Cargo { get; set; }
    public string? InfoGeneral { get; set; }
    /// <summary>JSON <c>data.info</c> de la lista (en pepChile: cargo, institución, etc.).</summary>
    public string? InfoData { get; set; }
    /// <summary>JSON <c>data.additionalData</c> de la lista.</summary>
    public string? DatosAdicionales { get; set; }
    public DateTime? UltimaRevision { get; set; }

    public bool EsPep => string.Equals(Tipo, "pepChile", StringComparison.OrdinalIgnoreCase);
    public bool TieneDetalle =>
        !string.IsNullOrWhiteSpace(InfoData) || !string.IsNullOrWhiteSpace(DatosAdicionales) || !string.IsNullOrWhiteSpace(InfoGeneral);
}

/// <summary>Caso de una operación con firma pendiente, según qué formularios REQUIERE (no según cuál falta).</summary>
public enum CasoGestion
{
    /// <summary>Requiere PEP (sin firmar) y no requiere DOF.</summary>
    Pep = 1,
    /// <summary>Requiere DOF (sin firmar) y no requiere PEP.</summary>
    Dof = 2,
    /// <summary>Requiere PEP y DOF, y falta al menos uno: sigue abierta hasta que ambos estén firmados.</summary>
    PepYDof = 3
}

/// <summary>Estado de firmas de una operación (columna "Firmas" de /Regcheq). 1-3 coinciden con <see cref="CasoGestion"/>.</summary>
public enum EstadoFirmas
{
    Pep = 1,
    Dof = 2,
    PepYDof = 3,
    /// <summary>Todos los formularios requeridos están firmados.</summary>
    Completa = 10,
    /// <summary>No requiere PEP ni DOF.</summary>
    NoRequiere = 11,
    /// <summary>Falta una firma, pero la operación es anterior al seguimiento de firmas: no se puede saber.</summary>
    SinSeguimiento = 12
}

/// <summary>Regla única de firmas PEP/DOF; la usan /Regcheq, /Regcheq/Gestion y el cierre automático.</summary>
public static class RegcheqFirmas
{
    /// <summary>
    /// El job TraerEstadoFirmasPep (integracionesNetcar) solo actualiza las firmas de operaciones creadas
    /// desde esta fecha; las anteriores nunca se marcarían como firmadas.
    /// </summary>
    public static readonly DateTime InicioSeguimiento = new(2026, 1, 1);

    /// <summary>Valor de ST_PEPDEFAULTFORMSTATUS / ST_DOFFORMSTATUS cuando el formulario es obligatorio (el otro es NOTREQUIRED).</summary>
    public const string Requerido = "REQUIRED";

    /// <summary>Caso pendiente, o null si no falta ninguna firma requerida.</summary>
    public static CasoGestion? Caso(bool requierePep, bool requiereDof, bool firmoPep, bool firmoDof) =>
        requierePep && requiereDof ? (firmoPep && firmoDof ? null : CasoGestion.PepYDof)
        : requierePep ? (firmoPep ? null : CasoGestion.Pep)
        : requiereDof ? (firmoDof ? null : CasoGestion.Dof)
        : null;

    public static EstadoFirmas Estado(bool requierePep, bool requiereDof, bool firmoPep, bool firmoDof, DateTime? creada)
    {
        if (!requierePep && !requiereDof) return EstadoFirmas.NoRequiere;
        if (Caso(requierePep, requiereDof, firmoPep, firmoDof) is not { } caso) return EstadoFirmas.Completa;
        return creada < InicioSeguimiento ? EstadoFirmas.SinSeguimiento : (EstadoFirmas)(int)caso;
    }
}

/// <summary>Operación natural con algún formulario requerido (PEP/DOF) aún sin firmar.</summary>
public class RegcheqPendienteGestion
{
    public int Id { get; set; }
    public int? Codigo { get; set; }
    public string? Referencia { get; set; }
    public DateTime? FechaCreacion { get; set; }
    public string? Rut { get; set; }
    public string? Nombre { get; set; }
    /// <summary>ST_PEPDEFAULTFORMSTATUS del asociado = REQUIRED.</summary>
    public bool RequierePep { get; set; }
    /// <summary>ST_DOFFORMSTATUS del asociado = REQUIRED.</summary>
    public bool RequiereDof { get; set; }
    public bool FirmoPep { get; set; }
    public bool FirmoDof { get; set; }

    public bool PepPendiente => RequierePep && !FirmoPep;
    public bool DofPendiente => RequiereDof && !FirmoDof;
    // La consulta solo trae operaciones con firma pendiente, así que siempre hay caso.
    public CasoGestion Caso => RegcheqFirmas.Caso(RequierePep, RequiereDof, FirmoPep, FirmoDof) ?? CasoGestion.PepYDof;
}

/// <summary>Cotización Netcar de una operación: ST_REFERENCENUMBER = IN_COD_COTIZACIONVEHICULO.</summary>
/// <param name="Empresa">EMPRESA de DB_REGCHEQ.T_OUT_CLIENTES_* ("Siglo 21" o VyD); null si no se encontró.</param>
public record RegcheqCotizacion(int OperacionId, int? Cotizacion, string? Empresa);

public interface IRegcheqService
{
    /// <summary>
    /// N° de cotización Netcar y empresa (Siglo 21 / VyD) de cada operación, igual que la integración
    /// (T_OUT_CLIENTES_* por NEGOCIO = referencia y RUT = asociado). Solo lectura.
    /// </summary>
    Task<Dictionary<int, RegcheqCotizacion>> ObtenerCotizacionesAsync(RegcheqTipo tipo, IReadOnlyCollection<int> operacionIds, CancellationToken ct = default);

    /// <summary>
    /// Operaciones de persona natural creadas desde <paramref name="desde"/> que requieren PEP y/o DOF
    /// y todavía no lo firman (vista Gestión). Null si RegCheqConnection no está configurada.
    /// Lanza excepción si la consulta falla (el llamador no debe cerrar gestiones en ese caso).
    /// </summary>
    Task<List<RegcheqPendienteGestion>?> ListarPendientesGestionAsync(DateTime desde, CancellationToken ct = default);

    /// <summary>Lista operaciones RegCheq con filtros y paginación (solo lectura).</summary>
    Task<RegcheqListado> ListarAsync(RegcheqFiltro filtro, CancellationToken ct = default);

    /// <summary>Detalle de una operación con sus asociados, fichas y listas (solo lectura).</summary>
    Task<RegcheqOperacionDetalle?> ObtenerAsync(RegcheqTipo tipo, int id, CancellationToken ct = default);
}
