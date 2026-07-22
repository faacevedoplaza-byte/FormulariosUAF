using System.ComponentModel.DataAnnotations;
using FormulariosUAF.Helpers;
using FormulariosUAF.Models.Domain;
using FormulariosUAF.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FormulariosUAF.Pages.Admin.Usuarios;

public class CrearModel : PageModel
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IAuditService _audit;

    public CrearModel(UserManager<ApplicationUser> userManager, IAuditService audit)
    {
        _userManager = userManager;
        _audit = audit;
    }

    [BindProperty] public InputModel Input { get; set; } = new();
    public string? ErrorMessage { get; set; }

    public class InputModel
    {
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
        public string Role { get; set; } = "Vendedor";

        [Required(ErrorMessage = "La contraseña es obligatoria")]
        [MinLength(4, ErrorMessage = "Mínimo 4 caracteres")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "Confirme la contraseña")]
        [Compare(nameof(Password), ErrorMessage = "Las contraseñas no coinciden")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; } = string.Empty;
    }

    public void OnGet() { }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid) return Page();

        var login = Input.Login.Trim();
        var rut = RutHelper.Normalizar(Input.Rut);
        if (!RutHelper.EsValido(rut))
        {
            ModelState.AddModelError("Input.Rut", "El RUT no es válido.");
            return Page();
        }

        if (await _userManager.FindByNameAsync(login) is not null)
        {
            ModelState.AddModelError("Input.Login", "Ya existe un usuario con ese login.");
            return Page();
        }

        var fullName = string.Join(" ",
            new[] { Input.Nombre, Input.ApellidoPaterno, Input.ApellidoMaterno }
                .Where(s => !string.IsNullOrWhiteSpace(s)));

        var user = new ApplicationUser
        {
            UserName = login,
            Email = Input.Email,
            Rut = rut,
            Nombre = Input.Nombre,
            ApellidoPaterno = Input.ApellidoPaterno,
            ApellidoMaterno = Input.ApellidoMaterno,
            FullName = fullName,
            EmailConfirmed = true,
            IsActive = true
        };

        var result = await _userManager.CreateAsync(user, Input.Password);
        if (!result.Succeeded)
        {
            ErrorMessage = string.Join("; ", result.Errors.Select(e => e.Description));
            return Page();
        }

        await _userManager.AddToRoleAsync(user, Input.Role);

        var adminId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
        await _audit.LogAsync("CREAR_USUARIO", "ApplicationUser", user.Id,
            newValues: new { user.UserName, user.Rut, user.Email, Input.Role },
            userId: adminId, userName: User.Identity?.Name);

        TempData["Success"] = $"Usuario {user.UserName} creado correctamente con rol {Input.Role}.";
        return RedirectToPage("/Admin/Usuarios/Index");
    }
}
