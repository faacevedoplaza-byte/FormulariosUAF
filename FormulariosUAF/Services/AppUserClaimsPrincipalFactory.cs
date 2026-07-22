using System.Security.Claims;
using FormulariosUAF.Models.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace FormulariosUAF.Services;

/// <summary>
/// Agrega al usuario autenticado los claims "FullName" (nombre completo) y Email,
/// para poder mostrar el NOMBRE en el panel/gestiones/correos aunque el login sea por RUT.
/// </summary>
public class AppUserClaimsPrincipalFactory : UserClaimsPrincipalFactory<ApplicationUser, IdentityRole>
{
    public AppUserClaimsPrincipalFactory(
        UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager,
        IOptions<IdentityOptions> options)
        : base(userManager, roleManager, options)
    {
    }

    protected override async Task<ClaimsIdentity> GenerateClaimsAsync(ApplicationUser user)
    {
        var identity = await base.GenerateClaimsAsync(user);

        if (!string.IsNullOrWhiteSpace(user.FullName))
            identity.AddClaim(new Claim("FullName", user.FullName));

        if (!string.IsNullOrWhiteSpace(user.Email) && identity.FindFirst(ClaimTypes.Email) is null)
            identity.AddClaim(new Claim(ClaimTypes.Email, user.Email));

        return identity;
    }
}
