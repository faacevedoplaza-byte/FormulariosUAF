using System.Text;
using FormulariosUAF.Data;
using FormulariosUAF.Hubs;
using FormulariosUAF.Models.Domain;
using FormulariosUAF.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace FormulariosUAF.Pages.Admin.Capacitaciones;

/// <summary>
/// Una capacitación: enlace/QR para los asistentes, la lista de quienes se registraron y el cruce con los
/// usuarios del sistema (por RUT normalizado y por correo).
/// </summary>
public class DetalleModel : PageModel
{
    private readonly ApplicationDbContext _db;
    private readonly IAuditService _audit;
    private readonly IHubContext<NotificationHub> _hub;
    private readonly ILogger<DetalleModel> _logger;

    public DetalleModel(ApplicationDbContext db, IAuditService audit, IHubContext<NotificationHub> hub, ILogger<DetalleModel> logger)
    {
        _db = db;
        _audit = audit;
        _hub = hub;
        _logger = logger;
    }

    public Capacitacion Capacitacion { get; private set; } = null!;
    public string Enlace { get; private set; } = string.Empty;
    public List<FilaAsistente> Asistentes { get; private set; } = [];
    /// <summary>Usuarios activos del sistema que no aparecen entre los asistentes (ni por RUT ni por correo).</summary>
    public List<UsuarioSistema> SinRegistro { get; private set; } = [];

    public enum Verificacion { Coincide, RutCorreoDistinto, SoloCorreo, NoEsUsuario }

    public record UsuarioSistema(string Id, string Nombre, string? UserName, string? Rut, string? Email, bool Activo, string Roles);
    public record FilaAsistente(CapacitacionAsistencia A, Verificacion Verificacion, UsuarioSistema? Usuario);

    public static (string Texto, string Color, string Ayuda) Info(Verificacion v) => v switch
    {
        Verificacion.Coincide => ("Usuario verificado", "success", "El RUT y el correo coinciden con el mismo usuario del sistema."),
        Verificacion.RutCorreoDistinto => ("RUT coincide, correo distinto", "warning", "El RUT es de un usuario del sistema, pero registró otro correo."),
        Verificacion.SoloCorreo => ("Solo coincide el correo", "warning", "El correo es de un usuario del sistema, pero su RUT es otro o no tiene RUT cargado."),
        _ => ("No es usuario del sistema", "secondary", "Ni el RUT ni el correo corresponden a un usuario del sistema."),
    };

    public async Task<IActionResult> OnGetAsync(int id, CancellationToken ct)
    {
        if (!await CargarAsync(id, ct)) return NotFound();
        return Page();
    }

    public async Task<IActionResult> OnPostAlternarRegistroAsync(int id, CancellationToken ct)
    {
        var cap = await _db.Capacitaciones.FindAsync([id], ct);
        if (cap is null) return NotFound();

        cap.RegistroAbierto = !cap.RegistroAbierto;
        await _db.SaveChangesAsync(ct);
        await _audit.LogAsync(cap.RegistroAbierto ? "ABRIR_REGISTRO_CAPACITACION" : "CERRAR_REGISTRO_CAPACITACION",
            "Capacitacion", cap.Id.ToString(), userId: UsuarioId, userName: User.Identity?.Name);
        await _hub.AvisarCapacitacionCambiadaAsync(cap.Id, _logger);

        TempData["Success"] = cap.RegistroAbierto
            ? "El enlace vuelve a aceptar registros."
            : "Registro cerrado: el enlace ya no acepta nuevas asistencias.";
        return RedirectToPage(new { id });
    }

