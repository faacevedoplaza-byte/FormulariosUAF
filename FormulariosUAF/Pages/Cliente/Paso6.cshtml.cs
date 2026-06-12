using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FormulariosUAF.Pages.Cliente;

public class Paso6Model : PageModel
{
    public IActionResult OnGet() => RedirectToPage("/Cliente/Adjuntos");
    public IActionResult OnPost() => RedirectToPage("/Cliente/Adjuntos");
}
