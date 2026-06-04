using FormulariosUAF.Models.Enums;

namespace FormulariosUAF.Models.Domain;

public class PepDeclaration
{
    public int Id { get; set; }
    public Guid RequestId { get; set; }
    public Request Request { get; set; } = null!;

    public string DeclarantName { get; set; } = string.Empty;
    public string IdNumber { get; set; } = string.Empty;
    public string Nationality { get; set; } = string.Empty;

    public bool DeclaresPEP { get; set; }
    public string? PepName { get; set; }
    public string? Institution { get; set; }
    public PepReason? PepReasonType { get; set; }
    public string? PepReasonOther { get; set; }
    public string? VinculoType { get; set; }

    public DateTime DeclarationDate { get; set; }
    public bool AcceptsUnderOath { get; set; }

    public string SignatureFullName { get; set; } = string.Empty;
    public string SignatureIdNumber { get; set; } = string.Empty;
    public DateTime SignatureDateTime { get; set; }
    public string SignatureIpAddress { get; set; } = string.Empty;
    public string? SignatureUserAgent { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
}
