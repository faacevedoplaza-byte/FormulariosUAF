using FormulariosUAF.Models.Enums;

namespace FormulariosUAF.Models.Domain;

public class LegalEntityDeclaration
{
    public int Id { get; set; }
    public Guid RequestId { get; set; }
    public Request Request { get; set; } = null!;

    public string RUT { get; set; } = string.Empty;
    public string BusinessName { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string CountryOfIncorporation { get; set; } = "Chile";
    public string? Phone { get; set; }

    public string LegalRepresentativeIdNumber { get; set; } = string.Empty;
    public string LegalRepresentativeName { get; set; } = string.Empty;

    public EntityType EntityType { get; set; }
    public string? EntityTypeOther { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
}
