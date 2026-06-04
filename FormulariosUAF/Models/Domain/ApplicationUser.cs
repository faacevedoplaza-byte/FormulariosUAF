using Microsoft.AspNetCore.Identity;

namespace FormulariosUAF.Models.Domain;

public class ApplicationUser : IdentityUser
{
    public string FullName { get; set; } = string.Empty;
    public string? VendorCode { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? LastLoginAt { get; set; }

    public ICollection<Request> Requests { get; set; } = new List<Request>();
}
