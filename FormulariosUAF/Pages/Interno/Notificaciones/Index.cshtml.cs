using FormulariosUAF.Models.Domain;
using FormulariosUAF.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FormulariosUAF.Pages.Interno.Notificaciones;

public class IndexModel : PageModel
{
    private readonly INotificationService _notifications;

    public IndexModel(INotificationService notifications) => _notifications = notifications;

    public List<Notification> Notifications { get; set; } = [];

    public async Task OnGetAsync()
    {
        var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value ?? "";
        Notifications = await _notifications.GetByUserAsync(userId, take: 100);
    }

    public async Task<IActionResult> OnPostMarkReadAsync(int id)
    {
        var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value ?? "";
        await _notifications.MarkAsReadAsync(id, userId);
        return RedirectToPage();
    }

    public async Task<IActionResult> OnPostMarkAllAsync()
    {
        var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value ?? "";
        await _notifications.MarkAllAsReadAsync(userId);
        return RedirectToPage();
    }
}
