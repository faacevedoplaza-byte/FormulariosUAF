using FormulariosUAF.Models.Enums;

namespace FormulariosUAF.Models.Domain;

public class DeclaredPerson
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid RequestId { get; set; }
    public Request Request { get; set; } = null!;

    public string IdNumber { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public string? Address { get; set; }
    public string? City { get; set; }
    public string Country { get; set; } = "Chile";

    public decimal ParticipationPercentage { get; set; }
    public RelationshipType RelationshipType { get; set; }
    public string? RelationshipTypeOther { get; set; }

    public bool HasMinTenPercentParticipation { get; set; }
    public bool IsEffectiveController { get; set; }
    public string? EffectiveControlDescription { get; set; }
    public bool HandlesCashOrFunds { get; set; }

    public bool IsPEP { get; set; }
    public PepType? PepType { get; set; }
    public string? PepTypeName { get; set; }
    public string? PepInstitution { get; set; }
    public string? PepPosition { get; set; }
    public string? PepRelationship { get; set; }
    public string? PepObservation { get; set; }

    public int SortOrder { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
}
