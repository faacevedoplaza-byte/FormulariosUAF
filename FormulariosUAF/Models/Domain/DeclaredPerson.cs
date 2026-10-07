using FormulariosUAF.Models.Enums;

namespace FormulariosUAF.Models.Domain;

public class DeclaredPerson
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid RequestId { get; set; }
    public Request Request { get; set; } = null!;

    public int TipoDocumentoId { get; set; } = TipoDocumento.IdRut;
    public TipoDocumento TipoDocumento { get; set; } = null!;
    public string IdNumber { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public int? NacionalidadId { get; set; }
    public Nacionalidad? Nacionalidad { get; set; }
    public string? Address { get; set; }
    public string? City { get; set; }
    public int? PaisResidenciaId { get; set; }
    public Pais? PaisResidencia { get; set; }

    /// <summary>
    /// Histórico: país de residencia en texto libre (antes de T_PAIS). Ya no se escribe;
    /// solo se muestra en registros antiguos que no se pudieron asociar a T_PAIS.
    /// </summary>
    public string? Country { get; set; }

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