    public async Task<IActionResult> OnPostEliminarAsync(int id, int asistenciaId, CancellationToken ct)
    {
        var a = await _db.CapacitacionesAsistencia.FirstOrDefaultAsync(x => x.Id == asistenciaId && x.CapacitacionId == id, ct);
        if (a is null)
        {
            // Pudo eliminarlo otro administrador mientras el modal estaba abierto.
            TempData["Error"] = "Ese registro ya no existe: probablemente otro administrador lo eliminó.";
            return RedirectToPage(new { id });
        }

        _db.CapacitacionesAsistencia.Remove(a);
        await _db.SaveChangesAsync(ct);
        await _audit.LogAsync("ELIMINAR_ASISTENCIA_CAPACITACION", "CapacitacionAsistencia", a.Id.ToString(),
            oldValues: new { a.CapacitacionId, a.NombreCompleto, a.Rut, a.Email, a.FechaRegistro },
            userId: UsuarioId, userName: User.Identity?.Name);
        await _hub.AvisarCapacitacionCambiadaAsync(id, _logger);

        TempData["Success"] = $"Se eliminó el registro de {a.NombreCompleto}.";
        return RedirectToPage(new { id });
    }

    /// <summary>Texto que hay que escribir en el modal para confirmar la eliminación de la capacitación.</summary>
    public const string PalabraConfirmacion = "ELIMINAR";

    /// <summary>
    /// Elimina la capacitación y todos sus registros de asistencia (cascada). No se puede deshacer: el enlace
    /// deja de funcionar y queda un respaldo de lo eliminado en la auditoría (ELIMINAR_CAPACITACION).
    /// </summary>
    public async Task<IActionResult> OnPostEliminarCapacitacionAsync(int id, string? confirmacion, CancellationToken ct)
    {
        var cap = await _db.Capacitaciones.Include(c => c.Asistencias).FirstOrDefaultAsync(c => c.Id == id, ct);
        if (cap is null) return NotFound();
        if (!string.Equals(confirmacion?.Trim(), PalabraConfirmacion, StringComparison.OrdinalIgnoreCase))
        {
            TempData["Error"] = $"No se eliminó: para confirmar hay que escribir {PalabraConfirmacion}.";
            return RedirectToPage(new { id });
        }

        var respaldo = new
        {
            cap.Titulo, cap.Fecha, cap.Lugar, cap.Relator, cap.Descripcion, cap.CreadoPor, cap.CreatedAt,
            Asistentes = cap.Asistencias.OrderBy(a => a.NombreCompleto).Select(a => new
            {
                a.NombreCompleto, a.Rut, a.Email, a.Telefono, Empresa = a.Empresa?.Nombre, a.Sucursal, a.Cargo,
                a.AceptaAsistencia, a.FechaRegistro, a.IpAddress,
            }),
        };
        var asistentes = cap.Asistencias.Count;

        _db.Capacitaciones.Remove(cap);
        await _db.SaveChangesAsync(ct);
        await _audit.LogAsync("ELIMINAR_CAPACITACION", "Capacitacion", id.ToString(),
            oldValues: respaldo, userId: UsuarioId, userName: User.Identity?.Name);
        await _hub.AvisarCapacitacionCambiadaAsync(id, _logger, NotificationHub.CapacitacionEliminada);
        await _hub.AvisarCapacitacionCambiadaAsync(id, _logger);

        TempData["Success"] = $"Se eliminó la capacitación «{cap.Titulo}» y sus {asistentes} registro(s) de asistencia.";
        return RedirectToPage("Index");
    }

    /// <summary>Descarga la lista de asistentes con el resultado del cruce (CSV con ';' para Excel en español).</summary>
    public async Task<IActionResult> OnGetCsvAsync(int id, CancellationToken ct)
    {
        if (!await CargarAsync(id, ct)) return NotFound();

        var sb = new StringBuilder();
        sb.AppendLine("Fecha registro;Nombre;RUT;Correo;Teléfono;Empresa;Sucursal;Cargo;Acepta asistencia;Verificación;Usuario del sistema;Login");
        foreach (var f in Asistentes)
        {
            sb.AppendLine(string.Join(';', new[]
            {
                f.A.FechaRegistro.ToLocalTime().ToString("dd/MM/yyyy HH:mm"), f.A.NombreCompleto, FormatoRut(f.A.Rut), f.A.Email,
                f.A.Telefono, f.A.Empresa?.Nombre, f.A.Sucursal, f.A.Cargo, f.A.AceptaAsistencia ? "Sí" : "No", Info(f.Verificacion).Texto,
                f.Usuario?.Nombre, f.Usuario?.UserName,
            }.Select(Csv)));
        }

        var bytes = Encoding.UTF8.GetPreamble().Concat(Encoding.UTF8.GetBytes(sb.ToString())).ToArray();
        return File(bytes, "text/csv", $"Asistencia_{Capacitacion.Fecha:yyyyMMdd}_{Capacitacion.Id}.csv");
    }

