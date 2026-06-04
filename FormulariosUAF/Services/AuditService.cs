using System.Text.Json;
using FormulariosUAF.Data;
using FormulariosUAF.Models.Domain;

namespace FormulariosUAF.Services;

public class AuditService : IAuditService
{
    private readonly ApplicationDbContext _db;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AuditService(ApplicationDbContext db, IHttpContextAccessor httpContextAccessor)
    {
        _db = db;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task LogAsync(string action, string entityType, string? entityId = null,
        object? oldValues = null, object? newValues = null,
        Guid? requestId = null, string? userId = null, string? userName = null)
    {
        var ctx = _httpContextAccessor.HttpContext;
        var ip = ctx?.Connection.RemoteIpAddress?.ToString() ?? "unknown";
        var ua = ctx?.Request.Headers.UserAgent.ToString();

        var log = new AuditLog
        {
            Action = action,
            EntityType = entityType,
            EntityId = entityId,
            OldValues = oldValues is null ? null : JsonSerializer.Serialize(oldValues),
            NewValues = newValues is null ? null : JsonSerializer.Serialize(newValues),
            RequestId = requestId,
            UserId = userId,
            UserName = userName,
            IpAddress = ip,
            UserAgent = ua,
            CreatedAt = DateTime.UtcNow
        };

        _db.AuditLogs.Add(log);
        await _db.SaveChangesAsync();
    }
}
