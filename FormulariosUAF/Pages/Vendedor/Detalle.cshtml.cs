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
    private readonly IEmailService _email;
    private readonly IAuditService _audit;
    private readonly IConfiguration _config;

    public DetalleModel(IRequestService requestService, IPdfService pdfService,
        IEmailService email, IAuditService audit, IConfiguration config)
    {
        _requestService = requestService;
        _pdfService = pdfService;
        _email = email;
        _audit = audit;
        _config = config;
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

    /// <summary>
    /// Envía el enlace seguro al email del cliente.
    /// Requiere que el email del cliente esté disponible en LegalEntityDeclaration o Client.
    /// </summary>
    public async Task<IActionResult> OnPostEnviarEmailAsync(Guid id, string? clientEmail)
    {
        var request = await _requestService.GetByIdAsync(id);
        if (request is null) return NotFound();

        if (string.IsNullOrEmpty(clientEmail))
        {
            TempData["Error"] = "Debe ingresar el email del cliente.";
            return RedirectToPage(new { id });
        }

        var baseUrl = _config["AppSettings:BaseUrl"] ?? $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}";
        var link = $"{baseUrl}/Cliente/Inicio?token={request.ClientToken}";
        var vendorName = User.Identity?.Name ?? "Ejecutivo";

        try
        {
            await _email.SendClientInvitationAsync(new ClientInvitationEmail(
                ToEmail: clientEmail,
                ClientName: request.Client.BusinessName,
                VendorName: vendorName,
                VendorEmail: User.Identity?.Name ?? "",
                SecureLink: link,
                ExpirationDate: request.TokenExpiry,
                RequestNumber: request.RequestNumber,
                SupportContact: _config["AppSettings:SupportEmail"]
            ));

            await _requestService.UpdateStatusAsync(id, RequestStatus.EnviadaAlCliente, vendorName);
            await _audit.LogAsync("ENVIAR_EMAIL_CLIENTE", "Request", id.ToString(),
                newValues: new { clientEmail }, requestId: id, userName: vendorName);

            TempData["Success"] = $"Email enviado a {clientEmail} y solicitud marcada como enviada.";
        }
        catch (Exception ex)
        {
            TempData["Error"] = $"No se pudo enviar el email: {ex.Message}. Puede copiar el enlace manualmente.";
        }

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
