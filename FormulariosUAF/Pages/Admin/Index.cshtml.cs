using FormulariosUAF.Data;
using FormulariosUAF.Models.Domain;
using FormulariosUAF.Models.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace FormulariosUAF.Pages.Admin;

public class IndexModel : PageModel
{
    private readonly ApplicationDbContext _db;
    private readonly UserManager<ApplicationUser> _userManager;

    public IndexModel(ApplicationDbContext db, UserManager<ApplicationUser> userManager)
    {
        _db = db;
        _userManager = userManager;
    }

    public AdminStats Stats { get; set; } = new();
    public List<UserViewModel> Users { get; set; } = [];
    public List<AuditLog> RecentLogs { get; set; } = [];

    public async Task OnGetAsync()
    {
        Stats = new AdminStats
        {
            TotalSolicitudes = await _db.Requests.CountAsync(),
            Aprobadas = await _db.Requests.CountAsync(r => r.Status == RequestStatus.Aprobada),
            Pendientes = await _db.Requests.CountAsync(r =>
                r.Status >= RequestStatus.EnviadaAlCliente && r.Status < RequestStatus.Aprobada),
            Vencidas = await _db.Requests.CountAsync(r => r.Status == RequestStatus.Vencida)
        };

        var users = await _userManager.Users.Where(u => u.IsActive).OrderBy(u => u.FullName).ToListAsync();
        Users = [];
        foreach (var u in users)
        {
            var roles = await _userManager.GetRolesAsync(u);
            Users.Add(new UserViewModel(u.FullName, u.Email ?? "", [.. roles], u.IsActive));
        }

        RecentLogs = await _db.AuditLogs
            .OrderByDescending(l => l.CreatedAt)
            .Take(20)
            .ToListAsync();
    }
}

public class AdminStats
{
    public int TotalSolicitudes { get; set; }
    public int Aprobadas { get; set; }
    public int Pendientes { get; set; }
    public int Vencidas { get; set; }
}

public record UserViewModel(string FullName, string Email, List<string> Roles, bool IsActive);
