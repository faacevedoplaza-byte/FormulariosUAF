using System.ComponentModel.DataAnnotations;
using FormulariosUAF.Data;
using FormulariosUAF.Models.Domain;
using FormulariosUAF.Models.Enums;
using FormulariosUAF.Services;
using FormulariosUAF.Validation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace FormulariosUAF.Pages.Cliente;

public class DeclaracionModel : PageModel
{
    private readonly ApplicationDbContext _db;
    private readonly IAuditService _audit;

    public DeclaracionModel(ApplicationDbContext db, IAuditService audit)
    {
        _db = db;
        _audit = audit;
    }

    // Display properties (GET only)
    public LegalEntityDeclaration? Declaration { get; set; }
    public RequestType RequestType { get; set; }
    public bool EsActualizacionSinCambios => RequestType == RequestType.ActualizacionSinCambios;
    public string? ErrorMessage { get; set; }

    [BindProperty] public InputModel Input { get; set; } = new();

    public class InputModel
    {
        // Empresa edit (optional — solo si el cliente activa "editar")
        public bool EmpresaEdited { get; set; }
        [MaxLength(500)] public string? EmpresaAddress { get; set; }
        [MaxLength(200)] public string? EmpresaCity { get; set; }
        [MaxLength(200)] public string? EmpresaCountryOfIncorporation { get; set; }
        [MaxLength(50)]  public string? EmpresaPhone { get; set; }
        public EntityType? EmpresaEntityType { get; set; }
        [MaxLength(200)] public string? EmpresaEntityTypeOther { get; set; }
        [MaxLength(50)]  public string? EmpresaLegalRepresentativeIdNumber { get; set; }
        [MaxLength(500)] public string? EmpresaLegalRepresentativeName { get; set; }

        // Personas declaradas
        public List<PersonaInput> Personas { get; set; } = [];

        // Sin personas: declaración explícita
        public bool SinPersonasConfirma { get; set; }

        // Actualización sin cambios
        public bool SinCambiosConfirma { get; set; }
        [MaxLength(100)] public string? SinCambiosNumDeclaracion { get; set; }
        [MaxLength(50)]  public string? SinCambiosFecha { get; set; }

        // Declarante
        public NationalityType DeclaranteNacionalidad { get; set; }
        [Required(ErrorMessage = "RUT del declarante obligatorio")]
        [MaxLength(50)] public string DeclaranteIdNumber { get; set; } = string.Empty;
        [Required(ErrorMessage = "Nombres obligatorios")]
        [MaxLength(200)] public string DeclaranteFirstName { get; set; } = string.Empty;
        [Required(ErrorMessage = "Apellido paterno obligatorio")]
        [MaxLength(200)] public string DeclaranteLastName1 { get; set; } = string.Empty;
        [MaxLength(200)] public string? DeclaranteLastName2 { get; set; }
        [MaxLength(200)] public string? DeclarantePlaceOfOrigin { get; set; }
        [Required(ErrorMessage = "Relación con la empresa obligatoria")]
        [MaxLength(300)] public string DeclaranteRelationship { get; set; } = string.Empty;
        [Required(ErrorMessage = "Ciudad obligatoria")]
        [MaxLength(200)] public string DeclaranteCity { get; set; } = string.Empty;
        [EmailAddress] [MaxLength(200)] public string? DeclaranteEmail { get; set; }
        [MaxLength(50)]  public string? DeclarantePhone { get; set; }

        // Firma
        [Required(ErrorMessage = "Nombre para firma obligatorio")]
        [MaxLength(500)] public string FirmaFullName { get; set; } = string.Empty;
        [Required(ErrorMessage = "RUT para firma obligatorio")]
        [MaxLength(50)]  public string FirmaIdNumber { get; set; } = string.Empty;
        [MustBeTrue(ErrorMessage = "Debe declarar bajo juramento para continuar.")]
        public bool FirmaDeclaraJuramento { get; set; }
    }

    public class PersonaInput
    {
        [MaxLength(50)]  public string IdNumber { get; set; } = string.Empty;
        [MaxLength(500)] public string FullName { get; set; } = string.Empty;
        [MaxLength(500)] public string? Address { get; set; }
        [MaxLength(200)] public string? City { get; set; }
        [MaxLength(200)] public string Country { get; set; } = "Chile";
        [Range(0, 100)]  public decimal ParticipationPercentage { get; set; }
        public RelationshipType RelationshipType { get; set; }
        [MaxLength(200)] public string? RelationshipTypeOther { get; set; }
        public bool HasMinTenPercentParticipation { get; set; }
        public bool IsEffectiveController { get; set; }
        [MaxLength(1000)] public string? EffectiveControlDescription { get; set; }
        public bool HandlesCashOrFunds { get; set; }
        public bool IsPEP { get; set; }
        public PepType? PepType { get; set; }
        [MaxLength(300)] public string? PepTypeName { get; set; }
        [MaxLength(300)] public string? PepInstitution { get; set; }
        [MaxLength(300)] public string? PepPosition { get; set; }
        [MaxLength(300)] public string? PepRelationship { get; set; }
        [MaxLength(1000)] public string? PepObservation { get; set; }
    }

