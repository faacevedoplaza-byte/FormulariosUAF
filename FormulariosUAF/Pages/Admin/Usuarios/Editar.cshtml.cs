using System.ComponentModel.DataAnnotations;
using FormulariosUAF.Models.Domain;
using FormulariosUAF.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FormulariosUAF.Pages.Admin.Usuarios;

public class EditarModel : PageModel
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IAuditService _audit;

    public EditarModel(UserManager<ApplicationUser> userManager, IAuditService audit)
    {
        _userManager = userManager;
        _audit = audit;
    }

    [BindProperty] public EditInput Input { get; set; } = new();
    public string? ErrorMessage { get; set; }

    public class EditInput
    {
        public string UserId { get; set; } = string.Empty;
        [Required] public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        [Required] public string Role { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;

        [MinLength(8, ErrorMessage = "Mínimo 8 caracteres")]
        [DataType(DataType.Password)]
        public string? NewPassword { get; set; }

        [Compare(nameof(NewPassword), ErrorMessage = "Las contraseñas no coinciden")]
        [DataType(DataType.Password)]
        public string? ConfirmNewPassword { get; set; }
    }

    public async Task<IActionResult> OnGetAsync(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user is null) return NotFound();

        var roles = await _userManager.GetRolesAsync(user);
        Input = new EditInput
        {
            UserId = user.Id,
            FullName = user.FullName,
            Email = user.Email ?? "",
            Role = roles.FirstOrDefault() ?? "Vendedor",
            IsActive = user.IsActive
        };
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid) return Page();

        var user = await _userManager.FindByIdAsync(Input.UserId);
        if (user is null) return NotFound();

        var oldRoles = await _userManager.GetRolesAsync(user);
        var adminId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

        user.FullName = Input.FullName;
        user.IsActive = Input.IsActive;
        await _userManager.UpdateAsync(user);

        // Actualizar rol
        if (!oldRoles.Contains(Input.Role))
        {
            await _userManager.RemoveFromRolesAsync(user, oldRoles);
            await _userManager.AddToRoleAsync(user, Input.Role);
        }

        // Cambiar contraseña si se proporcionó
        if (!string.IsNullOrEmpty(Input.NewPassword))
        {
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var result = await _userManager.ResetPasswordAsync(user, token, Input.NewPassword);
            if (!result.Succeeded)
            {
                ErrorMessage = string.Join("; ", result.Errors.Select(e => e.Description));
                return Page();
            }
        }

        await _audit.LogAsync("EDITAR_USUARIO", "ApplicationUser", user.Id,
            oldValues: new { Roles = oldRoles },
            newValues: new { Input.FullName, Input.Role, Input.IsActive },
            userId: adminId, userName: User.Identity?.Name);

        TempData["Success"] = $"Usuario {user.Email} actualizado correctamente.";
        return RedirectToPage("/Admin/Usuarios/Index");
    }
}
