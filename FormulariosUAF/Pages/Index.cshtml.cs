using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FormulariosUAF.Pages;

public class IndexModel : PageModel
{
    public IActionResult OnGet()
    {
        if (User.Identity?.IsAuthenticated != true)
            return Page();

        // Cada rol a su propia área (Cumplimiento/Revisor NO pueden abrir /Vendedor).
        if (User.IsInRole("Administrador")) return RedirectToPage("/Admin/Index");
        if (User.IsInRole("Vendedor"))      return RedirectToPage("/Vendedor/Index");
        if (User.IsInRole("Cumplimiento") || User.IsInRole("Revisor"))
            return RedirectToPage("/Cumplimiento/Index");

        // SoloLectura u otros roles sin área propia.
        return RedirectToPage("/Interno/Notificaciones/Index");
    }
}
