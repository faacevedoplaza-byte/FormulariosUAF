using System.Security.Claims;

namespace FormulariosUAF.Helpers;

public static class ClaimsPrincipalExtensions
{
    /// <summary>Nombre a mostrar: el nombre completo (claim "FullName") o, si no hay, el UserName (RUT).</summary>
    public static string? DisplayName(this ClaimsPrincipal user)
        => user.FindFirst("FullName")?.Value ?? user.Identity?.Name;

    /// <summary>Correo del usuario desde el claim de email (vacío si no hay).</summary>
    public static string CorreoUsuario(this ClaimsPrincipal user)
        => user.FindFirst(ClaimTypes.Email)?.Value ?? "";
}
