using FormulariosUAF.Data;
using FormulariosUAF.Models.Domain;
using FormulariosUAF.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace FormulariosUAF.Pages.Cliente;

public class ControllerInput
{
    public string IdNumber { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public string? Address { get; set; }
    public string? City { get; set; }
    public string Country { get; set; } = "Chile";
    public decimal? ParticipationPercentage { get; set; }
    public string ControlDescription { get; set; } = string.Empty;
    public bool IsPEP { get; set; }
    public string? PepDetail { get; set; }
}

public class Paso3Model : PageModel
{
    private readonly ApplicationDbContext _db;
    private readonly IAuditService _audit;

    public Paso3Model(ApplicationDbContext db, IAuditService audit) { _db = db; _audit = audit; }

    public List<ControllerInput> Controllers { get; set; } = [];

    public async Task<IActionResult> OnGetAsync()
    {
        var requestId = GetRequestId();
        if (requestId == Guid.Empty) return RedirectToPage("/Cliente/Inicio");

        Controllers = await _db.EffectiveControllers
            .Where(e => e.RequestId == requestId)
            .OrderBy(e => e.SortOrder)
            .Select(e => new ControllerInput
            {
                IdNumber = e.IdNumber,
                FullName = e.FullName,
                Address = e.Address,
                City = e.City,
                Country = e.Country,
                ParticipationPercentage = e.ParticipationPercentage,
                ControlDescription = e.ControlDescription,
                IsPEP = e.IsPEP,
                PepDetail = e.PepDetail
            }).ToListAsync();

        if (!Controllers.Any()) Controllers.Add(new ControllerInput());
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(List<ControllerInput> controllers, int count)
    {
        var requestId = GetRequestId();
        if (requestId == Guid.Empty) return RedirectToPage("/Cliente/Inicio");

        var existing = await _db.EffectiveControllers.Where(e => e.RequestId == requestId).ToListAsync();
        _db.EffectiveControllers.RemoveRange(existing);

        for (int i = 0; i < (controllers?.Count ?? 0); i++)
        {
            var c = controllers![i];
            if (string.IsNullOrEmpty(c.IdNumber)) continue;
            _db.EffectiveControllers.Add(new EffectiveController
            {
                RequestId = requestId,
                IdNumber = c.IdNumber,
                FullName = c.FullName,
                Address = c.Address,
                City = c.City,
                Country = c.Country,
                ParticipationPercentage = c.ParticipationPercentage,
                ControlDescription = c.ControlDescription,
                IsPEP = c.IsPEP,
                PepDetail = c.IsPEP ? c.PepDetail : null,
                SortOrder = i
            });
        }

        var req = await _db.Requests.FindAsync(requestId);
        if (req != null && req.CurrentStep < 4) req.CurrentStep = 4;

        await _db.SaveChangesAsync();
        await _audit.LogAsync("GUARDAR_PASO3", "EffectiveControllers", requestId.ToString(), requestId: requestId);
        return RedirectToPage("/Cliente/Paso4");
    }

    private Guid GetRequestId()
    {
        var val = HttpContext.Session.GetString("ClientRequestId");
        return Guid.TryParse(val, out var id) ? id : Guid.Empty;
    }
}
