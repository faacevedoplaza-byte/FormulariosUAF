using FormulariosUAF.Models.Domain;
using FormulariosUAF.Models.Enums;
using FormulariosUAF.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FormulariosUAF.Pages.Vendedor;

public class DetalleModel : PageModel
{
    private readonly IRequestService _requestService;
    private readonly IPdfService _pdfService;

    public DetalleModel(IRequestService requestService, IPdfService pdfService)
    {
        _requestService = requestService;
        _pdfService = pdfService;
    }

    public new Request? Request { get; set; }

    public async Task<IActionResult> OnGetAsync(Guid id)
    {
        Request = await _requestService.GetByIdAsync(id);
        if (Request is null) return NotFound();
        return Page();
    }

    public async Task<IActionResult> OnPostEnviarAsync(Guid id)
    {
        var userName = User.Identity?.Name ?? "sistema";
        await _requestService.UpdateStatusAsync(id, RequestStatus.EnviadaAlCliente, userName);
        TempData["Success"] = "Solicitud enviada al cliente.";
        return RedirectToPage(new { id });
    }

    public async Task<IActionResult> OnGetDescargarPdfAsync(Guid id)
    {
        var request = await _requestService.GetByIdAsync(id);
        if (request is null) return NotFound();
        var pdf = _pdfService.GenerateConsolidatedPdf(request);
        return File(pdf, "application/pdf", $"declaracion_{request.RequestNumber}.pdf");
    }
}
