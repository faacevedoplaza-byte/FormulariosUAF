using System.ComponentModel.DataAnnotations;
using FormulariosUAF.Data;
using FormulariosUAF.Models.Domain;
using FormulariosUAF.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace FormulariosUAF.Pages.Cliente;

public class BeneficiarioInput
{
    [Required(ErrorMessage = "RUT/ID obligatorio")]
    public string IdNumber { get; set; } = string.Empty;
    [Required(ErrorMessage = "Nombre obligatorio")]
    public string FullName { get; set; } = string.Empty;
    public string? Address { get; set; }
    public string? City { get; set; }
    public string Country { get; set; } = "Chile";
    [Range(0, 100, ErrorMessage = "Entre 0 y 100")]
    public decimal ParticipationPercentage { get; set; }
    public bool IsEffectiveControl { get; set; }
    public bool IsPEP { get; set; }
    public string? PepDetail { get; set; }
}

public record BeneficiarioRowModel(int Index, BeneficiarioInput Data);

public class Paso2Model : PageModel
{
    private readonly ApplicationDbContext _db;
    private readonly IAuditService _audit;

    public Paso2Model(ApplicationDbContext db, IAuditService audit)
    {
        _db = db;
        _audit = audit;
    }

    public List<BeneficiarioInput> Beneficiarios { get; set; } = [];
    public string? ErrorMessage { get; set; }

    public async Task<IActionResult> OnGetAsync()
    {
        var requestId = GetRequestId();
        if (requestId == Guid.Empty) return RedirectToPage("/Cliente/Inicio");

        var existing = await _db.BeneficialOwners
            .Where(b => b.RequestId == requestId)
            .OrderBy(b => b.SortOrder)
            .ToListAsync();

        Beneficiarios = existing.Select(b => new BeneficiarioInput
        {
            IdNumber = b.IdNumber,
            FullName = b.FullName,
            Address = b.Address,
            City = b.City,
            Country = b.Country,
            ParticipationPercentage = b.ParticipationPercentage,
            IsEffectiveControl = b.IsEffectiveControl,
            IsPEP = b.IsPEP,
            PepDetail = b.PepDetail
        }).ToList();

        if (!Beneficiarios.Any())
            Beneficiarios.Add(new BeneficiarioInput());

        return Page();
    }

    public async Task<IActionResult> OnPostAsync(List<BeneficiarioInput> beneficiarios, int count)
    {
        var requestId = GetRequestId();
        if (requestId == Guid.Empty) return RedirectToPage("/Cliente/Inicio");

        beneficiarios ??= [];

        if (!beneficiarios.Any(b => !string.IsNullOrEmpty(b.IdNumber)))
        {
            ErrorMessage = "Debe agregar al menos un beneficiario.";
            Beneficiarios = beneficiarios;
            return Page();
        }

        var existing = await _db.BeneficialOwners.Where(b => b.RequestId == requestId).ToListAsync();
        _db.BeneficialOwners.RemoveRange(existing);

        for (int i = 0; i < beneficiarios.Count; i++)
        {
            var b = beneficiarios[i];
            if (string.IsNullOrEmpty(b.IdNumber)) continue;
            _db.BeneficialOwners.Add(new BeneficialOwner
            {
                RequestId = requestId,
                IdNumber = b.IdNumber,
                FullName = b.FullName,
                Address = b.Address,
                City = b.City,
                Country = b.Country,
                ParticipationPercentage = b.ParticipationPercentage,
                IsBeneficialOwner = b.ParticipationPercentage >= 10,
                IsEffectiveControl = b.IsEffectiveControl,
                IsPEP = b.IsPEP,
                PepDetail = b.IsPEP ? b.PepDetail : null,
                SortOrder = i
            });
        }

        var req = await _db.Requests.FindAsync(requestId);
        if (req is not null && req.CurrentStep < 3) req.CurrentStep = 3;

        await _db.SaveChangesAsync();
        await _audit.LogAsync("GUARDAR_PASO2", "BeneficialOwners", requestId.ToString(), requestId: requestId);

        return RedirectToPage("/Cliente/Paso3");
    }

    private Guid GetRequestId()
    {
        var val = HttpContext.Session.GetString("ClientRequestId");
        return Guid.TryParse(val, out var id) ? id : Guid.Empty;
    }
}
