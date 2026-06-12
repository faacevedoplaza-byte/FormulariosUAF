using FormulariosUAF.Data;
using FormulariosUAF.Models.Domain;
using FormulariosUAF.Models.Enums;
using FormulariosUAF.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace FormulariosUAF.Pages.Cliente;

public class EnvioModel : PageModel
{
    private readonly ApplicationDbContext _db;
    private readonly IRequestService _requestService;
    private readonly IPdfService _pdfService;
    private readonly IFileStorageService _fileStorage;
    private readonly IAuditService _audit;

    public EnvioModel(ApplicationDbContext db, IRequestService requestService,
        IPdfService pdfService, IFileStorageService fileStorage, IAuditService audit)
    {
        _db = db; _requestService = requestService;
        _pdfService = pdfService; _fileStorage = fileStorage; _audit = audit;
    }

    public new Request? Request { get; set; }
    public List<TaxFolderAlert> TaxAlerts { get; set; } = [];
    public List<string> ValidationErrors { get; set; } = [];
    public bool IsComplete => !ValidationErrors.Any();

    public async Task<IActionResult> OnGetAsync()
    {
        var requestId = GetRequestId();
        if (requestId == Guid.Empty) return RedirectToPage("/Cliente/Inicio");

        await LoadRequestAsync(requestId);
        if (Request is null) return RedirectToPage("/Cliente/Inicio");
        Validate();
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        var requestId = GetRequestId();
        if (requestId == Guid.Empty) return RedirectToPage("/Cliente/Inicio");

        await LoadRequestAsync(requestId);
        if (Request is null) return RedirectToPage("/Cliente/Inicio");

        Validate();
        if (!IsComplete) return Page();

        // Generar PDF consolidado
        try
        {
            var pdf = _pdfService.GenerateConsolidatedPdf(Request);
            var dir = Path.Combine("Repository", "Clientes", Request.Client.RUT, requestId.ToString());
            Directory.CreateDirectory(dir);
            await System.IO.File.WriteAllBytesAsync(Path.Combine(dir, "declaracion_consolidada.pdf"), pdf);
        }
        catch { /* no bloquear el envío */ }

        await _requestService.UpdateStatusAsync(requestId, RequestStatus.CompletadaPorCliente,
            "cliente", "Declaración completada por el cliente");
        await _audit.LogAsync("ENVIAR_DECLARACION", "Request", requestId.ToString(), requestId: requestId);

        HttpContext.Session.Remove("ClientRequestId");
        HttpContext.Session.Remove("ClientToken");

        return RedirectToPage("/Cliente/Completado");
    }

    private async Task LoadRequestAsync(Guid requestId)
    {
        Request = await _db.Requests
            .Include(r => r.Client)
            .Include(r => r.LegalEntityDeclaration)
            .Include(r => r.DeclaredPersons.OrderBy(d => d.SortOrder))
            .Include(r => r.Declarant)
            .Include(r => r.Documents.Where(d => d.IsActive))
            .Include(r => r.TaxFolderAnalysis!).ThenInclude(t => t.Alerts)
            .FirstOrDefaultAsync(r => r.Id == requestId);

        TaxAlerts = Request?.TaxFolderAnalysis?.Alerts.Where(a => !a.IsResolved).ToList() ?? [];
    }

    private void Validate()
    {
        if (Request is null) return;
        if (Request.LegalEntityDeclaration is null)
            ValidationErrors.Add("Faltan los datos de la empresa.");
        if (!Request.DeclaredPersons.Any())
            ValidationErrors.Add("No se ingresaron personas declaradas. Vuelva a la declaración.");
        if (Request.Declarant is null)
            ValidationErrors.Add("Faltan los datos del declarante.");
        if (!Request.Documents.Any(d => d.IsActive && d.DocumentType == DocumentType.CarpetaTributaria))
            ValidationErrors.Add("Debe adjuntar la Carpeta Tributaria.");
    }

    private Guid GetRequestId()
    {
        var val = HttpContext.Session.GetString("ClientRequestId");
        return Guid.TryParse(val, out var id) ? id : Guid.Empty;
    }
}
