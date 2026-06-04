using FormulariosUAF.Data;
using FormulariosUAF.Models.Domain;
using FormulariosUAF.Models.Enums;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace FormulariosUAF.Pages.Cumplimiento;

public class IndexModel : PageModel
{
    private readonly ApplicationDbContext _db;

    public IndexModel(ApplicationDbContext db) => _db = db;

    public List<Request> Requests { get; set; } = [];
    public CumplimientoStats Stats { get; set; } = new();
    public string? FilterRut { get; set; }
    public int? FilterEstado { get; set; }

    public async Task OnGetAsync(string? rut, int? estado)
    {
        FilterRut = rut;
        FilterEstado = estado;

        var query = _db.Requests
            .Include(r => r.Client)
            .Include(r => r.VendorUser)
            .Include(r => r.BeneficialOwners)
            .Include(r => r.PepDeclaration)
            .Where(r => r.Status >= RequestStatus.CompletadaPorCliente && r.Status != RequestStatus.Aprobada)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(rut))
            query = query.Where(r => r.Client.RUT.Contains(rut));

        if (estado.HasValue)
            query = query.Where(r => (int)r.Status == estado.Value);

        Requests = await query.OrderByDescending(r => r.CompletedAt).Take(200).ToListAsync();

        var now = DateTime.UtcNow;
        var firstOfMonth = new DateTime(now.Year, now.Month, 1);

        Stats = new CumplimientoStats
        {
            Pendientes = await _db.Requests.CountAsync(r =>
                r.Status == RequestStatus.CompletadaPorCliente || r.Status == RequestStatus.EnRevision || r.Status == RequestStatus.CorregidaPorCliente),
            Aprobadas = await _db.Requests.CountAsync(r =>
                r.Status == RequestStatus.Aprobada && r.UpdatedAt >= firstOfMonth),
            Observadas = await _db.Requests.CountAsync(r => r.Status == RequestStatus.Observada),
            ConPEP = await _db.Requests.CountAsync(r =>
                r.PepDeclaration != null && r.PepDeclaration.DeclaresPEP)
        };
    }
}

public class CumplimientoStats
{
    public int Pendientes { get; set; }
    public int Aprobadas { get; set; }
    public int Observadas { get; set; }
    public int ConPEP { get; set; }
}
