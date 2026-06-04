using System.ComponentModel.DataAnnotations;
using FormulariosUAF.Data;
using FormulariosUAF.Models.Domain;
using FormulariosUAF.Models.Enums;
using FormulariosUAF.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace FormulariosUAF.Pages.Cliente;

public class Paso1Model : PageModel
{
    private readonly ApplicationDbContext _db;
    private readonly IAuditService _audit;

    public Paso1Model(ApplicationDbContext db, IAuditService audit)
    {
        _db = db;
        _audit = audit;
    }

    [BindProperty] public InputModel Input { get; set; } = new();
    public string? ErrorMessage { get; set; }

    public class InputModel
    {
        [Required(ErrorMessage = "El RUT es obligatorio")]
        [MaxLength(20)]
        public string RUT { get; set; } = string.Empty;

        [Required(ErrorMessage = "La razón social es obligatoria")]
        [MaxLength(500)]
        public string BusinessName { get; set; } = string.Empty;

        [Required(ErrorMessage = "El domicilio es obligatorio")]
        [MaxLength(500)]
        public string Address { get; set; } = string.Empty;

        [Required(ErrorMessage = "La ciudad es obligatoria")]
        [MaxLength(200)]
        public string City { get; set; } = string.Empty;

        [Required]
        [MaxLength(200)]
        public string CountryOfIncorporation { get; set; } = "Chile";

        [MaxLength(50)]
        public string? Phone { get; set; }

        [Required]
        public EntityType EntityType { get; set; }

        [MaxLength(200)]
        public string? EntityTypeOther { get; set; }

        [Required(ErrorMessage = "El RUT del representante es obligatorio")]
        [MaxLength(50)]
        public string LegalRepresentativeIdNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "El nombre del representante es obligatorio")]
        [MaxLength(500)]
        public string LegalRepresentativeName { get; set; } = string.Empty;
    }

    public async Task<IActionResult> OnGetAsync()
    {
        var requestId = GetRequestId();
        if (requestId == Guid.Empty) return RedirectToPage("/Cliente/Inicio");

        var existing = await _db.LegalEntityDeclarations.FirstOrDefaultAsync(l => l.RequestId == requestId);
        if (existing is not null)
        {
            Input = new InputModel
            {
                RUT = existing.RUT,
                BusinessName = existing.BusinessName,
                Address = existing.Address,
                City = existing.City,
                CountryOfIncorporation = existing.CountryOfIncorporation,
                Phone = existing.Phone,
                EntityType = existing.EntityType,
                EntityTypeOther = existing.EntityTypeOther,
                LegalRepresentativeIdNumber = existing.LegalRepresentativeIdNumber,
                LegalRepresentativeName = existing.LegalRepresentativeName
            };
        }
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        var requestId = GetRequestId();
        if (requestId == Guid.Empty) return RedirectToPage("/Cliente/Inicio");
        if (!ModelState.IsValid) return Page();

        var existing = await _db.LegalEntityDeclarations.FirstOrDefaultAsync(l => l.RequestId == requestId);
        if (existing is null)
        {
            existing = new LegalEntityDeclaration { RequestId = requestId };
            _db.LegalEntityDeclarations.Add(existing);
        }

        existing.RUT = Input.RUT;
        existing.BusinessName = Input.BusinessName;
        existing.Address = Input.Address;
        existing.City = Input.City;
        existing.CountryOfIncorporation = Input.CountryOfIncorporation;
        existing.Phone = Input.Phone;
        existing.EntityType = Input.EntityType;
        existing.EntityTypeOther = Input.EntityTypeOther;
        existing.LegalRepresentativeIdNumber = Input.LegalRepresentativeIdNumber;
        existing.LegalRepresentativeName = Input.LegalRepresentativeName;
        existing.UpdatedAt = DateTime.UtcNow;

        var req = await _db.Requests.FindAsync(requestId);
        if (req is not null && req.CurrentStep < 2) req.CurrentStep = 2;

        await _db.SaveChangesAsync();
        await _audit.LogAsync("GUARDAR_PASO1", "LegalEntityDeclaration", requestId.ToString(), requestId: requestId);

        return RedirectToPage("/Cliente/Paso2");
    }

    private Guid GetRequestId()
    {
        var val = HttpContext.Session.GetString("ClientRequestId");
        return Guid.TryParse(val, out var id) ? id : Guid.Empty;
    }
}
