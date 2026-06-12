using System.Text.RegularExpressions;
using FormulariosUAF.Data;
using FormulariosUAF.Models.Domain;
using FormulariosUAF.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace FormulariosUAF.Services;

public class TaxFolderAnalysisService : ITaxFolderAnalysisService
{
    private readonly ApplicationDbContext _db;
    private readonly ITaxFolderTextExtractor _extractor;

    public TaxFolderAnalysisService(ApplicationDbContext db, ITaxFolderTextExtractor extractor)
    {
        _db = db;
        _extractor = extractor;
    }

    public async Task<TaxFolderAnalysis> AnalyzeAndCompareAsync(Request request, Stream carpetaStream)
    {
        var text = await _extractor.ExtractTextAsync(carpetaStream, "application/pdf");
        var wasReadable = !string.IsNullOrWhiteSpace(text) && text.Length > 50;

        // Extraer campos básicos con regex
        var extractedRut = ExtractRut(text);
        var extractedName = ExtractBusinessName(text);
        var extractedAddress = ExtractAddress(text);
        var extractedActivity = ExtractActivity(text);
        var extractedLegalRep = ExtractLegalRep(text);
        var extractedDate = ExtractDate(text);

        // Eliminar análisis previo si existe
        var existing = await _db.TaxFolderAnalyses
            .Include(t => t.Alerts)
            .FirstOrDefaultAsync(t => t.RequestId == request.Id);
        if (existing is not null)
        {
            _db.TaxFolderAlerts.RemoveRange(existing.Alerts);
            _db.TaxFolderAnalyses.Remove(existing);
        }

        var analysis = new TaxFolderAnalysis
        {
            RequestId = request.Id,
            ExtractedRut = extractedRut,
            ExtractedBusinessName = extractedName,
            ExtractedAddress = extractedAddress,
            ExtractedActivity = extractedActivity,
            ExtractedLegalRep = extractedLegalRep,
            ExtractedIssuedDate = extractedDate,
            RawText = text.Length > 5000 ? text[..5000] : text,
            WasReadable = wasReadable,
            AnalyzedAt = DateTime.UtcNow
        };

        _db.TaxFolderAnalyses.Add(analysis);
        await _db.SaveChangesAsync();

        // Generar alertas comparativas
        var alerts = GenerateAlerts(analysis, request);
        if (!wasReadable)
        {
            alerts.Add(new TaxFolderAlert
            {
                TaxFolderAnalysisId = analysis.Id,
                AlertType = TaxFolderAlertType.DocumentUnreadable,
                Message = "No se pudo extraer texto del documento. Puede ser un PDF escaneado o imagen.",
                Severity = "Warning"
            });
        }

        if (alerts.Any())
        {
            _db.TaxFolderAlerts.AddRange(alerts);
            await _db.SaveChangesAsync();
        }

        analysis.Alerts = alerts;
        return analysis;
    }

