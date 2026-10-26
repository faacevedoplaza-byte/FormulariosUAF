using FormulariosUAF.Helpers;
using FormulariosUAF.Models.Domain;
using FormulariosUAF.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace FormulariosUAF.Pages.Admin.Usuarios;

/// <summary>
/// Crea usuarios con rol Vendedor a partir de los vendedores vigentes de Netcar (Siglo 21 / VyD), igual que los
/// creados a mano (p. ej. LVERARDI): UserName = abreviación Netcar (ST_ABREVIACION_USUARIO, la que usa
/// LeadsVendedorService; VendorCode vacío), RUT, nombre, correo y la MISMA contraseña de Netcar (copiada al crear,
/// guardada con hash de Identity; si después cambia en Netcar no se actualiza aquí).
/// En Netcar el login (ST_ID_USUARIO) es el RUT; aquí también se puede entrar con el RUT.
/// </summary>
public class ImportarNetcarModel : PageModel
{
    private const string RolVendedor = "Vendedor";

    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IVendedoresNetcarService _netcar;
    private readonly IAuditService _audit;
    private readonly ILogger<ImportarNetcarModel> _logger;

    public ImportarNetcarModel(UserManager<ApplicationUser> userManager, IVendedoresNetcarService netcar,
        IAuditService audit, ILogger<ImportarNetcarModel> logger)
    {
        _userManager = userManager;
        _netcar = netcar;
        _audit = audit;
        _logger = logger;
    }

    [BindProperty(SupportsGet = true)] public string Empresa { get; set; } = "Siglo 21";
    [BindProperty] public List<int> Seleccionados { get; set; } = [];

    public IReadOnlyList<string> Empresas => _netcar.Empresas;
    public List<Fila> Filas { get; private set; } = [];
    public string? ErrorNetcar { get; private set; }
    public List<string> Resultados { get; private set; } = [];

    /// <param name="Existente">Usuario de FormulariosUAF que ya corresponde a este vendedor (no se puede importar).</param>
    /// <param name="Problema">Dato de Netcar que impide crearlo (RUT inválido, sin correo, etc.).</param>
    public record Fila(VendedorNetcar Vendedor, string? Existente, string? Problema)
    {
        public bool Importable => Existente is null && Problema is null;
        /// <summary>Lo que se guardará en FormulariosUAF (misma fuente que CrearAsync).</summary>
        public UsuarioACrear Vista => UsuarioACrear.Desde(Vendedor);
    }

    /// <summary>Datos del usuario que se crea a partir de un vendedor Netcar (vista previa y creación).</summary>
    public record UsuarioACrear(string? UserName, string Rut, string? Nombre, string? ApellidoPaterno,
        string? ApellidoMaterno, string FullName, string? Email)
    {
        public const string Rol = RolVendedor;

        public static UsuarioACrear Desde(VendedorNetcar v) => new(
            v.Abreviacion, RutHelper.Normalizar(v.Rut), v.Nombre, v.ApellidoPaterno, v.ApellidoMaterno,
            v.NombreCompleto, v.Email);
    }

