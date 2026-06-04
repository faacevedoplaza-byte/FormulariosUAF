using FormulariosUAF.Models.Enums;

namespace FormulariosUAF.Models.Domain;

public class RequestStatusHistory
{
    public int Id { get; set; }
    public Guid RequestId { get; set; }
    public Request Request { get; set; } = null!;

    public RequestStatus OldStatus { get; set; }
    public RequestStatus NewStatus { get; set; }
    public string ChangedBy { get; set; } = string.Empty;
    public string? Notes { get; set; }
    public DateTime ChangedAt { get; set; } = DateTime.UtcNow;
}
