using FormulariosUAF.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FormulariosUAF.Pages.Vendedor;

public class DescargarPdfModel : PageModel
{
    private readonly IRequestService _requestService;
    private readonly IPdfService _pdfService;

    public DescargarPdfModel(IRequestService requestService, IPdfService pdfService)
    {
        _requestService = requestService;
        _pdfService = pdfService;
    }

    public async Task<IActionResult> OnGetAsync(Guid id)
    {
        var request = await _requestService.GetByIdAsync(id);
        if (request is null) return NotFound();
        var pdf = _pdfService.GenerateConsolidatedPdf(request);
        return File(pdf, "application/pdf", $"declaracion_{request.RequestNumber}.pdf");
    }
}
