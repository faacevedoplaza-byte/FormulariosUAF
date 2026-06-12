using FormulariosUAF.Models.Enums;

namespace FormulariosUAF.Models.Domain;

public class Declarant
{
    public int Id { get; set; }
    public Guid RequestId { get; set; }
    public Request Request { get; set; } = null!;

    public NationalityType NationalityType { get; set; }
    public string IdNumber { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName1 { get; set; } = string.Empty;
    public string? LastName2 { get; set; }
    public string? PlaceOfOrigin { get; set; }
    public string RelationshipWithLegalEntity { get; set; } = string.Empty;
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public bool DeclaresUnderOath { get; set; }
    public string City { get; set; } = string.Empty;
    public DateTime DeclarationDate { get; set; }

    public string SignatureFullName { get; set; } = string.Empty;
    public string SignatureIdNumber { get; set; } = string.Empty;
    public DateTime? SignatureDateTime { get; set; }
    public string? SignatureIpAddress { get; set; }
    public string? SignatureUserAgent { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }

    public string FullName => $"{FirstName} {LastName1} {LastName2}".Trim();
}
