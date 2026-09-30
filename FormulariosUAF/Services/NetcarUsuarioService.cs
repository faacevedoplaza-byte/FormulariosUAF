using System.Data;
using Microsoft.Data.SqlClient;

namespace FormulariosUAF.Services;

/// <summary>
/// Consulta usuarios de Netcar (tabla T_ACC_USUARIO en netcarsiglo21com) usando la
/// cadena 'NetcarUsuariosConnection'. Solo lectura: únicamente hace SELECT.
/// </summary>
public class NetcarUsuarioService : INetcarUsuarioService
{
    private readonly string? _connectionString;
    private readonly ILogger<NetcarUsuarioService> _logger;

    public NetcarUsuarioService(IConfiguration config, ILogger<NetcarUsuarioService> logger)
    {
        _connectionString = config.GetConnectionString("NetcarUsuariosConnection");
        _logger = logger;
    }

    public async Task<NetcarUsuarioDto?> BuscarPorRutAsync(string rut, CancellationToken ct = default)
    {
        if (string.IsNullOrWhiteSpace(_connectionString))
        {
            _logger.LogWarning("NetcarUsuariosConnection no está configurada. Búsqueda de usuario Netcar omitida.");
            return null;
        }

        var rutNormalizado = NormalizarRut(rut);
        if (string.IsNullOrEmpty(rutNormalizado)) return null;

        // El RUT en Netcar (ST_RUT_USUARIO) se guarda sin puntos ni guion; se normaliza
        // también la columna con REPLACE por si hubiera datos con formato.
        const string sql = @"
SELECT TOP 1
    u.ST_RUT_USUARIO AS Rut,
    LTRIM(RTRIM(u.ST_NOMBRE_USUARIO + ' ' + ISNULL(u.ST_APEPAT_USUARIO,'') + ' ' + ISNULL(u.ST_APEMAT_USUARIO,''))) AS NombreCompleto,
    u.ST_EMAIL_USUARIO AS Email,
    g.ST_DESC_GRUPO AS Grupo,
    CASE WHEN u.DT_FECHAVIGENCIA_USUARIO IS NULL OR u.DT_FECHAVIGENCIA_USUARIO >= CAST(GETDATE() AS date) THEN 1 ELSE 0 END AS Vigente
FROM T_ACC_USUARIO u
LEFT JOIN T_ACC_GRUPO g ON g.IN_COD_GRUPO = u.IN_COD_GRUPO
WHERE REPLACE(REPLACE(REPLACE(u.ST_RUT_USUARIO, '.', ''), '-', ''), ' ', '') = @rut";

        try
        {
            await using var conn = new SqlConnection(_connectionString);
            await using var cmd = new SqlCommand(sql, conn) { CommandType = CommandType.Text };
            cmd.Parameters.Add(new SqlParameter("@rut", SqlDbType.VarChar, 13) { Value = rutNormalizado });

            await conn.OpenAsync(ct);
            await using var reader = await cmd.ExecuteReaderAsync(ct);
            if (!await reader.ReadAsync(ct)) return null;

            return new NetcarUsuarioDto
            {
                Rut = GetStr(reader, "Rut"),
                NombreCompleto = GetStr(reader, "NombreCompleto"),
                Email = GetStr(reader, "Email"),
                Grupo = GetStr(reader, "Grupo"),
                Vigente = reader.GetInt32(reader.GetOrdinal("Vigente")) == 1
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error consultando usuario Netcar por RUT {Rut}", rutNormalizado);
            return null;
        }
    }

    /// <summary>Deja el RUT solo con dígitos + DV (sin puntos, guion ni espacios), DV en mayúscula.</summary>
    public static string NormalizarRut(string? rut)
    {
        if (string.IsNullOrWhiteSpace(rut)) return string.Empty;
        return new string(rut.Where(c => !char.IsWhiteSpace(c) && c != '.' && c != '-').ToArray()).ToUpperInvariant();
    }

    private static string? GetStr(SqlDataReader r, string column)
    {
        var i = r.GetOrdinal(column);
        return r.IsDBNull(i) ? null : r.GetValue(i)?.ToString()?.Trim();
    }
}
