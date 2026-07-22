using System.ComponentModel.DataAnnotations;
using FormulariosUAF.Helpers;
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

        [Required(ErrorMessage = "El login es obligatorio")]
        [MaxLength(100)]
        [Display(Name = "Login")]
        public string Login { get; set; } = string.Empty;

        [Required(ErrorMessage = "El RUT es obligatorio")]
        [MaxLength(20)]
        public string Rut { get; set; } = string.Empty;

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [MaxLength(100)]
        public string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "El apellido paterno es obligatorio")]
        [MaxLength(100)]
        public string ApellidoPaterno { get; set; } = string.Empty;

        [MaxLength(100)]
        public string? ApellidoMaterno { get; set; }

        [Required(ErrorMessage = "El email es obligatorio")]
        [EmailAddress(ErrorMessage = "Email inválido")]
        [MaxLength(256)]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "El rol es obligatorio")]
        public string Role { get; set; } = string.Empty;

        public bool IsActive { get; set; } = true;

        [MinLength(4, ErrorMessage = "Mínimo 4 caracteres")]
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
            Login = user.UserName ?? "",
            Rut = user.Rut ?? "",
            Nombre = user.Nombre ?? "",
            ApellidoPaterno = user.ApellidoPaterno ?? "",
            ApellidoMaterno = user.ApellidoMaterno,
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

        var login = Input.Login.Trim();
        var rut = RutHelper.Normalizar(Input.Rut);
        if (!RutHelper.EsValido(rut))
        {
            ModelState.AddModelError("Input.Rut", "El RUT no es válido.");
            return Page();
        }

        var oldRoles = await _userManager.GetRolesAsync(user);
        var adminId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

        // Login (UserName): si cambió, validar unicidad y actualizar.
        if (!string.Equals(login, user.UserName, StringComparison.OrdinalIgnoreCase))
        {
            var existing = await _userManager.FindByNameAsync(login);
            if (existing is not null && existing.Id != user.Id)
            {
                ModelState.AddModelError("Input.Login", "Ya existe un usuario con ese login.");
                return Page();
            }
            var setName = await _userManager.SetUserNameAsync(user, login);
            if (!setName.Succeeded)
            {
                ErrorMessage = string.Join("; ", setName.Errors.Select(e => e.Description));
                return Page();
            }
        }

        // Email: si cambió, actualizar (queda confirmado).
        if (!string.Equals(Input.Email, user.Email, StringComparison.OrdinalIgnoreCase))
        {
            var setEmail = await _userManager.SetEmailAsync(user, Input.Email);
            if (!setEmail.Succeeded)
            {
                ErrorMessage = string.Join("; ", setEmail.Errors.Select(e => e.Description));
                return Page();
            }
            user.EmailConfirmed = true;
        }

        user.Rut = rut;
        user.Nombre = Input.Nombre;
        user.ApellidoPaterno = Input.ApellidoPaterno;
        user.ApellidoMaterno = Input.ApellidoMaterno;
        user.FullName = string.Join(" ",
            new[] { Input.Nombre, Input.ApellidoPaterno, Input.ApellidoMaterno }
                .Where(s => !string.IsNullOrWhiteSpace(s)));
        user.IsActive = Input.IsActive;
        await _userManager.UpdateAsync(user);

        // Rol
        if (!oldRoles.Contains(Input.Role))
        {
            await _userManager.RemoveFromRolesAsync(user, oldRoles);
            await _userManager.AddToRoleAsync(user, Input.Role);
        }

        // Contraseña (solo si se ingresó)
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
            newValues: new { user.UserName, user.Rut, user.Email, Input.Role, Input.IsActive },
            userId: adminId, userName: User.Identity?.Name);

        TempData["Success"] = $"Usuario {user.UserName} actualizado correctamente.";
        return RedirectToPage("/Admin/Usuarios/Index");
    }
}
