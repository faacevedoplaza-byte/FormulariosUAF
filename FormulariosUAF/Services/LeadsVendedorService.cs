using System.Security.Claims;
using FormulariosUAF.Models.Domain;
using Microsoft.Extensions.Caching.Memory;

namespace FormulariosUAF.Services;

/// <summary>Operaciones de un vendedor y, si Netcar no respondió, el aviso para mostrar.</summary>
public record LeadsVendedor(HashSet<int> OperacionIds, string? AvisoNetcar);

public interface ILeadsVendedorService
{
    /// <summary>
    /// Operaciones RegCheq creadas en el rango cuya cotización Netcar (Siglo 21 o VyD) tiene como vendedor a
    /// <paramref name="codigo"/>. Con caché corta: la vista pagina y ordena sin volver a cruzar todo.
    /// </summary>
    Task<LeadsVendedor> OperacionesAsync(RegcheqTipo tipo, string? codigo, DateTime? desde, DateTime? hasta, CancellationToken ct = default);

    /// <summary>De entre <paramref name="operacionIds"/>, las que son leads de <paramref name="codigo"/>.</summary>
    Task<LeadsVendedor> FiltrarAsync(RegcheqTipo tipo, IReadOnlyCollection<int> operacionIds, string? codigo, CancellationToken ct = default);
}

/// <summary>
/// Vista de vendedor en /Regcheq: un usuario con rol Vendedor (y sin rol que vea todo) solo ve las operaciones
/// de sus leads. El vínculo es el vendedor de la cotización Netcar (T_ACC_USUARIO.ST_ABREVIACION_USUARIO, p. ej.
/// SLARA) contra el login de FormulariosUAF, o contra VendorCode si está definido.
/// </summary>
public class LeadsVendedorService : ILeadsVendedorService
{
    /// <summary>Roles que ven todas las operaciones RegCheq.</summary>
    public static readonly string[] RolesTodas = ["Administrador", "Cumplimiento", "Revisor", "Call Center"];
    private static readonly TimeSpan DuracionCache = TimeSpan.FromMinutes(2);

    private readonly IRegcheqService _regcheq;
    private readonly IVehiculoOperacionService _vehiculos;
    private readonly IMemoryCache _cache;

    public LeadsVendedorService(IRegcheqService regcheq, IVehiculoOperacionService vehiculos, IMemoryCache cache)
    {
        _regcheq = regcheq;
        _vehiculos = vehiculos;
        _cache = cache;
    }

    public static bool EsSoloVendedor(ClaimsPrincipal user) =>
        user.IsInRole("Vendedor") && !RolesTodas.Any(user.IsInRole);

    /// <summary>
    /// Usuario Netcar del vendedor: VendorCode si está definido (cuando el login no coincide con Netcar);
    /// si no, el login (UserName), que normalmente es la misma abreviatura de Netcar.
    /// </summary>
    public static string? CodigoNetcar(ApplicationUser u) =>
        !string.IsNullOrWhiteSpace(u.VendorCode) ? u.VendorCode.Trim() : u.UserName?.Trim();

    public async Task<LeadsVendedor> OperacionesAsync(RegcheqTipo tipo, string? codigo, DateTime? desde, DateTime? hasta, CancellationToken ct = default)
    {
        if (string.IsNullOrWhiteSpace(codigo)) return new([], null);

        var clave = $"leads-vendedor:{tipo}:{codigo.ToUpperInvariant()}:{desde:yyyyMMdd}:{hasta:yyyyMMdd}";
        if (_cache.TryGetValue(clave, out LeadsVendedor? enCache) && enCache is not null) return enCache;

        var cotizaciones = await _regcheq.ListarCotizacionesAsync(tipo, desde, hasta, ct);
        if (cotizaciones is null)
            return new([], "No se pudo consultar RegCheq para identificar sus operaciones. Intente nuevamente.");

        var resultado = Propias(await _vehiculos.ObtenerAsync(cotizaciones, ct), codigo);
        // Si Netcar falló, no se guarda: el próximo intento vuelve a consultar.
        if (resultado.AvisoNetcar is null) _cache.Set(clave, resultado, DuracionCache);
        return resultado;
    }

    public async Task<LeadsVendedor> FiltrarAsync(RegcheqTipo tipo, IReadOnlyCollection<int> operacionIds, string? codigo, CancellationToken ct = default)
    {
        if (operacionIds.Count == 0 || string.IsNullOrWhiteSpace(codigo)) return new([], null);
        return Propias(await _vehiculos.ObtenerAsync(tipo, operacionIds, ct), codigo);
    }

    private static LeadsVendedor Propias(Dictionary<int, VehiculoOperacion> vehiculos, string codigo)
    {
        var ids = vehiculos
            .Where(kv => string.Equals(kv.Value.Vehiculo?.Vendedor, codigo, StringComparison.OrdinalIgnoreCase))
            .Select(kv => kv.Key)
            .ToHashSet();
        var aviso = vehiculos.Values.FirstOrDefault(v => v.NetcarNoDisponible) is { } caida
            ? $"{caida.Motivo} Pueden faltar operaciones suyas; intente nuevamente en unos minutos."
            : null;
        return new(ids, aviso);
    }
}
