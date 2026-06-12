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
    private readonly IExpedienteExportService _export;
    private readonly INotificationService _notifications;

    public RevisionModel(IRequestService requestService, IPdfService pdfService,
        IAuditService audit, IExpedienteExportService export,
        INotificationService notifications)
    {
        _requestService = requestService;
        _pdfService = pdfService;
        _audit = audit;
        _export = export;
        _notifications = notifications;
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
        var userName = User.Identity?.Name ?? "cumplimiento";
        await _requestService.UpdateStatusAsync(requestId, RequestStatus.Aprobada, userName, observacion);
        await _audit.LogAsync("APROBAR", "Request", requestId.ToString(), requestId: requestId, userName: userName);

        // Notificar al vendedor
        var req = await _requestService.GetByIdAsync(requestId);
        if (req is not null)
            await _notifications.CreateAsync(req.VendorUserId, NotificationType.SolicitudAprobada,
                $"Solicitud {req.RequestNumber} aprobada.", requestId);

        TempData["Success"] = "Solicitud aprobada.";
        return RedirectToPage(new { id = requestId });
    }

    public async Task<IActionResult> OnPostObservarAsync(Guid requestId, string? observacion)
    {
        var userName = User.Identity?.Name ?? "cumplimiento";
        await _requestService.UpdateStatusAsync(requestId, RequestStatus.Observada, userName, observacion);
        await _audit.LogAsync("OBSERVAR", "Request", requestId.ToString(), requestId: requestId,
            newValues: new { observacion }, userName: userName);

        var req = await _requestService.GetByIdAsync(requestId);
        if (req is not null)
            await _notifications.CreateAsync(req.VendorUserId, NotificationType.SolicitudObservada,
                $"Solicitud {req.RequestNumber} observada: {observacion}", requestId);

        TempData["Success"] = "Solicitud marcada como observada.";
        return RedirectToPage(new { id = requestId });
    }

    public async Task<IActionResult> OnPostRechazarAsync(Guid requestId, string? observacion)
    {
        var userName = User.Identity?.Name ?? "cumplimiento";
        await _requestService.UpdateStatusAsync(requestId, RequestStatus.Rechazada, userName, observacion);
        await _audit.LogAsync("RECHAZAR", "Request", requestId.ToString(), requestId: requestId,
            newValues: new { observacion }, userName: userName);

        var req = await _requestService.GetByIdAsync(requestId);
        if (req is not null)
            await _notifications.CreateAsync(req.VendorUserId, NotificationType.SolicitudRechazada,
                $"Solicitud {req.RequestNumber} rechazada: {observacion}", requestId);

        TempData["Success"] = "Solicitud rechazada.";
        return RedirectToPage(new { id = requestId });
    }

    public async Task<IActionResult> OnPostSolicitarCorreccionAsync(Guid requestId, string? observacion)
    {
        var userName = User.Identity?.Name ?? "cumplimiento";
        await _requestService.UpdateStatusAsync(requestId, RequestStatus.CorreccionSolicitada, userName, observacion);
        await _audit.LogAsync("SOLICITAR_CORRECCION", "Request", requestId.ToString(), requestId: requestId,
            newValues: new { observacion }, userName: userName);

        var req = await _requestService.GetByIdAsync(requestId);
        if (req is not null)
            await _notifications.CreateAsync(req.VendorUserId, NotificationType.SolicitudObservada,
                $"Solicitud {req.RequestNumber} requiere corrección: {observacion}", requestId);

        TempData["Success"] = "Se solicitó corrección al cliente.";
        return RedirectToPage(new { id = requestId });
    }

    public async Task<IActionResult> OnGetDescargarPdfAsync(Guid id)
    {
        var request = await _requestService.GetByIdAsync(id);
        if (request is null) return NotFound();
        var pdf = _pdfService.GenerateConsolidatedPdf(request);
        return File(pdf, "application/pdf", $"declaracion_{request.RequestNumber}.pdf");
    }

    public async Task<IActionResult> OnGetExportarZipAsync(Guid id)
    {
        var request = await _requestService.GetByIdAsync(id);
        if (request is null) return NotFound();

        var zip = await _export.ExportarExpedienteZipAsync(id);
        var filename = _export.GetNombreArchivo(request.Client.RUT, id);
        return File(zip, "application/zip", filename);
    }
}
