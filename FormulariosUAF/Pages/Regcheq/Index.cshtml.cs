using FormulariosUAF.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FormulariosUAF.Pages.Regcheq;

public class IndexModel : PageModel
{
    private const int TamanoPagina = 25;

    private readonly IRegcheqService _regcheq;
    private readonly IVehiculoOperacionService _vehiculos;

    public IndexModel(IRegcheqService regcheq, IVehiculoOperacionService vehiculos)
    {
        _regcheq = regcheq;
        _vehiculos = vehiculos;
    }

    /// <summary>Vehículo de la cotización Netcar por Id de operación (solo las de la página).</summary>
    public Dictionary<int, VehiculoOperacion> Vehiculos { get; private set; } = [];

    // Filtros (query string). Banderas: "si" / "no" / vacío = todos.
    [BindProperty(SupportsGet = true)] public string Tipo { get; set; } = "natural";
    [BindProperty(SupportsGet = true)] public string? Q { get; set; }
    [BindProperty(SupportsGet = true)] public DateTime? Desde { get; set; }
    [BindProperty(SupportsGet = true)] public DateTime? Hasta { get; set; }
    [BindProperty(SupportsGet = true)] public string? Pep { get; set; }
    [BindProperty(SupportsGet = true)] public string? ListaPep { get; set; }
    [BindProperty(SupportsGet = true)] public string? FirmoPep { get; set; }
    [BindProperty(SupportsGet = true)] public string? FirmoDof { get; set; }
    [BindProperty(SupportsGet = true)] public string? Fin { get; set; }
    /// <summary>Estado de firmas (solo naturales): completa | pep | dof | ambos | norequiere | sinseguimiento.</summary>
    [BindProperty(SupportsGet = true)] public string? Firmas { get; set; }
    [BindProperty(SupportsGet = true)] public int Pagina { get; set; } = 1;
    /// <summary>Columna de orden (ver <see cref="RegcheqFiltro.Orden"/>). Vacío = fecha, más nuevas primero.</summary>
    [BindProperty(SupportsGet = true)] public string? Orden { get; set; }
    /// <summary>"asc" | "desc".</summary>
    [BindProperty(SupportsGet = true)] public string? Dir { get; set; }

    /// <summary>Columnas que solo existen en personas naturales.</summary>
    private static readonly string[] OrdenSoloNatural = ["firmapep", "firmadof", "firmas"];

    public string OrdenActual => string.IsNullOrWhiteSpace(Orden) ? "fecha" : Orden.ToLowerInvariant();
    /// <summary>Sin orden elegido: fecha descendente (como antes). Columna elegida sin sentido: ascendente.</summary>
    public bool Ascendente => Dir?.ToLowerInvariant() switch
    {
        "asc" => true,
        "desc" => false,
        _ => !string.IsNullOrWhiteSpace(Orden)
    };

    public RegcheqListado Listado { get; set; } = new();
    public bool EsNatural => TipoEnum == RegcheqTipo.Natural;
    public int TotalPaginas => Math.Max(1, (int)Math.Ceiling(Listado.Totales.Total / (double)TamanoPagina));

    private RegcheqTipo TipoEnum =>
        string.Equals(Tipo, "empresa", StringComparison.OrdinalIgnoreCase) ? RegcheqTipo.Empresa : RegcheqTipo.Natural;

    public async Task OnGetAsync(CancellationToken ct)
    {
        Tipo = EsNatural ? "natural" : "empresa";
        Pagina = Math.Max(1, Pagina);

        // Sin fechas en la URL: por defecto todo el año en curso (1 de enero al 31 de diciembre).
        var anio = DateTime.Today.Year;
        Desde ??= new DateTime(anio, 1, 1);
        Hasta ??= new DateTime(anio, 12, 31);

        Listado = await _regcheq.ListarAsync(new RegcheqFiltro
        {
            Tipo = TipoEnum,
            Texto = Q,
            Desde = Desde,
            Hasta = Hasta,
            TienePep = Bandera(Pep),
            CoincidenciaListaPep = Bandera(ListaPep),
            FirmoPep = Bandera(FirmoPep),
            FirmoDof = Bandera(FirmoDof),
            Finalizada = Bandera(Fin),
            Firmas = EstadoFirmasFiltro(Firmas),
            Orden = OrdenActual,
            Ascendente = Ascendente,
            Pagina = Pagina,
            TamanoPagina = TamanoPagina
        }, ct);

        if (Listado.Operaciones.Count > 0)
            Vehiculos = await _vehiculos.ObtenerAsync(TipoEnum, Listado.Operaciones.Select(o => o.Id).ToList(), ct);
    }

    /// <summary>Query string para paginar o cambiar de pestaña manteniendo los filtros.</summary>
    public Dictionary<string, string?> Ruta(int? pagina = null, string? tipo = null)
    {
        var mismoTipo = tipo is null || tipo == Tipo;
        return new Dictionary<string, string?>
        {
            ["tipo"] = tipo ?? Tipo,
            ["q"] = Q,
            ["desde"] = Desde?.ToString("yyyy-MM-dd"),
            ["hasta"] = Hasta?.ToString("yyyy-MM-dd"),
            ["pep"] = Pep,
            ["listaPep"] = ListaPep,
            ["firmoPep"] = mismoTipo ? FirmoPep : null,
            ["firmoDof"] = mismoTipo ? FirmoDof : null,
            ["fin"] = Fin,
            ["firmas"] = mismoTipo ? Firmas : null,
            // Al cambiar de pestaña se descarta un orden por columna que la otra no tiene.
            ["orden"] = mismoTipo || !OrdenSoloNatural.Contains(OrdenActual) ? Orden : null,
            ["dir"] = mismoTipo || !OrdenSoloNatural.Contains(OrdenActual) ? Dir : null,
            ["pagina"] = (pagina ?? 1).ToString()
        };
    }

    /// <summary>Enlace de un encabezado: ordena por esa columna; si ya lo está, invierte el sentido. Vuelve a la página 1.</summary>
    public Dictionary<string, string?> RutaOrden(string columna)
    {
        var ruta = Ruta();
        ruta["orden"] = columna;
        ruta["dir"] = OrdenActual == columna ? (Ascendente ? "desc" : "asc") : "asc";
        return ruta;
    }

    private static EstadoFirmas? EstadoFirmasFiltro(string? valor) => valor?.ToLowerInvariant() switch
    {
        "completa" => EstadoFirmas.Completa,
        "pep" => EstadoFirmas.Pep,
        "dof" => EstadoFirmas.Dof,
        "ambos" => EstadoFirmas.PepYDof,
        "norequiere" => EstadoFirmas.NoRequiere,
        "sinseguimiento" => EstadoFirmas.SinSeguimiento,
        _ => null
    };

    private static bool? Bandera(string? valor) => valor?.ToLowerInvariant() switch
    {
        "si" => true,
        "no" => false,
        _ => null
    };
}
