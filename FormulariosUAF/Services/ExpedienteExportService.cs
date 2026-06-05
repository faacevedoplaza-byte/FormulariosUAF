using System.IO.Compression;
using System.Text;
using System.Text.Json;
using FormulariosUAF.Data;
using FormulariosUAF.Extensions;
using Microsoft.EntityFrameworkCore;

namespace FormulariosUAF.Services;

public class ExpedienteExportService : IExpedienteExportService
{
    private readonly IRequestService _requestService;
    private readonly IPdfService _pdfService;
    private readonly IFileStorageService _fileStorage;
    private readonly ApplicationDbContext _db;
    private readonly ILogger<ExpedienteExportService> _logger;

    public ExpedienteExportService(
        IRequestService requestService, IPdfService pdfService,
        IFileStorageService fileStorage, ApplicationDbContext db,
        ILogger<ExpedienteExportService> logger)
    {
        _requestService = requestService;
        _pdfService = pdfService;
        _fileStorage = fileStorage;
        _db = db;
        _logger = logger;
    }

    public string GetNombreArchivo(string rutCliente, Guid requestId)
    {
        var fecha = DateTime.Now.ToString("yyyyMMdd");
        var rut = rutCliente.Replace(".", "").Replace("-", "");
        return $"EXPEDIENTE_{rut}_{requestId.ToString()[..8].ToUpper()}_{fecha}.zip";
    }

    public async Task<byte[]> ExportarExpedienteZipAsync(Guid requestId)
    {
        var request = await _requestService.GetByIdAsync(requestId)
            ?? throw new InvalidOperationException("Solicitud no encontrada.");

        var auditLogs = await _db.AuditLogs
            .Where(a => a.RequestId == requestId)
            .OrderBy(a => a.CreatedAt)
            .ToListAsync();

        using var ms = new MemoryStream();
        using (var zip = new ZipArchive(ms, ZipArchiveMode.Create, leaveOpen: true))
        {
            // PDF Declaración Beneficiario Final
            await AddBytesEntry(zip, "declaracion_beneficiario_final.pdf",
                () => _pdfService.GenerateBeneficialOwnerDeclarationPdf(request));

            // PDF Declaración PEP
            await AddBytesEntry(zip, "declaracion_pep.pdf",
                () => _pdfService.GeneratePepDeclarationPdf(request));

            // PDF Consolidado
            await AddBytesEntry(zip, "declaracion_consolidada.pdf",
                () => _pdfService.GenerateConsolidatedPdf(request));

            // Documentos adjuntos
            foreach (var doc in request.Documents.Where(d => d.IsActive))
            {
                try
                {
                    await using var stream = await _fileStorage.GetFileAsync(doc.StoragePath);
                    var folder = doc.DocumentType.ToDisplayString().Replace(" ", "_");
                    var entry = zip.CreateEntry($"documentos/{folder}/{doc.OriginalFileName}",
                        CompressionLevel.Fastest);
                    await using var entryStream = entry.Open();
                    await stream.CopyToAsync(entryStream);
                }
                catch (Exception ex)
                {
                    _logger.LogWarning(ex, "No se pudo incluir el documento {Id} en el ZIP", doc.Id);
                }
            }

            // Auditoría JSON
            var auditJson = JsonSerializer.Serialize(
                auditLogs.Select(a => new
                {
                    a.Action, a.EntityType, a.EntityId,
                    a.UserName, a.IpAddress,
                    Fecha = a.CreatedAt.ToString("dd/MM/yyyy HH:mm:ss"),
                    a.OldValues, a.NewValues
                }),
                new JsonSerializerOptions { WriteIndented = true }
            );
            await AddTextEntry(zip, "auditoria.json", auditJson);

            // Resumen TXT
            await AddTextEntry(zip, "resumen.txt", BuildResumen(request));
        }

        return ms.ToArray();
    }

    private static async Task AddBytesEntry(ZipArchive zip, string name, Func<byte[]> generator)
    {
        try
        {
            var bytes = generator();
            var entry = zip.CreateEntry(name, CompressionLevel.Optimal);
            await using var s = entry.Open();
            await s.WriteAsync(bytes);
        }
        catch { /* no bloquear el export si falla un PDF */ }
    }

    private static async Task AddTextEntry(ZipArchive zip, string name, string content)
    {
        var entry = zip.CreateEntry(name, CompressionLevel.Optimal);
        await using var s = entry.Open();
        await s.WriteAsync(Encoding.UTF8.GetBytes(content));
    }

    private static string BuildResumen(Models.Domain.Request request)
    {
        var sb = new StringBuilder();
        sb.AppendLine("=============================================================");
        sb.AppendLine("  RESUMEN DE EXPEDIENTE — FormulariosUAF");
        sb.AppendLine("=============================================================");
        sb.AppendLine($"Folio:            {request.RequestNumber}");
        sb.AppendLine($"RUT Cliente:      {request.Client.RUT}");
        sb.AppendLine($"Razón Social:     {request.Client.BusinessName}");
        sb.AppendLine($"Tipo:             {request.RequestType.ToDisplayString()}");
        sb.AppendLine($"Estado:           {request.Status.ToDisplayString()}");
        sb.AppendLine($"Creada:           {request.CreatedAt:dd/MM/yyyy HH:mm}");
        sb.AppendLine($"Completada:       {request.CompletedAt?.ToString("dd/MM/yyyy HH:mm") ?? "—"}");
        sb.AppendLine($"Exportado:        {DateTime.Now:dd/MM/yyyy HH:mm}");
        sb.AppendLine();

        if (request.BeneficialOwners.Any())
        {
            sb.AppendLine("BENEFICIARIOS FINALES:");
            foreach (var bo in request.BeneficialOwners.OrderBy(b => b.SortOrder))
                sb.AppendLine($"  - {bo.FullName} ({bo.IdNumber}) | {bo.ParticipationPercentage:F2}% | PEP: {(bo.IsPEP ? "SÍ" : "No")}");
        }

        sb.AppendLine();
        if (request.PepDeclaration is { } pep)
            sb.AppendLine($"DECLARACIÓN PEP: {(pep.DeclaresPEP ? "DECLARA SER PEP" : "No es PEP")} — Firmante: {pep.SignatureFullName}");

        sb.AppendLine();
        sb.AppendLine("HISTORIAL DE ESTADOS:");
        foreach (var h in request.StatusHistory.OrderBy(x => x.ChangedAt))
            sb.AppendLine($"  [{h.ChangedAt:dd/MM/yyyy HH:mm}] {h.OldStatus.ToDisplayString()} → {h.NewStatus.ToDisplayString()} por {h.ChangedBy}" +
                          (string.IsNullOrEmpty(h.Notes) ? "" : $" — {h.Notes}"));

        sb.AppendLine();
        sb.AppendLine("-------------------------------------------------------------");
        sb.AppendLine("La información declarada es confidencial.");
        sb.AppendLine("=============================================================");
        return sb.ToString();
    }
}
