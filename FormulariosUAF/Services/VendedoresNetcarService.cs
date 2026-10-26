using Microsoft.Data.SqlClient;

namespace FormulariosUAF.Services;

/// <summary>Vendedor vigente de Netcar (T_ACC_USUARIO), sin contraseña.</summary>
public class VendedorNetcar
{
    public int IdUsuario { get; set; }
    /// <summary>"Siglo 21" o "VyD": base Netcar de donde salió.</summary>
    public string Empresa { get; set; } = "";
    /// <summary>Login de Netcar (ST_ID_USUARIO).</summary>
    public string Login { get; set; } = "";
    /// <summary>ST_ABREVIACION_USUARIO: el "Vendedor" de las cotizaciones (lo usa LeadsVendedorService).</summary>
    public string? Abreviacion { get; set; }
    public string? Rut { get; set; }
    public string? Nombre { get; set; }
    public string? ApellidoPaterno { get; set; }
    public string? ApellidoMaterno { get; set; }
    public string? Email { get; set; }
    public string? Telefono { get; set; }
    public DateTime? Vigencia { get; set; }

    public string NombreCompleto => string.Join(' ',
        new[] { Nombre, ApellidoPaterno, ApellidoMaterno }.Where(s => !string.IsNullOrWhiteSpace(s)));
}

public interface IVendedoresNetcarService
{
    /// <summary>Bases Netcar disponibles ("Siglo 21", "VyD").</summary>
    IReadOnlyList<string> Empresas { get; }

    /// <summary>Vendedores vigentes de una base Netcar. Lanza si la consulta falla.</summary>
    Task<List<VendedorNetcar>> ListarAsync(string empresa, CancellationToken ct = default);

    /// <summary>Contraseña Netcar de un vendedor vigente (null si no existe). Usar solo para crear el usuario; no mostrar ni registrar.</summary>
    Task<string?> ObtenerClaveAsync(string empresa, int idUsuario, CancellationToken ct = default);
}

/// <summary>
/// Lee los vendedores de Netcar con dbo.sp_UAF_VendedoresNetcar / dbo.sp_UAF_ClaveVendedorNetcar
/// (SQL/Netcar_03_sp_UAF_VendedoresNetcar.sql). Mismas conexiones que VehiculoOperacionService.
/// </summary>
public class VendedoresNetcarService : IVendedoresNetcarService
{
    private const int TimeoutSegundos = 15;
    private const string Siglo21 = "Siglo 21";
    private const string VyD = "VyD";

    private readonly Dictionary<string, string?> _conexiones;

    public VendedoresNetcarService(IConfiguration config)
    {
        _conexiones = new(StringComparer.OrdinalIgnoreCase)
        {
            [Siglo21] = config.GetConnectionString("NetcarSiglo21Connection") ?? config.GetConnectionString("ProduccionConnection"),
            [VyD] = config.GetConnectionString("NetcarVyDConnection")
        };
    }

    public IReadOnlyList<string> Empresas { get; } = [Siglo21, VyD];

    public async Task<List<VendedorNetcar>> ListarAsync(string empresa, CancellationToken ct = default)
    {
        await using var conn = await AbrirAsync(empresa, ct);
        await using var cmd = new SqlCommand("dbo.sp_UAF_VendedoresNetcar", conn)
        {
            CommandType = System.Data.CommandType.StoredProcedure,
            CommandTimeout = TimeoutSegundos
        };
        var lista = new List<VendedorNetcar>();
        await using var r = await cmd.ExecuteReaderAsync(ct);
        while (await r.ReadAsync(ct))
        {
            lista.Add(new VendedorNetcar
            {
                IdUsuario = Convert.ToInt32(r["IdUsuario"]),
                Empresa = NombreEmpresa(empresa),
                Login = Texto(r["Login"]) ?? "",
                Abreviacion = Texto(r["Abreviacion"]),
                Rut = Texto(r["Rut"]),
                Nombre = Texto(r["Nombre"]),
                ApellidoPaterno = Texto(r["ApellidoPaterno"]),
                ApellidoMaterno = Texto(r["ApellidoMaterno"]),
                Email = Texto(r["Email"]),
                Telefono = Texto(r["Telefono"]),
                Vigencia = r["Vigencia"] as DateTime?
            });
        }
        return lista;
    }

    public async Task<string?> ObtenerClaveAsync(string empresa, int idUsuario, CancellationToken ct = default)
    {
        await using var conn = await AbrirAsync(empresa, ct);
        await using var cmd = new SqlCommand("dbo.sp_UAF_ClaveVendedorNetcar", conn)
        {
            CommandType = System.Data.CommandType.StoredProcedure,
            CommandTimeout = TimeoutSegundos
        };
        cmd.Parameters.Add(new SqlParameter("@IdUsuario", System.Data.SqlDbType.Int) { Value = idUsuario });
        // Sin Trim: la contraseña se copia tal cual está en Netcar.
        return await cmd.ExecuteScalarAsync(ct) is string clave ? clave : null;
    }

    private async Task<SqlConnection> AbrirAsync(string empresa, CancellationToken ct)
    {
        if (!_conexiones.TryGetValue(empresa, out var conexion) || string.IsNullOrWhiteSpace(conexion))
            throw new InvalidOperationException($"Conexión a Netcar {empresa} no configurada (NetcarSiglo21Connection / NetcarVyDConnection).");
        var conn = new SqlConnection(conexion);
        await conn.OpenAsync(ct);
        return conn;
    }

    private string NombreEmpresa(string empresa) =>
        Empresas.First(e => string.Equals(e, empresa, StringComparison.OrdinalIgnoreCase));

    private static string? Texto(object valor)
    {
        var s = valor is DBNull ? null : valor?.ToString()?.Trim();
        return string.IsNullOrEmpty(s) ? null : s;
    }
}
