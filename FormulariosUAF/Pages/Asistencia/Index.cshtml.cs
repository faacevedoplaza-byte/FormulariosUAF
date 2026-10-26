using System.ComponentModel.DataAnnotations;
using FormulariosUAF.Data;
using FormulariosUAF.Helpers;
using FormulariosUAF.Hubs;
using FormulariosUAF.Models.Domain;
using FormulariosUAF.Validation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace FormulariosUAF.Pages.Asistencia;

/// <summary>
/// Enlace público de registro de asistencia a una capacitación (/Asistencia?t=..., sin iniciar sesión).
/// El enlace lo genera Admin/Capacitaciones; cada RUT se registra una vez por capacitación.
/// </summary>
[EnableRateLimiting("AsistenciaPublica")]
public class IndexModel : PageModel
{
    private readonly ApplicationDbContext _db;
    private readonly IHubContext<NotificationHub> _hub;
    private readonly ILogger<IndexModel> _logger;

    public IndexModel(ApplicationDbContext db, IHubContext<NotificationHub> hub, ILogger<IndexModel> logger)
    {
        _db = db;
        _hub = hub;
        _logger = logger;
    }

    [BindProperty(SupportsGet = true, Name = "t")] public string? Token { get; set; }
    [BindProperty] public InputModel Input { get; set; } = new();

    public Capacitacion? Capacitacion { get; private set; }
    public string? ErrorMessage { get; private set; }
    /// <summary>Registro recién guardado (o el que ya existía para ese RUT): se muestra la confirmación.</summary>
    public CapacitacionAsistencia? Registrado { get; private set; }
    public bool YaEstabaRegistrado { get; private set; }
    public List<Empresa> Empresas { get; private set; } = [];

    public class InputModel
    {
        [Required(ErrorMessage = "El nombre es obligatorio")]
        [MaxLength(200)]
        public string NombreCompleto { get; set; } = string.Empty;

        [Required(ErrorMessage = "El RUT es obligatorio")]
        [MaxLength(20)]
        public string Rut { get; set; } = string.Empty;

        [Required(ErrorMessage = "El correo es obligatorio")]
        [EmailAddress(ErrorMessage = "Correo inválido")]
        [MaxLength(256)]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Seleccione la empresa")]
        public int? EmpresaId { get; set; }

        [MaxLength(30)]
        public string? Telefono { get; set; }

        [MaxLength(100)]
        public string? Cargo { get; set; }

        [MaxLength(100)]
        public string? Sucursal { get; set; }

        [MustBeTrue(ErrorMessage = "Debe confirmar que asistió a la capacitación")]
        public bool AceptaAsistencia { get; set; }
    }

    public async Task OnGetAsync(CancellationToken ct) => await CargarAsync(ct);

    public async Task<IActionResult> OnPostAsync(CancellationToken ct)
    {
        if (!await CargarAsync(ct)) return Page();

        // Sin ceros a la izquierda: "011.111.111-1" es el mismo RUT que "11.111.111-1" (el dígito verificador
        // valida igual) y no debe permitir un segundo registro en la misma capacitación.
        var rut = RutHelper.Normalizar(Input.Rut).TrimStart('0');
        if (ModelState.IsValid && !RutHelper.EsValido(rut))
            ModelState.AddModelError("Input.Rut", "El RUT no es válido.");
        if (Input.EmpresaId is int empresaId && !Empresas.Any(e => e.Id == empresaId))
            ModelState.AddModelError("Input.EmpresaId", "Seleccione la empresa");
        if (!ModelState.IsValid) return Page();

        var existente = await _db.CapacitacionesAsistencia.AsNoTracking()
            .FirstOrDefaultAsync(a => a.CapacitacionId == Capacitacion!.Id && a.Rut == rut, ct);
        if (existente is not null)
        {
            Registrado = existente;
            YaEstabaRegistrado = true;
            return Page();
        }

        var asistencia = new CapacitacionAsistencia
        {
            CapacitacionId = Capacitacion!.Id,
            NombreCompleto = Input.NombreCompleto.Trim(),
            Rut = rut,
            Email = Input.Email.Trim().ToLowerInvariant(),
            Telefono = Limpiar(Input.Telefono),
            Cargo = Limpiar(Input.Cargo),
            Sucursal = Limpiar(Input.Sucursal),
            EmpresaId = Input.EmpresaId,
            AceptaAsistencia = true,
            IpAddress = HttpContext.Connection.RemoteIpAddress?.ToString(),
            UserAgent = Recortar(Request.Headers.UserAgent.ToString(), 500),
        };
        _db.CapacitacionesAsistencia.Add(asistencia);
        try
        {
            await _db.SaveChangesAsync(ct);
        }
        catch (DbUpdateException)
        {
            // Doble envío simultáneo del mismo RUT: el índice único lo rechazó, se muestra el que quedó.
            _db.ChangeTracker.Clear();
            var ganador = await _db.CapacitacionesAsistencia.AsNoTracking()
                .FirstOrDefaultAsync(a => a.CapacitacionId == Capacitacion.Id && a.Rut == rut, ct);
            if (ganador is null) throw;
            Registrado = ganador;
            YaEstabaRegistrado = true;
            return Page();
        }

        Registrado = asistencia;
        // Admin/Capacitaciones/Detalle abierto agrega al asistente sin recargar.
        await _hub.AvisarCapacitacionCambiadaAsync(Capacitacion.Id, _logger);
        return Page();
    }

    /// <summary>Busca la capacitación del enlace. False (con ErrorMessage) si el enlace no sirve.</summary>
    private async Task<bool> CargarAsync(CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(Token))
        {
            ErrorMessage = "El enlace está incompleto. Pide el enlace correcto a quien dictó la capacitación.";
            return false;
        }

        Capacitacion = await _db.Capacitaciones.AsNoTracking().FirstOrDefaultAsync(c => c.Token == Token, ct);
        if (Capacitacion is null)
        {
            ErrorMessage = "El enlace no es válido o no existe.";
            return false;
        }
        if (!Capacitacion.RegistroAbierto)
        {
            ErrorMessage = "El registro de asistencia para esta capacitación ya está cerrado.";
            return false;
        }

        Empresas = await _db.Empresas.AsNoTracking().Where(e => e.Activo)
            .OrderBy(e => e.Orden).ThenBy(e => e.Nombre).ToListAsync(ct);
        return true;
    }

    private static string? Limpiar(string? s) => string.IsNullOrWhiteSpace(s) ? null : s.Trim();

    private static string? Recortar(string? s, int max) =>
        string.IsNullOrEmpty(s) ? null : s.Length <= max ? s : s[..max];
}
