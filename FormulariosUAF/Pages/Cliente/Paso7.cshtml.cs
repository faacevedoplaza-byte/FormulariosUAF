using FormulariosUAF.Data;
using FormulariosUAF.Models.Domain;
using FormulariosUAF.Models.Enums;
using FormulariosUAF.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace FormulariosUAF.Pages.Cliente;

public class Paso7Model : PageModel
{
    private readonly ApplicationDbContext _db;
    private readonly IRequestService _requestService;
    private readonly IPdfService _pdfService;
    private readonly IFileStorageService _fileStorage;
    private readonly IAuditService _audit;

    public Paso7Model(ApplicationDbContext db, IRequestService requestService,
        IPdfService pdfService, IFileStorageService fileStorage, IAuditService audit)
    {
        _db = db; _requestService = requestService;
        _pdfService = pdfService; _fileStorage = fileStorage; _audit = audit;
    }

    public new Request? Request { get; set; }
    public List<string> ValidationErrors { get; set; } = [];
    public bool IsComplete => !ValidationErrors.Any();

    public async Task<IActionResult> OnGetAsync()
    {
        var requestId = GetRequestId();
        if (requestId == Guid.Empty) return RedirectToPage("/Cliente/Inicio");

        Request = await _requestService.GetByIdAsync(requestId);
        if (Request is null) return RedirectToPage("/Cliente/Inicio");

        Validate();
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        var requestId = GetRequestId();
        if (requestId == Guid.Empty) return RedirectToPage("/Cliente/Inicio");

        Request = await _requestService.GetByIdAsync(requestId);
        if (Request is null) return RedirectToPage("/Cliente/Inicio");

        Validate();
        if (!IsComplete) return Page();

        // Generate and save PDFs
        try
        {
            var rutCliente = Request.Client.RUT;
            var idStr = requestId.ToString();

            var pdf1 = _pdfService.GenerateBeneficialOwnerDeclarationPdf(Request);
            await SaveGeneratedPdf(pdf1, rutCliente, idStr, "declaracion_beneficiario_final.pdf");

            var pdf2 = _pdfService.GeneratePepDeclarationPdf(Request);
            await SaveGeneratedPdf(pdf2, rutCliente, idStr, "declaracion_pep.pdf");
        }
        catch { /* log but don't fail the submission */ }

        await _requestService.UpdateStatusAsync(requestId, RequestStatus.CompletadaPorCliente,
            "cliente", "Declaración completada por el cliente");

        await _audit.LogAsync("ENVIAR_DECLARACION", "Request", requestId.ToString(), requestId: requestId);

        HttpContext.Session.Remove("ClientRequestId");
        HttpContext.Session.Remove("ClientToken");

        return RedirectToPage("/Cliente/Completado");
    }

    private async Task SaveGeneratedPdf(byte[] pdfBytes, string rutCliente, string solicitudId, string fileName)
    {
        var dirPath = Path.Combine("Repository", "Clientes", rutCliente, solicitudId);
        Directory.CreateDirectory(dirPath);
        var fullPath = Path.Combine(dirPath, fileName);
        await System.IO.File.WriteAllBytesAsync(fullPath, pdfBytes);
    }

    private void Validate()
    {
        if (Request is null) return;
        if (Request.LegalEntityDeclaration is null)
            ValidationErrors.Add("Falta completar los datos de la empresa (Paso 1).");
        if (!Request.BeneficialOwners.Any())
            ValidationErrors.Add("Debe agregar al menos un beneficiario final (Paso 2).");
        if (Request.PepDeclaration is null)
            ValidationErrors.Add("Falta la declaración PEP (Paso 4).");
        if (Request.Declarant is null)
            ValidationErrors.Add("Faltan los datos del declarante (Paso 5).");
        if (!Request.Documents.Any(d => d.IsActive && d.DocumentType == DocumentType.CarpetaTributaria))
            ValidationErrors.Add("Debe adjuntar la Carpeta Tributaria (Paso 6).");
    }

    private Guid GetRequestId()
    {
        var val = HttpContext.Session.GetString("ClientRequestId");
        return Guid.TryParse(val, out var id) ? id : Guid.Empty;
    }
}
