namespace FormulariosUAF.Models.Domain;

public class AuditLog
{
    public long Id { get; set; }
    public Guid? RequestId { get; set; }
    public Request? Request { get; set; }

    public string Action { get; set; } = string.Empty;
    public string EntityType { get; set; } = string.Empty;
    public string? EntityId { get; set; }
    public string? OldValues { get; set; }
    public string? NewValues { get; set; }

    public string? UserId { get; set; }
    public string? UserName { get; set; }
    public string IpAddress { get; set; } = string.Empty;
    public string? UserAgent { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
