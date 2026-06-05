using FormulariosUAF.Data;
using FormulariosUAF.Hubs;
using FormulariosUAF.Models.Domain;
using FormulariosUAF.Models.Enums;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace FormulariosUAF.Services;

public class NotificationService : INotificationService
{
    private readonly ApplicationDbContext _db;
    private readonly IHubContext<NotificationHub> _hub;

    public NotificationService(ApplicationDbContext db, IHubContext<NotificationHub> hub)
    {
        _db = db;
        _hub = hub;
    }

    public async Task CreateAsync(string userId, NotificationType type, string message, Guid? requestId = null)
    {
        var notification = new Notification
        {
            UserId = userId,
            Type = type,
            Message = message,
            RequestId = requestId,
            IsRead = false,
            CreatedAt = DateTime.UtcNow
        };

        _db.Notifications.Add(notification);
        await _db.SaveChangesAsync();

        // Push en tiempo real si el usuario está conectado
        try
        {
            await _hub.Clients.User(userId).SendAsync("NewNotification", new
            {
                notification.Id,
                notification.Message,
                Type = type.ToString(),
                notification.RequestId,
                Fecha = notification.CreatedAt.ToString("dd/MM HH:mm")
            });
        }
        catch { /* SignalR es best-effort; no bloquear el flujo */ }
    }

    public Task<List<Notification>> GetByUserAsync(string userId, bool onlyUnread = false, int take = 50)
    {
        var query = _db.Notifications.Where(n => n.UserId == userId);
        if (onlyUnread) query = query.Where(n => !n.IsRead);
        return query.OrderByDescending(n => n.CreatedAt).Take(take).ToListAsync();
    }

    public Task<int> GetUnreadCountAsync(string userId)
        => _db.Notifications.CountAsync(n => n.UserId == userId && !n.IsRead);

    public async Task MarkAsReadAsync(int notificationId, string userId)
    {
        var n = await _db.Notifications.FirstOrDefaultAsync(x => x.Id == notificationId && x.UserId == userId);
        if (n is null || n.IsRead) return;
        n.IsRead = true;
        n.ReadAt = DateTime.UtcNow;
        await _db.SaveChangesAsync();
    }

    public async Task MarkAllAsReadAsync(string userId)
    {
        await _db.Notifications
            .Where(n => n.UserId == userId && !n.IsRead)
            .ExecuteUpdateAsync(s => s
                .SetProperty(n => n.IsRead, true)
                .SetProperty(n => n.ReadAt, DateTime.UtcNow));
    }
}
