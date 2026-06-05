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

public class Paso4Model : PageModel
{
    private readonly ApplicationDbContext _db;
    private readonly IAuditService _audit;

    public Paso4Model(ApplicationDbContext db, IAuditService audit) { _db = db; _audit = audit; }

    [BindProperty] public PepInput Input { get; set; } = new();
    public string? ErrorMessage { get; set; }

    public class PepInput
    {
        [Required] public string DeclarantName { get; set; } = string.Empty;
        [Required] public string IdNumber { get; set; } = string.Empty;
        [Required] public string Nationality { get; set; } = "Chilena";
        public bool DeclaresPEP { get; set; }
        public string? PepName { get; set; }
        public string? Institution { get; set; }
        public PepReason? PepReasonType { get; set; }
        public string? VinculoType { get; set; }
        [Required] public string SignatureFullName { get; set; } = string.Empty;
        [Required] public string SignatureIdNumber { get; set; } = string.Empty;
        [MustBeTrue(ErrorMessage = "Debe aceptar la declaración bajo juramento para continuar.")]
        public bool AcceptsUnderOath { get; set; }
    }

    public async Task<IActionResult> OnGetAsync()
    {
        var requestId = GetRequestId();
        if (requestId == Guid.Empty) return RedirectToPage("/Cliente/Inicio");

        var existing = await _db.PepDeclarations.FirstOrDefaultAsync(p => p.RequestId == requestId);
        if (existing is not null)
        {
            Input = new PepInput
            {
                DeclarantName = existing.DeclarantName,
                IdNumber = existing.IdNumber,
                Nationality = existing.Nationality,
                DeclaresPEP = existing.DeclaresPEP,
                PepName = existing.PepName,
                Institution = existing.Institution,
                PepReasonType = existing.PepReasonType,
                VinculoType = existing.VinculoType,
                SignatureFullName = existing.SignatureFullName,
                SignatureIdNumber = existing.SignatureIdNumber,
                AcceptsUnderOath = existing.AcceptsUnderOath
            };
        }
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        var requestId = GetRequestId();
        if (requestId == Guid.Empty) return RedirectToPage("/Cliente/Inicio");
        if (!ModelState.IsValid) return Page();

        if (Input.DeclaresPEP && string.IsNullOrEmpty(Input.PepName))
        {
            ErrorMessage = "Si declara ser PEP, debe completar el nombre del PEP.";
            return Page();
        }

        var ip = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "unknown";
        var ua = Request.Headers.UserAgent.ToString();

        var existing = await _db.PepDeclarations.FirstOrDefaultAsync(p => p.RequestId == requestId);
        if (existing is null)
        {
            existing = new PepDeclaration { RequestId = requestId };
            _db.PepDeclarations.Add(existing);
        }

        existing.DeclarantName = Input.DeclarantName;
        existing.IdNumber = Input.IdNumber;
        existing.Nationality = Input.Nationality;
        existing.DeclaresPEP = Input.DeclaresPEP;
        existing.PepName = Input.DeclaresPEP ? Input.PepName : null;
        existing.Institution = Input.DeclaresPEP ? Input.Institution : null;
        existing.PepReasonType = Input.DeclaresPEP ? Input.PepReasonType : null;
        existing.VinculoType = Input.DeclaresPEP ? Input.VinculoType : null;
        existing.DeclarationDate = DateTime.Now;
        existing.AcceptsUnderOath = Input.AcceptsUnderOath;
        existing.SignatureFullName = Input.SignatureFullName;
        existing.SignatureIdNumber = Input.SignatureIdNumber;
        existing.SignatureDateTime = DateTime.UtcNow;
        existing.SignatureIpAddress = ip;
        existing.SignatureUserAgent = ua;
        existing.UpdatedAt = DateTime.UtcNow;

        var req = await _db.Requests.FindAsync(requestId);
        if (req != null && req.CurrentStep < 5) req.CurrentStep = 5;

        await _db.SaveChangesAsync();
        await _audit.LogAsync("GUARDAR_PASO4", "PepDeclaration", requestId.ToString(), requestId: requestId);
        return RedirectToPage("/Cliente/Paso5");
    }

    private Guid GetRequestId()
    {
        var val = HttpContext.Session.GetString("ClientRequestId");
        return Guid.TryParse(val, out var id) ? id : Guid.Empty;
    }
}