    private async Task<bool> CargarAsync(int id, CancellationToken ct)
    {
        var cap = await _db.Capacitaciones.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id, ct);
        if (cap is null) return false;
        Capacitacion = cap;
        Enlace = $"{Request.Scheme}://{Request.Host}{Request.PathBase}/Asistencia?t={Uri.EscapeDataString(cap.Token)}";

        var asistencias = await _db.CapacitacionesAsistencia.AsNoTracking()
            .Where(a => a.CapacitacionId == id)
            .OrderBy(a => a.NombreCompleto)
            .ToListAsync(ct);

        var roles = (await (from ur in _db.UserRoles
                            join r in _db.Roles on ur.RoleId equals r.Id
                            select new { ur.UserId, r.Name }).ToListAsync(ct))
            .ToLookup(x => x.UserId, x => x.Name);
        var usuarios = (await _db.Users.AsNoTracking()
                .Select(u => new { u.Id, u.FullName, u.UserName, u.Rut, u.Email, u.IsActive })
                .ToListAsync(ct))
            .Select(u => new UsuarioSistema(u.Id, string.IsNullOrWhiteSpace(u.FullName) ? u.UserName ?? u.Id : u.FullName,
                u.UserName, u.Rut, u.Email, u.IsActive, string.Join(", ", roles[u.Id].OrderBy(n => n))))
            .ToList();

        // ApplicationUser.Rut ya está normalizado (RutHelper); el correo se compara sin mayúsculas.
        var porRut = usuarios.Where(u => !string.IsNullOrEmpty(u.Rut))
            .GroupBy(u => u.Rut!).ToDictionary(g => g.Key, g => g.First());
        var porEmail = usuarios.Where(u => !string.IsNullOrEmpty(u.Email))
            .GroupBy(u => u.Email!.Trim().ToLowerInvariant()).ToDictionary(g => g.Key, g => g.First());

        Asistentes = asistencias.Select(a =>
        {
            porRut.TryGetValue(a.Rut, out var uRut);
            porEmail.TryGetValue(a.Email.Trim().ToLowerInvariant(), out var uEmail);
            return uRut is not null
                ? new FilaAsistente(a, uEmail?.Id == uRut.Id ? Verificacion.Coincide : Verificacion.RutCorreoDistinto, uRut)
                : uEmail is not null
                    ? new FilaAsistente(a, Verificacion.SoloCorreo, uEmail)
                    : new FilaAsistente(a, Verificacion.NoEsUsuario, null);
        }).ToList();

        var encontrados = Asistentes.Where(f => f.Usuario is not null).Select(f => f.Usuario!.Id).ToHashSet();
        SinRegistro = usuarios.Where(u => u.Activo && !encontrados.Contains(u.Id)).OrderBy(u => u.Nombre).ToList();
        return true;
    }

    private string? UsuarioId => User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

    /// <summary>"123456789" -> "12.345.678-9" (solo para mostrar).</summary>
    public static string FormatoRut(string? rut)
    {
        if (string.IsNullOrEmpty(rut) || rut.Length < 2) return rut ?? string.Empty;
        var cuerpo = rut[..^1];
        return (long.TryParse(cuerpo, out var n) ? n.ToString("#,0", new System.Globalization.CultureInfo("es-CL")) : cuerpo) + "-" + rut[^1];
    }

    private static string Csv(string? s)
    {
        if (string.IsNullOrEmpty(s)) return string.Empty;
        // Evita que Excel interprete el texto como fórmula.
        if ("=+-@".Contains(s[0])) s = "'" + s;
        return s.IndexOfAny([';', '"', '\n', '\r']) >= 0 ? "\"" + s.Replace("\"", "\"\"") + "\"" : s;
    }
}