    public async Task<IActionResult> OnGetAsync(CancellationToken ct)
    {
        if (!EmpresaValida()) return RedirectToPage(new { Empresa = Empresas[0] });
        await CargarAsync(ct);
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(CancellationToken ct)
    {
        if (!EmpresaValida()) return RedirectToPage(new { Empresa = Empresas[0] });

        // Se vuelve a leer Netcar: los datos no se toman del formulario.
        await CargarAsync(ct);
        if (ErrorNetcar is not null) return Page();

        var aCrear = Filas.Where(f => f.Importable && Seleccionados.Contains(f.Vendedor.IdUsuario)).ToList();
        if (aCrear.Count == 0)
        {
            Resultados.Add("No se seleccionó ningún vendedor importable.");
            return Page();
        }

        var adminId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
        int creados = 0;
        foreach (var fila in aCrear)
        {
            var v = fila.Vendedor;
            try
            {
                var error = await CrearAsync(v, adminId, ct);
                if (error is null) creados++;
                Resultados.Add(error is null
                    ? $"✔ {v.Abreviacion} ({v.NombreCompleto}) creado."
                    : $"✖ {v.Abreviacion} ({v.NombreCompleto}): {error}");
            }
            catch (Exception ex) when (!ct.IsCancellationRequested)
            {
                _logger.LogError(ex, "Error importando vendedor Netcar {Empresa} {IdUsuario}", Empresa, v.IdUsuario);
                Resultados.Add($"✖ {v.Abreviacion} ({v.NombreCompleto}): error inesperado, ver log.");
            }
        }

        Resultados.Insert(0, $"Se crearon {creados} de {aCrear.Count} usuario(s).");
        await CargarAsync(ct);
        return Page();
    }

    /// <summary>Crea el usuario con la contraseña de Netcar. Devuelve el motivo si no se pudo.</summary>
    private async Task<string?> CrearAsync(VendedorNetcar v, string? adminId, CancellationToken ct)
    {
        var clave = await _netcar.ObtenerClaveAsync(Empresa, v.IdUsuario, ct);
        if (string.IsNullOrEmpty(clave)) return "no tiene contraseña en Netcar (o dejó de estar vigente).";

        var datos = UsuarioACrear.Desde(v);
        var user = new ApplicationUser
        {
            UserName = datos.UserName,
            Email = datos.Email,
            Rut = datos.Rut,
            Nombre = datos.Nombre,
            ApellidoPaterno = datos.ApellidoPaterno,
            ApellidoMaterno = datos.ApellidoMaterno,
            FullName = datos.FullName,
            EmailConfirmed = true,
            IsActive = true
        };

        var result = await _userManager.CreateAsync(user, clave);
        if (!result.Succeeded)
            return string.Join("; ", result.Errors.Select(e => e.Description));

        var asignarRol = await _userManager.AddToRoleAsync(user, UsuarioACrear.Rol);
        if (!asignarRol.Succeeded)
        {
            // Sin rol el usuario no puede entrar a ninguna sección: no dejarlo creado a medias.
            await _userManager.DeleteAsync(user);
            return "no se pudo asignar el rol: " + string.Join("; ", asignarRol.Errors.Select(e => e.Description));
        }

        await _audit.LogAsync("CREAR_USUARIO", "ApplicationUser", user.Id,
            newValues: new { user.UserName, user.Rut, user.Email, Role = RolVendedor, user.VendorCode, Origen = $"Netcar {Empresa} #{v.IdUsuario}" },
            userId: adminId, userName: User.Identity?.Name);
        return null;
    }

    private bool EmpresaValida() =>
        Empresas.Any(e => string.Equals(e, Empresa, StringComparison.OrdinalIgnoreCase));

    private async Task CargarAsync(CancellationToken ct)
    {
        Empresa = Empresas.First(e => string.Equals(e, Empresa, StringComparison.OrdinalIgnoreCase));
        List<VendedorNetcar> vendedores;
        try
        {
            vendedores = await _netcar.ListarAsync(Empresa, ct);
        }
        catch (Exception ex) when (!ct.IsCancellationRequested)
        {
            _logger.LogError(ex, "Error listando vendedores de Netcar {Empresa}", Empresa);
            ErrorNetcar = $"No se pudo consultar Netcar {Empresa}: {ex.Message}";
            Filas = [];
            return;
        }

        var usuarios = await _userManager.Users.AsNoTracking()
            .Select(u => new { u.UserName, u.VendorCode, u.Rut, u.Email })
            .ToListAsync(ct);

        static bool Igual(string? a, string? b) =>
            !string.IsNullOrWhiteSpace(a) && !string.IsNullOrWhiteSpace(b)
            && string.Equals(a.Trim(), b.Trim(), StringComparison.OrdinalIgnoreCase);

        Filas = vendedores.Select(v =>
        {
            var rut = RutHelper.Normalizar(v.Rut);
            var existente = usuarios.FirstOrDefault(u =>
                Igual(u.UserName, v.Abreviacion)
                || Igual(u.VendorCode, v.Abreviacion)
                || (rut.Length > 0 && u.Rut == rut));

            string? problema = null;
            if (existente is null)
            {
                if (string.IsNullOrWhiteSpace(v.Abreviacion)) problema = "Sin abreviación en Netcar.";
                else if (!RutHelper.EsValido(rut)) problema = "RUT inválido o vacío en Netcar.";
                else if (string.IsNullOrWhiteSpace(v.Email)) problema = "Sin correo en Netcar.";
                else if (usuarios.Any(u => Igual(u.Email, v.Email))) problema = "El correo ya lo usa otro usuario.";
            }
            return new Fila(v, existente?.UserName, problema);
        })
        .OrderBy(f => f.Importable ? 0 : 1).ThenBy(f => f.Vendedor.NombreCompleto)
        .ToList();
    }
}
