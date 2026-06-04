using FormulariosUAF.Data;
using FormulariosUAF.Models.Domain;
using FormulariosUAF.Models.Enums;
using FormulariosUAF.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace FormulariosUAF.Pages.Cliente;

public class Paso6Model : PageModel
{
    private readonly ApplicationDbContext _db;
    private readonly IFileStorageService _fileStorage;
    private readonly IAuditService _audit;
    private readonly IConfiguration _config;

    private static readonly string[] AllowedExtensions = [".pdf", ".jpg", ".jpeg", ".png", ".docx", ".xlsx"];
    private static readonly string[] AllowedMimeTypes =
    [
        "application/pdf", "image/jpeg", "image/png",
        "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
    ];

    public Paso6Model(ApplicationDbContext db, IFileStorageService fileStorage, IAuditService audit, IConfiguration config)
    {
        _db = db; _fileStorage = fileStorage; _audit = audit; _config = config;
    }

    public List<Document> ExistingDocs { get; set; } = [];
    public bool HasCarpetaTributaria { get; set; }
    public string? ErrorMessage { get; set; }
    public string? SuccessMessage { get; set; }

    public async Task<IActionResult> OnGetAsync()
    {
        var requestId = GetRequestId();
        if (requestId == Guid.Empty) return RedirectToPage("/Cliente/Inicio");

        ExistingDocs = await _db.Documents
            .Where(d => d.RequestId == requestId && d.IsActive)
            .ToListAsync();
        HasCarpetaTributaria = ExistingDocs.Any(d => d.DocumentType == DocumentType.CarpetaTributaria);
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(
        IFormFile? carpetaTributaria,
        IFormFile? cedulaRepresentante,
        IFormFile? escrituras,
        IList<IFormFile>? otrosDocumentos)
    {
        var requestId = GetRequestId();
        if (requestId == Guid.Empty) return RedirectToPage("/Cliente/Inicio");

        var req = await _db.Requests.Include(r => r.Client).FirstOrDefaultAsync(r => r.Id == requestId);
        if (req is null) return RedirectToPage("/Cliente/Inicio");

        var maxMb = _config.GetValue<int>("FileStorage:MaxFileSizeMB", 20);
        var ip = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "unknown";
        var uploads = new List<(IFormFile file, DocumentType type)>();

        if (carpetaTributaria is not null) uploads.Add((carpetaTributaria, DocumentType.CarpetaTributaria));
        if (cedulaRepresentante is not null) uploads.Add((cedulaRepresentante, DocumentType.CedulaRepresentanteLegal));
        if (escrituras is not null) uploads.Add((escrituras, DocumentType.EscriturasOPoderes));
        if (otrosDocumentos is not null)
            foreach (var f in otrosDocumentos)
                uploads.Add((f, DocumentType.OtroDocumento));

        foreach (var (file, docType) in uploads)
        {
            var ext = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (!AllowedExtensions.Contains(ext))
            {
                ErrorMessage = $"Formato no permitido: {file.FileName}";
                await LoadExistingDocs(requestId);
                return Page();
            }
            if (!AllowedMimeTypes.Contains(file.ContentType))
            {
                ErrorMessage = $"Tipo MIME no permitido: {file.ContentType}";
                await LoadExistingDocs(requestId);
                return Page();
            }
            if (file.Length > maxMb * 1024 * 1024)
            {
                ErrorMessage = $"Archivo demasiado grande: {file.FileName} (máx. {maxMb} MB)";
                await LoadExistingDocs(requestId);
                return Page();
            }

            // Mark old version inactive
            var oldDocs = await _db.Documents
                .Where(d => d.RequestId == requestId && d.DocumentType == docType && d.IsActive)
                .ToListAsync();
            var version = oldDocs.Any() ? oldDocs.Max(d => d.Version) + 1 : 1;
            oldDocs.ForEach(d => d.IsActive = false);

            var stored = await _fileStorage.SaveFileAsync(file, req.Client.RUT, requestId.ToString(), docType.ToString());

            var doc = new Document
            {
                RequestId = requestId,
                DocumentType = docType,
                OriginalFileName = file.FileName,
                StoredFileName = stored.StoredFileName,
                StoragePath = stored.StoragePath,
                MimeType = file.ContentType,
                FileSizeBytes = file.Length,
                IsActive = true,
                Version = version,
                UploadedAt = DateTime.UtcNow,
                UploadedByIp = ip
            };
            _db.Documents.Add(doc);
        }

        if (req.CurrentStep < 7) req.CurrentStep = 7;
        await _db.SaveChangesAsync();
        await _audit.LogAsync("GUARDAR_PASO6", "Documents", requestId.ToString(), requestId: requestId);

        return RedirectToPage("/Cliente/Paso7");
    }

    private async Task LoadExistingDocs(Guid requestId)
    {
        ExistingDocs = await _db.Documents.Where(d => d.RequestId == requestId && d.IsActive).ToListAsync();
        HasCarpetaTributaria = ExistingDocs.Any(d => d.DocumentType == DocumentType.CarpetaTributaria);
    }

    private Guid GetRequestId()
    {
        var val = HttpContext.Session.GetString("ClientRequestId");
        return Guid.TryParse(val, out var id) ? id : Guid.Empty;
    }
}
