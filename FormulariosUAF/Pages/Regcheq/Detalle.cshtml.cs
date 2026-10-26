using FormulariosUAF.Data;
using FormulariosUAF.Models.Domain;
using FormulariosUAF.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace FormulariosUAF.Pages.Regcheq;

public class DetalleModel : PageModel
{
    private readonly IRegcheqService _regcheq;
    private readonly IVehiculoOperacionService _vehiculos;
    private readonly ApplicationDbContext _db;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ILogger<DetalleModel> _logger;

    public DetalleModel(IRegcheqService regcheq, IVehiculoOperacionService vehiculos, ApplicationDbContext db,
                        UserManager<ApplicationUser> userManager, ILogger<DetalleModel> logger)
    {
        _regcheq = regcheq;
        _vehiculos = vehiculos;
        _db = db;
        _userManager = userManager;
        _logger = logger;
    }

    public RegcheqOperacionDetalle? Operacion { get; set; }
    public string Tipo { get; set; } = "natural";
    public string? Error { get; set; }
    public bool EsNatural => Tipo == "natural";
    /// <summary>Vehículo de la cotización Netcar (null solo si no se consultó).</summary>
    public VehiculoOperacion? Vehiculo { get; private set; }

    // Gestión (solo personas naturales; las acciones las procesa Regcheq/Gestion con origen=detalle)
    public GestionOperacion? Gestion { get; private set; }
    public List<GestionOperacionNota> Notas { get; private set; } = [];
    public string? UsuarioActualId { get; private set; }
    public bool EsAdmin { get; private set; }
    /// <summary>Vendedor (sin otro rol RegCheq): solo entra a sus leads y no ve ROS ni los datos técnicos.</summary>
    public bool EsVendedor { get; private set; }
    /// <summary>Administrador, Cumplimiento, Call Center o el vendedor de la cotización (Revisor solo ve).</summary>
    public bool PuedeGestionar { get; private set; }

    /// <summary>Caso pendiente según RegCheq (null si no falta firma o la operación es anterior al seguimiento).</summary>
    public CasoGestion? CasoPendiente => Operacion?.EstadoFirmas is { } e && (int)e is >= 1 and <= 3 ? (CasoGestion)(int)e : null;
    public bool TomadaPorMi => Gestion?.UsuarioId is { } u && u == UsuarioActualId;
    /// <summary>Registrar gestiones y liberar: solo quien tiene tomada la operación, y solo si está abierta.
    /// Ni el administrador escribe en la bitácora de otro; él solo puede reasignar (vista Gestión).</summary>
    public bool PuedeOperar => PuedeGestionar && Gestion is { FechaCierre: null, UsuarioId: not null } && TomadaPorMi;
    /// <summary>Tomada por otro usuario y abierta: se muestra quién la gestiona (bloqueada para este usuario).</summary>
    public bool TomadaPorOtro => Gestion is { FechaCierre: null, UsuarioId: not null } && !TomadaPorMi;
    public bool MostrarGestion => EsNatural && (CasoPendiente is not null || Gestion is not null);

    public async Task<IActionResult> OnGetAsync(string? tipo, int id, CancellationToken ct)
    {
        var tipoEnum = string.Equals(tipo, "empresa", StringComparison.OrdinalIgnoreCase) ? RegcheqTipo.Empresa : RegcheqTipo.Natural;
        Tipo = tipoEnum == RegcheqTipo.Empresa ? "empresa" : "natural";

        try
        {
            Operacion = await _regcheq.ObtenerAsync(tipoEnum, id, ct);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "No se pudo cargar la operación RegCheq {Tipo} {Id}", Tipo, id);
            Error = "No se pudo consultar RegCheq. Intente nuevamente o revise el log.";
            return Page();
        }

        if (Operacion is null) return NotFound();

        Vehiculo = (await _vehiculos.ObtenerAsync(tipoEnum, [id], ct)).GetValueOrDefault(id);

        // Vendedor: solo si la cotización Netcar de la operación es suya (misma regla que Operaciones y Gestión).
        EsVendedor = LeadsVendedorService.EsSoloVendedor(User);
        if (EsVendedor)
        {
            var yo = await _userManager.GetUserAsync(User);
            var codigo = yo is null ? null : LeadsVendedorService.CodigoNetcar(yo);
            if (Vehiculo is { NetcarNoDisponible: true })
            {
                Operacion = null;
                Error = $"{Vehiculo.Motivo} No se pudo confirmar que la operación sea suya; intente nuevamente en unos minutos.";
                return Page();
            }
            if (string.IsNullOrWhiteSpace(codigo)
                || !string.Equals(Vehiculo?.Vehiculo?.Vendedor, codigo, StringComparison.OrdinalIgnoreCase))
                return Forbid();
        }

        if (EsNatural)
        {
            UsuarioActualId = _userManager.GetUserId(User);
            EsAdmin = User.IsInRole("Administrador");
            PuedeGestionar = EsAdmin || User.IsInRole("Cumplimiento") || User.IsInRole("Call Center") || EsVendedor;

            Gestion = await _db.GestionesOperacion.AsNoTracking()
                .Include(g => g.Usuario)
                .FirstOrDefaultAsync(g => g.OperacionId == id, ct);
            if (Gestion is not null)
                Notas = await _db.GestionesOperacionNotas.AsNoTracking()
                    .Where(n => n.GestionOperacionId == Gestion.Id)
                    .OrderByDescending(n => n.CreatedAt).ThenByDescending(n => n.Id)
                    .ToListAsync(ct);
        }

        return Page();
    }
}
