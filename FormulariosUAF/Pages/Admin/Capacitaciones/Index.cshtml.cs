using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using FormulariosUAF.Data;
using FormulariosUAF.Hubs;
using FormulariosUAF.Models.Domain;
using FormulariosUAF.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace FormulariosUAF.Pages.Admin.Capacitaciones;

/// <summary>Capacitaciones con enlace público de asistencia (Pages/Asistencia): listado y creación.</summary>
public class IndexModel : PageModel
{
    private readonly ApplicationDbContext _db;
    private readonly IAuditService _audit;
    private readonly IHubContext<NotificationHub> _hub;
    private readonly ILogger<IndexModel> _logger;

    public IndexModel(ApplicationDbContext db, IAuditService audit, IHubContext<NotificationHub> hub, ILogger<IndexModel> logger)
    {
        _db = db;
        _audit = audit;
        _hub = hub;
        _logger = logger;
    }

    [BindProperty] public InputModel Input { get; set; } = new();

    public record Fila(int Id, string Titulo, DateTime Fecha, string? Lugar, string? Relator, bool RegistroAbierto, int Asistentes);
    public List<Fila> Capacitaciones { get; private set; } = [];

    public class InputModel
    {
        [Required(ErrorMessage = "El título es obligatorio")]
        [MaxLength(200)]
        public string Titulo { get; set; } = string.Empty;

        [Required(ErrorMessage = "La fecha es obligatoria")]
        public DateTime? Fecha { get; set; }

        [MaxLength(200)]
        public string? Lugar { get; set; }

        [MaxLength(200)]
        public string? Relator { get; set; }

        [MaxLength(2000)]
        public string? Descripcion { get; set; }
    }

    public async Task OnGetAsync(CancellationToken ct)
    {
        Input.Fecha = DateTime.Today.AddHours(DateTime.Now.Hour);
        await CargarAsync(ct);
    }

    public async Task<IActionResult> OnPostAsync(CancellationToken ct)
    {
        if (!ModelState.IsValid)
        {
            await CargarAsync(ct);
            return Page();
        }

        var cap = new Capacitacion
        {
            Titulo = Input.Titulo.Trim(),
            Fecha = Input.Fecha!.Value,
            Lugar = string.IsNullOrWhiteSpace(Input.Lugar) ? null : Input.Lugar.Trim(),
            Relator = string.IsNullOrWhiteSpace(Input.Relator) ? null : Input.Relator.Trim(),
            Descripcion = string.IsNullOrWhiteSpace(Input.Descripcion) ? null : Input.Descripcion.Trim(),
            Token = NuevoToken(),
            CreadoPor = User.Identity?.Name,
        };
        _db.Capacitaciones.Add(cap);
        await _db.SaveChangesAsync(ct);

        await _audit.LogAsync("CREAR_CAPACITACION", "Capacitacion", cap.Id.ToString(),
            newValues: new { cap.Titulo, cap.Fecha },
            userId: User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value, userName: User.Identity?.Name);
        await _hub.AvisarCapacitacionCambiadaAsync(cap.Id, _logger);

        TempData["Success"] = "Capacitación creada. Comparte el enlace o el código QR con los asistentes.";
        return RedirectToPage("Detalle", new { id = cap.Id });
    }

    private async Task CargarAsync(CancellationToken ct)
    {
        Capacitaciones = await _db.Capacitaciones.AsNoTracking()
            .OrderByDescending(c => c.Fecha)
            .Select(c => new Fila(c.Id, c.Titulo, c.Fecha, c.Lugar, c.Relator, c.RegistroAbierto, c.Asistencias.Count))
            .ToListAsync(ct);
    }

    /// <summary>Token corto (22 caracteres, URL-safe) para que el enlace y el QR sean fáciles de usar.</summary>
    private static string NuevoToken() =>
        Convert.ToBase64String(RandomNumberGenerator.GetBytes(16)).Replace("+", "-").Replace("/", "_").TrimEnd('=');
}
