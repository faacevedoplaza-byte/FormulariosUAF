using FormulariosUAF.Models.Enums;

namespace FormulariosUAF.Models.Domain;

public class Request
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string RequestNumber { get; set; } = string.Empty;
    public int ClientId { get; set; }
    public Client Client { get; set; } = null!;
    public string VendorUserId { get; set; } = string.Empty;
    public ApplicationUser VendorUser { get; set; } = null!;
    public RequestType RequestType { get; set; }
    public RequestStatus Status { get; set; } = RequestStatus.Borrador;
    public string ClientToken { get; set; } = string.Empty;
    public DateTime TokenExpiry { get; set; }
    public string? InternalNotes { get; set; }
    public string? ClientEmail { get; set; }
    public string? ClientPhone { get; set; }
    public string? NetcarBusinessNumber { get; set; }
    public int CurrentStep { get; set; } = 1;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public string CreatedBy { get; set; } = string.Empty;
    public DateTime? UpdatedAt { get; set; }
    public string? UpdatedBy { get; set; }
    public DateTime? SentAt { get; set; }
    public DateTime? OpenedAt { get; set; }
    public DateTime? CompletedAt { get; set; }
    public DateTime? DueDate { get; set; }
    public bool IsDeleted { get; set; }

    public LegalEntityDeclaration? LegalEntityDeclaration { get; set; }
    public ICollection<DeclaredPerson> DeclaredPersons { get; set; } = new List<DeclaredPerson>();
    // Legacy — kept for backward compatibility
    public ICollection<BeneficialOwner> BeneficialOwners { get; set; } = new List<BeneficialOwner>();
    public ICollection<EffectiveController> EffectiveControllers { get; set; } = new List<EffectiveController>();
    public PepDeclaration? PepDeclaration { get; set; }
    public Declarant? Declarant { get; set; }
    public TaxFolderAnalysis? TaxFolderAnalysis { get; set; }
    public ICollection<Document> Documents { get; set; } = new List<Document>();
    public ICollection<RequestStatusHistory> StatusHistory { get; set; } = new List<RequestStatusHistory>();
    public ICollection<AuditLog> AuditLogs { get; set; } = new List<AuditLog>();
}