    private List<TaxFolderAlert> GenerateAlerts(TaxFolderAnalysis analysis, Request request)
    {
        var alerts = new List<TaxFolderAlert>();
        var led = request.LegalEntityDeclaration;
        if (led is null) return alerts;

        // RUT empresa
        if (!string.IsNullOrEmpty(analysis.ExtractedRut) &&
            !NormalizeRut(analysis.ExtractedRut).Equals(NormalizeRut(led.RUT), StringComparison.OrdinalIgnoreCase))
        {
            alerts.Add(Alert(analysis.Id, TaxFolderAlertType.RutMismatch,
                $"El RUT en la carpeta tributaria ({analysis.ExtractedRut}) no coincide con el declarado ({led.RUT}).", "Warning"));
        }

        // Razón social
        if (!string.IsNullOrEmpty(analysis.ExtractedBusinessName) &&
            !NormalizeName(analysis.ExtractedBusinessName).Contains(NormalizeName(led.BusinessName)) &&
            !NormalizeName(led.BusinessName).Contains(NormalizeName(analysis.ExtractedBusinessName)))
        {
            alerts.Add(Alert(analysis.Id, TaxFolderAlertType.BusinessNameMismatch,
                $"La razón social en la carpeta ({analysis.ExtractedBusinessName}) difiere de la declarada ({led.BusinessName}).", "Warning"));
        }

        // Representante legal
        if (!string.IsNullOrEmpty(analysis.ExtractedLegalRep) && !string.IsNullOrEmpty(led.LegalRepresentativeName) &&
            !NormalizeName(analysis.ExtractedLegalRep).Contains(NormalizeName(led.LegalRepresentativeName)) &&
            !NormalizeName(led.LegalRepresentativeName).Contains(NormalizeName(analysis.ExtractedLegalRep)))
        {
            alerts.Add(Alert(analysis.Id, TaxFolderAlertType.LegalRepMismatch,
                $"El representante legal en la carpeta ({analysis.ExtractedLegalRep}) no coincide con el declarado ({led.LegalRepresentativeName}).", "Warning"));
        }

        // Verificar personas declaradas
        foreach (var person in request.DeclaredPersons)
        {
            var nameNorm = NormalizeName(person.FullName);
            var rutNorm = NormalizeRut(person.IdNumber);
            if (!string.IsNullOrEmpty(analysis.RawText) &&
                !analysis.RawText.Contains(nameNorm, StringComparison.OrdinalIgnoreCase) &&
                !analysis.RawText.Contains(rutNorm, StringComparison.OrdinalIgnoreCase))
            {
                alerts.Add(Alert(analysis.Id, TaxFolderAlertType.PersonNotInFolder,
                    $"La persona declarada '{person.FullName}' ({person.IdNumber}) no aparece en la carpeta tributaria.", "Info"));
            }
        }

        return alerts;
    }

    private static TaxFolderAlert Alert(int analysisId, TaxFolderAlertType type, string msg, string severity) =>
        new() { TaxFolderAnalysisId = analysisId, AlertType = type, Message = msg, Severity = severity };

    private static string? ExtractRut(string text)
    {
        var m = Regex.Match(text, @"\b\d{1,2}\.?\d{3}\.?\d{3}-[\dKk]\b");
        return m.Success ? m.Value : null;
    }

    private static string? ExtractBusinessName(string text)
    {
        var m = Regex.Match(text, @"(?:Razón Social|Nombre|NOMBRE)[:\s]+([^\n\r]{5,80})", RegexOptions.IgnoreCase);
        return m.Success ? m.Groups[1].Value.Trim() : null;
    }

    private static string? ExtractAddress(string text)
    {
        var m = Regex.Match(text, @"(?:Domicilio|Dirección|DOMICILIO)[:\s]+([^\n\r]{5,120})", RegexOptions.IgnoreCase);
        return m.Success ? m.Groups[1].Value.Trim() : null;
    }

    private static string? ExtractActivity(string text)
    {
        var m = Regex.Match(text, @"(?:Actividad|Giro|GIRO)[:\s]+([^\n\r]{5,120})", RegexOptions.IgnoreCase);
        return m.Success ? m.Groups[1].Value.Trim() : null;
    }

    private static string? ExtractLegalRep(string text)
    {
        var m = Regex.Match(text, @"(?:Representante Legal|REP\. LEGAL)[:\s]+([^\n\r]{5,80})", RegexOptions.IgnoreCase);
        return m.Success ? m.Groups[1].Value.Trim() : null;
    }

    private static string? ExtractDate(string text)
    {
        var m = Regex.Match(text, @"\b(\d{1,2}[/\-]\d{1,2}[/\-]\d{4}|\d{4}[/\-]\d{1,2}[/\-]\d{1,2})\b");
        return m.Success ? m.Value : null;
    }

    private static string NormalizeRut(string rut) =>
        Regex.Replace(rut ?? "", @"[.\-\s]", "").ToUpperInvariant();

    private static string NormalizeName(string name) =>
        (name ?? "").Trim().ToUpperInvariant().Replace(".", "").Replace(",", "");
}
