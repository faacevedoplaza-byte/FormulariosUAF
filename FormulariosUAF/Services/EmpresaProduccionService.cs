using System.Data;
using Microsoft.Data.SqlClient;

namespace FormulariosUAF.Services;

/// <summary>
/// Ejecuta el procedimiento almacenado sp_UAF_BuscarEmpresaPorRut en la base de
/// producción usando la cadena 'ProduccionConnection'. Solo lectura: únicamente
/// ejecuta el SP, nunca hace INSERT/UPDATE/DELETE.
/// </summary>
public class EmpresaProduccionService : IEmpresaProduccionService
{
    private readonly string? _connectionString;
    private readonly ILogger<EmpresaProduccionService> _logger;

    public EmpresaProduccionService(IConfiguration config, ILogger<EmpresaProduccionService> logger)
    {
        _connectionString = config.GetConnectionString("ProduccionConnection");
        _logger = logger;
    }

    public async Task<EmpresaProduccionDto?> BuscarPorRutAsync(string rut, CancellationToken ct = default)
    {
        if (string.IsNullOrWhiteSpace(_connectionString))
        {
            _logger.LogWarning("ProduccionConnection no está configurada. Búsqueda por RUT omitida.");
            return null;
        }

        try
        {
            await using var conn = new SqlConnection(_connectionString);
            await using var cmd = new SqlCommand("dbo.sp_UAF_BuscarEmpresaPorRut", conn)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.Add(new SqlParameter("@RutEmpresa", SqlDbType.VarChar, 20) { Value = rut });

            await conn.OpenAsync(ct);
            await using var reader = await cmd.ExecuteReaderAsync(ct);
            if (!await reader.ReadAsync(ct)) return null;

            return new EmpresaProduccionDto
            {
                RutEmpresa = GetStr(reader, "RutEmpresa"),
                RazonSocial = GetStr(reader, "RazonSocial"),
                Domicilio = GetStr(reader, "Domicilio"),
                Ciudad = GetStr(reader, "Ciudad"),
                PaisConstitucion = GetStr(reader, "PaisConstitucion"),
                Telefono = GetStr(reader, "Telefono"),
                TipoEntidad = GetStr(reader, "TipoEntidad"),
                RutRepresentanteLegal = GetStr(reader, "RutRepresentanteLegal"),
                NombreRepresentanteLegal = GetStr(reader, "NombreRepresentanteLegal"),
                Email = GetStr(reader, "Email")
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error consultando empresa por RUT {Rut} en producción", rut);
            return null;
        }
    }

    private static string? GetStr(SqlDataReader r, string column)
    {
        var i = r.GetOrdinal(column);
        return r.IsDBNull(i) ? null : r.GetValue(i)?.ToString()?.Trim();
    }
}
