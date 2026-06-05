using FormulariosUAF.Models.Enums;

namespace FormulariosUAF.Models.Domain;

public class SignatureRecord
{
    public int Id { get; set; }
    public Guid RequestId { get; set; }
    public Request Request { get; set; } = null!;

    public SignatureType Type { get; set; } = SignatureType.Simple;
    public SignatureStatus Status { get; set; } = SignatureStatus.Firmada;

    public string SignerName { get; set; } = string.Empty;
    public string SignerIdNumber { get; set; } = string.Empty;
    public DateTime? SignedAt { get; set; }
    public string? IpAddress { get; set; }
    public string? UserAgent { get; set; }
    public string? DocumentHash { get; set; }
    public string FolioNumber { get; set; } = string.Empty;

    // Campos para futura FEA
    public string? ProviderTransactionId { get; set; }
    public string? SignatureUrl { get; set; }
    public string? Certificate { get; set; }
    public string? SignedPdfPath { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
}
