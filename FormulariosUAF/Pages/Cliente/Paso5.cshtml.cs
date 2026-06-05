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

public class Paso5Model : PageModel
{
    private readonly ApplicationDbContext _db;
    private readonly IAuditService _audit;

    public Paso5Model(ApplicationDbContext db, IAuditService audit) { _db = db; _audit = audit; }

    [BindProperty] public DeclarantInput Input { get; set; } = new();
    public string? ErrorMessage { get; set; }

    public class DeclarantInput
    {
        public NationalityType NationalityType { get; set; }
        [Required] public string IdNumber { get; set; } = string.Empty;
        [Required] public string FirstName { get; set; } = string.Empty;
        [Required] public string LastName1 { get; set; } = string.Empty;
        public string? LastName2 { get; set; }
        public string? PlaceOfOrigin { get; set; }
        [Required] public string RelationshipWithLegalEntity { get; set; } = string.Empty;
        [MustBeTrue(ErrorMessage = "Debe declarar bajo juramento para continuar.")]
        public bool DeclaresUnderOath { get; set; }
        [Required] public string City { get; set; } = string.Empty;
        [Required] public DateTime DeclarationDate { get; set; } = DateTime.Today;
    }

    public async Task<IActionResult> OnGetAsync()
    {
        var requestId = GetRequestId();
        if (requestId == Guid.Empty) return RedirectToPage("/Cliente/Inicio");

        var existing = await _db.Declarants.FirstOrDefaultAsync(d => d.RequestId == requestId);
        if (existing is not null)
        {
            Input = new DeclarantInput
            {
                NationalityType = existing.NationalityType,
                IdNumber = existing.IdNumber,
                FirstName = existing.FirstName,
                LastName1 = existing.LastName1,
                LastName2 = existing.LastName2,
                PlaceOfOrigin = existing.PlaceOfOrigin,
                RelationshipWithLegalEntity = existing.RelationshipWithLegalEntity,
                DeclaresUnderOath = existing.DeclaresUnderOath,
                City = existing.City,
                DeclarationDate = existing.DeclarationDate
            };
        }
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        var requestId = GetRequestId();
        if (requestId == Guid.Empty) return RedirectToPage("/Cliente/Inicio");
        if (!ModelState.IsValid) return Page();

        var existing = await _db.Declarants.FirstOrDefaultAsync(d => d.RequestId == requestId);
        if (existing is null)
        {
            existing = new Declarant { RequestId = requestId };
            _db.Declarants.Add(existing);
        }

        existing.NationalityType = Input.NationalityType;
        existing.IdNumber = Input.IdNumber;
        existing.FirstName = Input.FirstName;
        existing.LastName1 = Input.LastName1;
        existing.LastName2 = Input.LastName2;
        existing.PlaceOfOrigin = Input.PlaceOfOrigin;
        existing.RelationshipWithLegalEntity = Input.RelationshipWithLegalEntity;
        existing.DeclaresUnderOath = Input.DeclaresUnderOath;
        existing.City = Input.City;
        existing.DeclarationDate = Input.DeclarationDate;
        existing.UpdatedAt = DateTime.UtcNow;

        var req = await _db.Requests.FindAsync(requestId);
        if (req != null && req.CurrentStep < 6) req.CurrentStep = 6;

        await _db.SaveChangesAsync();
        await _audit.LogAsync("GUARDAR_PASO5", "Declarant", requestId.ToString(), requestId: requestId);
        return RedirectToPage("/Cliente/Paso6");
    }

    private Guid GetRequestId()
    {
        var val = HttpContext.Session.GetString("ClientRequestId");
        return Guid.TryParse(val, out var id) ? id : Guid.Empty;
    }
}
