using FormulariosUAF.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FormulariosUAF.Pages.Api;

[Authorize]
public class NotificacionesCountModel : PageModel
{
    private readonly INotificationService _notifications;

    public NotificacionesCountModel(INotificationService notifications) => _notifications = notifications;

    public async Task<IActionResult> OnGetAsync()
    {
        var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value ?? "";
        var count = await _notifications.GetUnreadCountAsync(userId);
        return new JsonResult(new { count });
    }
}
