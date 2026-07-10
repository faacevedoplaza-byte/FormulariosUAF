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

            // Si venía de un enlace protegido (returnUrl local y seguro), respétalo.
            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                return LocalRedirect(returnUrl);

            // Si no, cada rol a su propia área.
            return LocalRedirect(await LandingPageForAsync(user));
        }

        if (result.IsLockedOut)
            ErrorMessage = "Cuenta bloqueada temporalmente. Intente en 15 minutos.";
        else
            ErrorMessage = "Credenciales incorrectas.";

        return Page();
    }

    // Área de inicio según el rol (evita mandar a todos a /Vendedor,
    // que Cumplimiento/Revisor no pueden abrir → acceso denegado).
    private async Task<string> LandingPageForAsync(ApplicationUser user)
    {
        if (await _userManager.IsInRoleAsync(user, "Administrador")) return "/Admin";
        if (await _userManager.IsInRoleAsync(user, "Vendedor"))      return "/Vendedor";
        if (await _userManager.IsInRoleAsync(user, "Cumplimiento"))  return "/Cumplimiento";
        if (await _userManager.IsInRoleAsync(user, "Revisor"))       return "/Cumplimiento";
        // SoloLectura u otros roles sin área propia.
        return "/Interno/Notificaciones";
    }
}
