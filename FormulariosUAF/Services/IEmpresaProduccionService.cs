namespace FormulariosUAF.Services;

/// <summary>
/// Datos de una empresa obtenidos desde la base de producción
/// mediante el procedimiento almacenado sp_UAF_BuscarEmpresaPorRut.
/// </summary>
public class EmpresaProduccionDto
{
    public string? RutEmpresa { get; set; }
    public string? RazonSocial { get; set; }
    public string? Domicilio { get; set; }
    public string? Ciudad { get; set; }
    public string? PaisConstitucion { get; set; }
    public string? Telefono { get; set; }
    public string? TipoEntidad { get; set; }
    public string? RutRepresentanteLegal { get; set; }
    public string? NombreRepresentanteLegal { get; set; }
    public string? Email { get; set; }
}

public interface IEmpresaProduccionService
{
    /// <summary>Busca una empresa por RUT en la base de producción (solo lectura).</summary>
    Task<EmpresaProduccionDto?> BuscarPorRutAsync(string rut, CancellationToken ct = default);
}
