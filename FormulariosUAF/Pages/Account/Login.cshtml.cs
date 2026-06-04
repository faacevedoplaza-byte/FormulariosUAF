using System.ComponentModel.DataAnnotations;
using FormulariosUAF.Models.Domain;
using FormulariosUAF.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FormulariosUAF.Pages.Account;

public class LoginModel : PageModel
{
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IAuditService _audit;

    public LoginModel(SignInManager<ApplicationUser> signInManager,
        UserManager<ApplicationUser> userManager, IAuditService audit)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _audit = audit;
    }

    [BindProperty]
    public InputModel Input { get; set; } = new();
    public string? ErrorMessage { get; set; }
    public string? ReturnUrl { get; set; }

    public class InputModel
    {
        [Required(ErrorMessage = "El correo es obligatorio")]
        [EmailAddress(ErrorMessage = "Correo inválido")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "La contraseña es obligatoria")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        public bool RememberMe { get; set; }
    }

    public void OnGet(string? returnUrl = null) => ReturnUrl = returnUrl;

    public async Task<IActionResult> OnPostAsync(string? returnUrl = null)
    {
        if (!ModelState.IsValid) return Page();

        var user = await _userManager.FindByEmailAsync(Input.Email);
        if (user is null || !user.IsActive)
        {
            ErrorMessage = "Credenciales incorrectas o usuario inactivo.";
            return Page();
        }

        var result = await _signInManager.PasswordSignInAsync(user, Input.Password, Input.RememberMe, lockoutOnFailure: true);

        if (result.Succeeded)
        {
            user.LastLoginAt = DateTime.UtcNow;
            await _userManager.UpdateAsync(user);
            await _audit.LogAsync("LOGIN", "ApplicationUser", user.Id, userId: user.Id, userName: user.Email);
            return LocalRedirect(returnUrl ?? "/Vendedor");
        }

        if (result.IsLockedOut)
            ErrorMessage = "Cuenta bloqueada temporalmente. Intente en 15 minutos.";
        else
            ErrorMessage = "Credenciales incorrectas.";

        return Page();
    }
}
