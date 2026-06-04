namespace FormulariosUAF.Models.Domain;

public class Client
{
    public int Id { get; set; }
    public string RUT { get; set; } = string.Empty;
    public string BusinessName { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public string CreatedBy { get; set; } = string.Empty;
    public DateTime? UpdatedAt { get; set; }
    public string? UpdatedBy { get; set; }

    public ICollection<Request> Requests { get; set; } = new List<Request>();
}
