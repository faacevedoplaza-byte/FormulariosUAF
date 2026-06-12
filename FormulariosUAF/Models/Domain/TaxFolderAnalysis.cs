namespace FormulariosUAF.Models.Domain;

public class TaxFolderAnalysis
{
    public int Id { get; set; }
    public Guid RequestId { get; set; }
    public Request Request { get; set; } = null!;

    public string? ExtractedRut { get; set; }
    public string? ExtractedBusinessName { get; set; }
    public string? ExtractedAddress { get; set; }
    public string? ExtractedActivity { get; set; }
    public string? ExtractedLegalRep { get; set; }
    public string? ExtractedIssuedDate { get; set; }

    public string? RawText { get; set; }
    public bool WasReadable { get; set; }
    public DateTime AnalyzedAt { get; set; } = DateTime.UtcNow;

    public ICollection<TaxFolderAlert> Alerts { get; set; } = new List<TaxFolderAlert>();
}
