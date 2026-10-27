using FormulariosUAF.Data;
using FormulariosUAF.Models.Domain;
using FormulariosUAF.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace FormulariosUAF.Pages.Admin.Usuarios;

public class IndexModel : PageModel
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ApplicationDbContext _db;
    private readonly IAuditService _audit;

    public IndexModel(UserManager<ApplicationUser> userManager, ApplicationDbContext db, IAuditService audit)
    {
        _userManager = userManager;
        _db = db;
        _audit = audit;
    }

    public List<UserListItem> Users { get; set; } = [];

    // Filtros (se aplican en el navegador; vienen en la URL para poder compartirlos y conservarlos tras activar/desactivar)
    [BindProperty(SupportsGet = true)] public string? Q { get; set; }
    [BindProperty(SupportsGet = true)] public string? Rol { get; set; }
    [BindProperty(SupportsGet = true)] public string? Estado { get; set; }
    [BindProperty(SupportsGet = true)] public string? Acceso { get; set; }

    public async Task OnGetAsync()
    {
        var users = await _userManager.Users
            .OrderBy(u => u.FullName)
            .ToListAsync();

        // Una sola consulta para los roles de todos (antes era GetRolesAsync por usuario)
        var rolesPorUsuario = (await (
                from ur in _db.UserRoles
                join r in _db.Roles on ur.RoleId equals r.Id
                select new { ur.UserId, r.Name }
            ).ToListAsync())
            .GroupBy(x => x.UserId)
            .ToDictionary(g => g.Key, g => g.Select(x => x.Name ?? "").OrderBy(n => n).ToList());

        Users = users.Select(u => new UserListItem(
            u.Id, u.FullName, u.Email ?? "", u.UserName ?? "", u.Rut, u.VendorCode, u.PhoneNumber,
            rolesPorUsuario.GetValueOrDefault(u.Id) ?? [],
            u.IsActive, u.LastLoginAt
        )).ToList();
    }

    public async Task<IActionResult> OnPostToggleActiveAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user is null) return NotFound();

        var adminId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
        if (adminId == userId)
        {
            TempData["Error"] = "No puedes desactivar tu propio usuario.";
            return RedirectConFiltros();
        }

        user.IsActive = !user.IsActive;
        await _userManager.UpdateAsync(user);

        var action = user.IsActive ? "ACTIVAR_USUARIO" : "DESACTIVAR_USUARIO";
        await _audit.LogAsync(action, "ApplicationUser", userId,
            userId: adminId, userName: User.Identity?.Name);

        TempData["Success"] = $"Usuario {user.Email} {(user.IsActive ? "activado" : "desactivado")}.";
        return RedirectConFiltros();
    }

    private IActionResult RedirectConFiltros() => RedirectToPage(new
    {
        q = string.IsNullOrWhiteSpace(Q) ? null : Q,
        rol = string.IsNullOrWhiteSpace(Rol) ? null : Rol,
        estado = string.IsNullOrWhiteSpace(Estado) ? null : Estado,
        acceso = string.IsNullOrWhiteSpace(Acceso) ? null : Acceso
    });
}

public record UserListItem(
    string Id, string FullName, string Email, string UserName, string? Rut, string? VendorCode, string? Phone,
    List<string> Roles, bool IsActive, DateTime? LastLoginAt
);
