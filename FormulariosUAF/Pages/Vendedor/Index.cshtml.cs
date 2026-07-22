using FormulariosUAF.Data;
using FormulariosUAF.Helpers;
using FormulariosUAF.Models.Domain;
using FormulariosUAF.Models.Enums;
using FormulariosUAF.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace FormulariosUAF.Pages.Vendedor;

public class IndexModel : PageModel
{
    private readonly ApplicationDbContext _db;
    private readonly IAuditService _audit;

    public IndexModel(ApplicationDbContext db, IAuditService audit)
    {
        _db = db;
        _audit = audit;
    }

    public List<Request> Requests { get; set; } = [];
    [BindProperty(SupportsGet = true)] public string? FilterRut { get; set; }
    [BindProperty(SupportsGet = true)] public string? FilterRazon { get; set; }
    [BindProperty(SupportsGet = true)] public int? FilterEstado { get; set; }

    public async Task OnGetAsync()
    {
        var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
        var isAdmin = User.IsInRole("Administrador");

        var query = _db.Requests.Include(r => r.Client).AsQueryable();

        if (!isAdmin)
            query = query.Where(r => r.VendorUserId == userId);

        if (!string.IsNullOrWhiteSpace(FilterRut))
            query = query.Where(r => r.Client.RUT.Contains(FilterRut));

        if (!string.IsNullOrWhiteSpace(FilterRazon))
            query = query.Where(r => r.Client.BusinessName.Contains(FilterRazon));

        if (FilterEstado.HasValue)
            query = query.Where(r => (int)r.Status == FilterEstado.Value);

        Requests = await query.OrderByDescending(r => r.CreatedAt).Take(200).ToListAsync();
    }

    public async Task<IActionResult> OnPostEnviarAsync(Guid id)
    {
        var request = await _db.Requests.FindAsync(id);
        if (request is null) return NotFound();

        request.Status = RequestStatus.EnviadaAlCliente;
        request.SentAt = DateTime.UtcNow;
        request.UpdatedAt = DateTime.UtcNow;

        _db.RequestStatusHistories.Add(new RequestStatusHistory
        {
            RequestId = id,
            OldStatus = RequestStatus.Borrador,
            NewStatus = RequestStatus.EnviadaAlCliente,
            ChangedBy = User.DisplayName() ?? "sistema"
        });

        await _db.SaveChangesAsync();
        await _audit.LogAsync("ENVIAR_SOLICITUD", "Request", id.ToString(), requestId: id,
            userName: User.Identity?.Name);

        return new JsonResult(new { ok = true });
    }
}
