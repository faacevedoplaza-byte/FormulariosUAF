namespace FormulariosUAF.Pages.Cliente;

// Kept for backward compatibility with _BeneficiarioRow.cshtml (legacy partial, no longer active)
public class BeneficiarioRowModel
{
    public int Index { get; set; }
    public BeneficiarioRowData Data { get; set; } = new();
}

public class BeneficiarioRowData
{
    public string IdNumber { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public string? Address { get; set; }
    public string? City { get; set; }
    public string Country { get; set; } = "Chile";
    public decimal ParticipationPercentage { get; set; }
    public bool IsEffectiveControl { get; set; }
    public bool IsPEP { get; set; }
    public string? PepDetail { get; set; }
}
