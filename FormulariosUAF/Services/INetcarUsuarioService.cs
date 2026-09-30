namespace FormulariosUAF.Services;

/// <summary>
/// Datos de un usuario de Netcar (tabla T_ACC_USUARIO en netcarsiglo21com)
/// obtenidos por RUT. Solo lectura.
/// </summary>
public class NetcarUsuarioDto
{
    public string? Rut { get; set; }
    public string? NombreCompleto { get; set; }
    public string? Email { get; set; }
    public string? Grupo { get; set; }
    public bool Vigente { get; set; }
}

public interface INetcarUsuarioService
{
    /// <summary>Busca un usuario de Netcar por RUT (solo lectura). Null si no existe.</summary>
    Task<NetcarUsuarioDto?> BuscarPorRutAsync(string rut, CancellationToken ct = default);
}
