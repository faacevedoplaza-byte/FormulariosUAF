namespace FormulariosUAF.Models.Domain;

public class EffectiveController
{
    public int Id { get; set; }
    public Guid RequestId { get; set; }
    public Request Request { get; set; } = null!;

    public string IdNumber { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public string? Address { get; set; }
    public string? City { get; set; }
    public string Country { get; set; } = "Chile";
    public decimal? ParticipationPercentage { get; set; }

    public bool IsPEP { get; set; }
    public string? PepDetail { get; set; }
    public string ControlDescription { get; set; } = string.Empty;

    public int SortOrder { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
}
