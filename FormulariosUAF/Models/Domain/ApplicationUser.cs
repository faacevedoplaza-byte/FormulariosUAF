using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace FormulariosUAF.Models.Domain;

public class ApplicationUser : IdentityUser
{
    /// <summary>RUT normalizado (sin puntos ni guión). Coincide con el UserName y es el identificador de login.</summary>
    [MaxLength(20)]
    public string? Rut { get; set; }

    [MaxLength(100)]
    public string? Nombre { get; set; }

    [MaxLength(100)]
    public string? ApellidoPaterno { get; set; }

    [MaxLength(100)]
    public string? ApellidoMaterno { get; set; }

    /// <summary>Nombre completo compuesto (Nombre + apellidos). Se mantiene para listados y código existente.</summary>
    public string FullName { get; set; } = string.Empty;
    public string? VendorCode { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? LastLoginAt { get; set; }

    public ICollection<Request> Requests { get; set; } = new List<Request>();
}
