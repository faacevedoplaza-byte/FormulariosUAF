using FormulariosUAF.Models.Enums;

namespace FormulariosUAF.Models.Domain;

public class TaxFolderAlert
{
    public int Id { get; set; }
    public int TaxFolderAnalysisId { get; set; }
    public TaxFolderAnalysis TaxFolderAnalysis { get; set; } = null!;

    public TaxFolderAlertType AlertType { get; set; }
    public string Message { get; set; } = string.Empty;
    public string Severity { get; set; } = "Warning";
    public bool IsResolved { get; set; }
}
