using FormulariosUAF.Models.Domain;
using Microsoft.AspNetCore.Identity;

namespace FormulariosUAF.Data;

public static class SeedData
{
    // Roles del sistema
    public static readonly string[] Roles =
    [
        "Administrador",
        "Vendedor",
        "Cumplimiento",
        "Revisor",       // Fase 2: revisor interno sin aprobar
        "SoloLectura"    // Fase 2: consulta sin modificar
    ];

    public static async Task InitializeAsync(IServiceProvider services)
    {
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
        var db = services.GetRequiredService<ApplicationDbContext>();

        await db.Database.EnsureCreatedAsync();

        foreach (var role in Roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
                await roleManager.CreateAsync(new IdentityRole(role));
        }

        // Usuarios de prueba / seed
        await CreateUserAsync(userManager, "admin@uaf.cl",         "Admin@123!",         "Administrador del Sistema", "Administrador");
        await CreateUserAsync(userManager, "vendedor@uaf.cl",      "Vendedor@123!",      "Vendedor Demo",             "Vendedor");
        await CreateUserAsync(userManager, "cumplimiento@uaf.cl",  "Cumplimiento@123!",  "Oficial de Cumplimiento",   "Cumplimiento");
        await CreateUserAsync(userManager, "revisor@uaf.cl",       "Revisor@123!",       "Revisor Interno",           "Revisor");
    }

    private static async Task CreateUserAsync(UserManager<ApplicationUser> userManager,
        string email, string password, string fullName, string role)
    {
        if (await userManager.FindByEmailAsync(email) is not null) return;

        var user = new ApplicationUser
        {
            UserName = email,
            Email = email,
            FullName = fullName,
            EmailConfirmed = true,
            IsActive = true
        };

        var result = await userManager.CreateAsync(user, password);
        if (result.Succeeded)
            await userManager.AddToRoleAsync(user, role);
    }
}