    public async Task<IActionResult> OnGetAsync()
    {
        var requestId = GetRequestId();
        if (requestId == Guid.Empty) return RedirectToPage("/Cliente/Inicio");

        var req = await _db.Requests
            .Include(r => r.LegalEntityDeclaration)
            .Include(r => r.DeclaredPersons.OrderBy(d => d.SortOrder))
            .Include(r => r.Declarant)
            .FirstOrDefaultAsync(r => r.Id == requestId);

        if (req is null) return RedirectToPage("/Cliente/Inicio");
        if (req.LegalEntityDeclaration is null) return RedirectToPage("/Cliente/Inicio");

        Declaration = req.LegalEntityDeclaration;
        RequestType = req.RequestType;

        // Pre-populate from existing saved data (para regresos al paso)
        if (req.DeclaredPersons.Any())
        {
            Input.Personas = req.DeclaredPersons.Select(p => new PersonaInput
            {
                IdNumber = p.IdNumber,
                FullName = p.FullName,
                Address = p.Address,
                City = p.City,
                Country = p.Country,
                ParticipationPercentage = p.ParticipationPercentage,
                RelationshipType = p.RelationshipType,
                RelationshipTypeOther = p.RelationshipTypeOther,
                HasMinTenPercentParticipation = p.HasMinTenPercentParticipation,
                IsEffectiveController = p.IsEffectiveController,
                EffectiveControlDescription = p.EffectiveControlDescription,
                HandlesCashOrFunds = p.HandlesCashOrFunds,
                IsPEP = p.IsPEP,
                PepType = p.PepType,
                PepTypeName = p.PepTypeName,
                PepInstitution = p.PepInstitution,
                PepPosition = p.PepPosition,
                PepRelationship = p.PepRelationship,
                PepObservation = p.PepObservation
            }).ToList();
        }

        // Mostrar una persona vacía por defecto al abrir el formulario (formulario nuevo).
        // En OnPost las personas con RUT vacío se descartan, así que no afecta el guardado.
        if (Input.Personas.Count == 0)
        {
            Input.Personas.Add(new PersonaInput());
        }

        if (req.Declarant is { } dec)
        {
            Input.DeclaranteNacionalidad = dec.NationalityType;
            Input.DeclaranteIdNumber = dec.IdNumber;
            Input.DeclaranteFirstName = dec.FirstName;
            Input.DeclaranteLastName1 = dec.LastName1;
            Input.DeclaranteLastName2 = dec.LastName2;
            Input.DeclarantePlaceOfOrigin = dec.PlaceOfOrigin;
            Input.DeclaranteRelationship = dec.RelationshipWithLegalEntity;
            Input.DeclaranteCity = dec.City;
            Input.DeclaranteEmail = dec.Email;
            Input.DeclarantePhone = dec.Phone;
            Input.FirmaFullName = dec.SignatureFullName;
            Input.FirmaIdNumber = dec.SignatureIdNumber;
        }

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        var requestId = GetRequestId();
        if (requestId == Guid.Empty) return RedirectToPage("/Cliente/Inicio");

        var req = await _db.Requests
            .Include(r => r.LegalEntityDeclaration)
            .Include(r => r.DeclaredPersons)
            .Include(r => r.Declarant)
            .FirstOrDefaultAsync(r => r.Id == requestId);

        if (req is null || req.LegalEntityDeclaration is null) return RedirectToPage("/Cliente/Inicio");

        Declaration = req.LegalEntityDeclaration;
        RequestType = req.RequestType;

        if (!ModelState.IsValid) return Page();

        // Validar personas
        var personasValidas = Input.Personas.Where(p => !string.IsNullOrWhiteSpace(p.IdNumber)).ToList();
        bool requierePersonas = !(EsActualizacionSinCambios && Input.SinCambiosConfirma);
        if (requierePersonas && !personasValidas.Any() && !Input.SinPersonasConfirma)
        {
            ErrorMessage = "Debe agregar al menos una persona declarada, o confirmar explícitamente que no existen beneficiarios finales.";
            return Page();
        }

        // Validar campos PEP por persona
        foreach (var (p, i) in personasValidas.Select((p, i) => (p, i)))
        {
            if (p.IsPEP && p.PepType is null)
            {
                ModelState.AddModelError($"Input.Personas[{i}].PepType", "Debe indicar el tipo PEP.");
                ErrorMessage = "Complete los campos PEP requeridos.";
                return Page();
            }
            // Evitar duplicados por RUT
            if (personasValidas.Count(x => x.IdNumber == p.IdNumber) > 1)
            {
                ErrorMessage = $"El RUT/ID {p.IdNumber} está duplicado. Cada persona debe aparecer una sola vez.";
                return Page();
            }
        }

        var ip = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "unknown";
        var ua = Request.Headers.UserAgent.ToString();

        // 1. Edición empresa (si el cliente activó el modo edición)
        if (Input.EmpresaEdited)
        {
            var led = req.LegalEntityDeclaration;
            var oldValues = new { led.Address, led.City, led.CountryOfIncorporation, led.Phone, led.EntityType, led.LegalRepresentativeName, led.LegalRepresentativeIdNumber };
            if (Input.EmpresaAddress is not null) led.Address = Input.EmpresaAddress;
            if (Input.EmpresaCity is not null) led.City = Input.EmpresaCity;
            if (Input.EmpresaCountryOfIncorporation is not null) led.CountryOfIncorporation = Input.EmpresaCountryOfIncorporation;
            if (Input.EmpresaPhone is not null) led.Phone = Input.EmpresaPhone;
            if (Input.EmpresaEntityType.HasValue) led.EntityType = Input.EmpresaEntityType.Value;
            if (Input.EmpresaEntityTypeOther is not null) led.EntityTypeOther = Input.EmpresaEntityTypeOther;
            if (Input.EmpresaLegalRepresentativeIdNumber is not null) led.LegalRepresentativeIdNumber = Input.EmpresaLegalRepresentativeIdNumber;
            if (Input.EmpresaLegalRepresentativeName is not null) led.LegalRepresentativeName = Input.EmpresaLegalRepresentativeName;
            led.UpdatedAt = DateTime.UtcNow;
            await _audit.LogAsync("EDITAR_EMPRESA_CLIENTE", "LegalEntityDeclaration", requestId.ToString(),
                oldValues: oldValues, newValues: new { led.Address, led.City }, requestId: requestId);
        }

        // 2. Personas declaradas (replace-all)
        var existing = await _db.DeclaredPersons.Where(d => d.RequestId == requestId).ToListAsync();
        _db.DeclaredPersons.RemoveRange(existing);

        for (int i = 0; i < personasValidas.Count; i++)
        {
            var p = personasValidas[i];
            _db.DeclaredPersons.Add(new DeclaredPerson
            {
                RequestId = requestId,
                IdNumber = p.IdNumber,
                FullName = p.FullName,
                Address = p.Address,
                City = p.City,
                Country = p.Country,
                ParticipationPercentage = p.ParticipationPercentage,
                RelationshipType = p.RelationshipType,
                RelationshipTypeOther = p.RelationshipTypeOther,
                HasMinTenPercentParticipation = p.HasMinTenPercentParticipation || p.ParticipationPercentage >= 10,
                IsEffectiveController = p.IsEffectiveController,
                EffectiveControlDescription = p.IsEffectiveController ? p.EffectiveControlDescription : null,
                HandlesCashOrFunds = p.HandlesCashOrFunds,
                IsPEP = p.IsPEP,
                PepType = p.IsPEP ? p.PepType : null,
                PepTypeName = p.IsPEP ? p.PepTypeName : null,
                PepInstitution = p.IsPEP ? p.PepInstitution : null,
                PepPosition = p.IsPEP ? p.PepPosition : null,
                PepRelationship = p.IsPEP ? p.PepRelationship : null,
                PepObservation = p.IsPEP ? p.PepObservation : null,
                SortOrder = i
            });
        }

        // 3. Declarante
        var declarant = req.Declarant ?? new Declarant { RequestId = requestId };
        if (req.Declarant is null) _db.Declarants.Add(declarant);

        declarant.NationalityType = Input.DeclaranteNacionalidad;
        declarant.IdNumber = Input.DeclaranteIdNumber;
        declarant.FirstName = Input.DeclaranteFirstName;
        declarant.LastName1 = Input.DeclaranteLastName1;
        declarant.LastName2 = Input.DeclaranteLastName2;
        declarant.PlaceOfOrigin = Input.DeclarantePlaceOfOrigin;
        declarant.RelationshipWithLegalEntity = Input.DeclaranteRelationship;
        declarant.City = Input.DeclaranteCity;
        declarant.Email = Input.DeclaranteEmail;
        declarant.Phone = Input.DeclarantePhone;
        declarant.DeclaresUnderOath = Input.FirmaDeclaraJuramento;
        declarant.DeclarationDate = DateTime.Today;
        declarant.SignatureFullName = Input.FirmaFullName;
        declarant.SignatureIdNumber = Input.FirmaIdNumber;
        declarant.SignatureDateTime = DateTime.UtcNow;
        declarant.SignatureIpAddress = ip;
        declarant.SignatureUserAgent = ua;
        declarant.UpdatedAt = DateTime.UtcNow;

        // 4. Avanzar paso
        if (req.CurrentStep < 2) req.CurrentStep = 2;

        await _db.SaveChangesAsync();
        await _audit.LogAsync("GUARDAR_DECLARACION", "Request", requestId.ToString(), requestId: requestId);

        return RedirectToPage("/Cliente/Adjuntos");
    }

    private Guid GetRequestId()
    {
        var val = HttpContext.Session.GetString("ClientRequestId");
        return Guid.TryParse(val, out var id) ? id : Guid.Empty;
    }
}
