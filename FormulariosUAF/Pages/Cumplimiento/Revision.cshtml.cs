using FormulariosUAF.Models.Domain;
using FormulariosUAF.Models.Enums;
using FormulariosUAF.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FormulariosUAF.Pages.Cumplimiento;

public class RevisionModel : PageModel
{
    private readonly IRequestService _requestService;
    private readonly IPdfService _pdfService;
    private readonly IAuditService _audit;

    public RevisionModel(IRequestService requestService, IPdfService pdfService, IAuditService audit)
    {
        _requestService = requestService;
        _pdfService = pdfService;
        _audit = audit;
    }

    public new Request? Request { get; set; }

    public async Task<IActionResult> OnGetAsync(Guid id)
    {
        Request = await _requestService.GetByIdAsync(id);
        if (Request is null) return NotFound();

        if (Request.Status == RequestStatus.CompletadaPorCliente || Request.Status == RequestStatus.CorregidaPorCliente)
            await _requestService.UpdateStatusAsync(id, RequestStatus.EnRevision, User.Identity?.Name ?? "cumplimiento");

        Request = await _requestService.GetByIdAsync(id);
        return Page();
    }

    public async Task<IActionResult> OnPostAprobarAsync(Guid requestId, string? observacion)
    {
        await _requestService.UpdateStatusAsync(requestId, RequestStatus.Aprobada,
            User.Identity?.Name ?? "cumplimiento", observacion);
        await _audit.LogAsync("APROBAR", "Request", requestId.ToString(), requestId: requestId,
            userName: User.Identity?.Name);
        TempData["Success"] = "Solicitud aprobada.";
        return RedirectToPage(new { id = requestId });
    }

    public async Task<IActionResult> OnPostObservarAsync(Guid requestId, string? observacion)
    {
        await _requestService.UpdateStatusAsync(requestId, RequestStatus.Observada,
            User.Identity?.Name ?? "cumplimiento", observacion);
        await _audit.LogAsync("OBSERVAR", "Request", requestId.ToString(), requestId: requestId,
            newValues: new { observacion }, userName: User.Identity?.Name);
        TempData["Success"] = "Solicitud marcada como observada.";
        return RedirectToPage(new { id = requestId });
    }

    public async Task<IActionResult> OnPostRechazarAsync(Guid requestId, string? observacion)
    {
        await _requestService.UpdateStatusAsync(requestId, RequestStatus.Rechazada,
            User.Identity?.Name ?? "cumplimiento", observacion);
        await _audit.LogAsync("RECHAZAR", "Request", requestId.ToString(), requestId: requestId,
            newValues: new { observacion }, userName: User.Identity?.Name);
        TempData["Success"] = "Solicitud rechazada.";
        return RedirectToPage(new { id = requestId });
    }

    public async Task<IActionResult> OnGetDescargarPdfAsync(Guid id)
    {
        var request = await _requestService.GetByIdAsync(id);
        if (request is null) return NotFound();
        var pdf = _pdfService.GenerateConsolidatedPdf(request);
        return File(pdf, "application/pdf", $"declaracion_{request.RequestNumber}.pdf");
    }
}
