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
    private readonly IAuditService _audit;

    public IndexModel(UserManager<ApplicationUser> userManager, IAuditService audit)
    {
        _userManager = userManager;
        _audit = audit;
    }

    public List<UserListItem> Users { get; set; } = [];

    public async Task OnGetAsync()
    {
        var users = await _userManager.Users
            .OrderBy(u => u.FullName)
            .ToListAsync();

        Users = [];
        foreach (var u in users)
        {
            var roles = await _userManager.GetRolesAsync(u);
            Users.Add(new UserListItem(
                u.Id, u.FullName, u.Email ?? "", [.. roles],
                u.IsActive, u.LastLoginAt
            ));
        }
    }

    public async Task<IActionResult> OnPostToggleActiveAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user is null) return NotFound();

        var adminId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
        if (adminId == userId)
        {
            TempData["Error"] = "No puedes desactivar tu propio usuario.";
            return RedirectToPage();
        }

        user.IsActive = !user.IsActive;
        await _userManager.UpdateAsync(user);

        var action = user.IsActive ? "ACTIVAR_USUARIO" : "DESACTIVAR_USUARIO";
        await _audit.LogAsync(action, "ApplicationUser", userId,
            userId: adminId, userName: User.Identity?.Name);

        TempData["Success"] = $"Usuario {user.Email} {(user.IsActive ? "activado" : "desactivado")}.";
        return RedirectToPage();
    }
}

public record UserListItem(
    string Id, string FullName, string Email,
    List<string> Roles, bool IsActive, DateTime? LastLoginAt
);
