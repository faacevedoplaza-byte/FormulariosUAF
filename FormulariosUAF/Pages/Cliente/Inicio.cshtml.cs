using FormulariosUAF.Models.Enums;
using FormulariosUAF.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FormulariosUAF.Pages.Cliente;

public class InicioModel : PageModel
{
    private readonly IRequestService _requestService;
    private readonly IAuditService _audit;

    public InicioModel(IRequestService requestService, IAuditService audit)
    {
        _requestService = requestService;
        _audit = audit;
    }

    [BindProperty(SupportsGet = true)]
    public string Token { get; set; } = string.Empty;

    public RequestInfo? RequestInfo { get; set; }
    public string? ErrorMessage { get; set; }

    public async Task OnGetAsync()
    {
        if (string.IsNullOrEmpty(Token))
        {
            ErrorMessage = "Token no proporcionado.";
            return;
        }

        var request = await _requestService.GetByTokenAsync(Token);
        if (request is null)
        {
            ErrorMessage = "El enlace no es válido o no existe.";
            return;
        }
        if (request.TokenExpiry < DateTime.UtcNow)
        {
            ErrorMessage = "El enlace ha vencido. Solicita uno nuevo a tu ejecutivo.";
            return;
        }
        if (request.Status == RequestStatus.Aprobada || request.Status == RequestStatus.Rechazada)
        {
            ErrorMessage = "Esta declaración ya fue procesada.";
            return;
        }

        RequestInfo = new RequestInfo(request.Client.RUT, request.Client.BusinessName, request.TokenExpiry);
    }

    public async Task<IActionResult> OnPostAsync()
    {
        var request = await _requestService.GetByTokenAsync(Token);
        if (request is null || request.TokenExpiry < DateTime.UtcNow)
            return RedirectToPage(new { token = Token });

        HttpContext.Session.SetString("ClientRequestId", request.Id.ToString());
        HttpContext.Session.SetString("ClientToken", Token);

        if (request.Status == RequestStatus.EnviadaAlCliente)
            await _requestService.UpdateStatusAsync(request.Id, RequestStatus.AbiertaPorCliente, "cliente");

        await _audit.LogAsync("ABRIR_ENLACE", "Request", request.Id.ToString(), requestId: request.Id);

        return RedirectToPage("/Cliente/Paso1");
    }
}

public record RequestInfo(string ClientRut, string ClientRazonSocial, DateTime Expiry);
