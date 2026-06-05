using FormulariosUAF.Models.Domain;
using FormulariosUAF.Models.Enums;

namespace FormulariosUAF.Services;

public interface INotificationService
{
    Task CreateAsync(string userId, NotificationType type, string message, Guid? requestId = null);
    Task<List<Notification>> GetByUserAsync(string userId, bool onlyUnread = false, int take = 50);
    Task<int> GetUnreadCountAsync(string userId);
    Task MarkAsReadAsync(int notificationId, string userId);
    Task MarkAllAsReadAsync(string userId);
}
