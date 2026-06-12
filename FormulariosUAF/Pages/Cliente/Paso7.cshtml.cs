using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FormulariosUAF.Pages.Cliente;

public class Paso7Model : PageModel
{
    public IActionResult OnGet() => RedirectToPage("/Cliente/Envio");
    public IActionResult OnPost() => RedirectToPage("/Cliente/Envio");
}
