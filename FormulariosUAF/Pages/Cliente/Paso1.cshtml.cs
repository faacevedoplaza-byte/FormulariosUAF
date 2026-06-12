using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FormulariosUAF.Pages.Cliente;

public class Paso1Model : PageModel
{
    public IActionResult OnGet() => RedirectToPage("/Cliente/Declaracion");
    public IActionResult OnPost() => RedirectToPage("/Cliente/Declaracion");
}
