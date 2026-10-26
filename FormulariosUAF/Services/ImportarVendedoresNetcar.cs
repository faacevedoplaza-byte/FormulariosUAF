using System.Data;
using FormulariosUAF.Data;
using FormulariosUAF.Helpers;
using FormulariosUAF.Models.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace FormulariosUAF.Services;

/// <summary>
/// Importación piloto (temporal) de vendedores desde Netcar (dbo.T_ACC_USUARIO) a FormulariosUAF.
/// - Netcar: solo SELECT, con la cadena 'NetcarReadOnly' (se aborta si la cuenta puede escribir en la tabla).
/// - UAF: crea los usuarios con UserManager (Identity genera el hash de la contraseña de Netcar) y rol Vendedor.
/// - Nunca modifica usuarios existentes ni aplica migraciones.
/// - Las contraseñas solo viven en memoria durante la ejecución: no se imprimen, registran ni serializan.
/// Se lanza a mano desde Program.cs con IMPORTAR_NETCAR_PRUEBA=VERIFICAR|CONFIRMAR (solo Development).
/// </summary>
public static class ImportarVendedoresNetcar
{
    private const string RolVendedor = "Vendedor";

    // Clase (no record): el ToString de un record expondría la contraseña.
    private sealed class VendedorNetcar
    {
        public string Login { get; set; } = string.Empty;
        public string Rut { get; set; } = string.Empty;
        public string? Nombre { get; set; }
        public string? ApellidoPaterno { get; set; }
        public string? Email { get; set; }
        public string Password { get; set; } = string.Empty;
        public string Accion { get; set; } = "CREAR";   // CREAR | OMITIR | ERROR
        public string? Detalle { get; set; }

        public override string ToString() => Login;
    }

    /// <summary>Devuelve el código de salida del proceso (0 = OK, 1 = hubo errores o se abortó).</summary>
    public static async Task<int> EjecutarAsync(IServiceProvider services, IConfiguration config, bool confirmar)
    {
        Console.WriteLine();
        Console.WriteLine(confirmar
            ? "=== Importación Netcar -> UAF | MODO CONFIRMAR (crea usuarios) ==="
            : "=== Importación Netcar -> UAF | MODO VERIFICAR (sin escrituras) ===");

        // ── 1. Logins configurados ─────────────────────────────────────────────
        var logins = (config.GetSection("ImportarNetcar:Logins").Get<string[]>() ?? [])
            .Select(l => l?.Trim() ?? string.Empty)
            .ToList();
        var maxUsuarios = config.GetValue("ImportarNetcar:MaxUsuarios", 2);

        if (logins.Count == 0 || logins.Any(string.IsNullOrEmpty))
            return Abortar("Configure ImportarNetcar:Logins con los login de Netcar a importar (sin vacíos).");
        if (logins.Distinct(StringComparer.OrdinalIgnoreCase).Count() != logins.Count)
            return Abortar("ImportarNetcar:Logins tiene login repetidos.");
        if (logins.Count > maxUsuarios)
            return Abortar($"Se configuraron {logins.Count} login y el máximo permitido es {maxUsuarios} (ImportarNetcar:MaxUsuarios).");

        // ── 2. Base destino UAF ────────────────────────────────────────────────
        var destino = new SqlConnectionStringBuilder(config.GetConnectionString("LocalConnection") ?? string.Empty);
        var baseEsperada = config["ImportarNetcar:BaseDestino"];
        Console.WriteLine($"Destino UAF (LocalConnection): servidor {destino.DataSource} / base {destino.InitialCatalog}");
        if (string.IsNullOrWhiteSpace(baseEsperada))
            return Abortar("Configure ImportarNetcar:BaseDestino con el nombre de la base UAF de pruebas (debe coincidir con LocalConnection).");
        if (!string.Equals(destino.InitialCatalog, baseEsperada, StringComparison.OrdinalIgnoreCase))
            return Abortar($"LocalConnection apunta a '{destino.InitialCatalog}' y ImportarNetcar:BaseDestino dice '{baseEsperada}'.");

        // ── 3. Netcar: conexión de solo lectura y lectura de los vendedores ────
        var netcarCs = config.GetConnectionString("NetcarReadOnly");
        if (string.IsNullOrWhiteSpace(netcarCs))
            return Abortar("Falta la cadena de conexión 'NetcarReadOnly' (definirla en User Secrets, no en el repositorio).");

        List<VendedorNetcar> vendedores;
        try
        {
            await using var conn = new SqlConnection(netcarCs);
            await conn.OpenAsync();

            var error = await VerificarSoloLecturaAsync(conn);
            if (error is not null) return Abortar(error);

            vendedores = await LeerVendedoresAsync(conn, logins);
        }
        catch (SqlException ex)
        {
            return Abortar($"Error consultando Netcar: {ex.Message}");
        }

        var noEncontrados = logins
            .Where(l => vendedores.Count(v => string.Equals(v.Login, l, StringComparison.OrdinalIgnoreCase)) != 1)
            .ToList();
        if (noEncontrados.Count > 0)
            return Abortar("Estos login no aparecen exactamente una vez como vendedor activo en Netcar " +
                           $"(IN_ESVENDEDOR_USUARIO = 1, vigencia 20261231): {string.Join(", ", noEncontrados)}");

        // ── 4. Validaciones contra UAF (solo lectura) ──────────────────────────
        using (var scope = services.CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            var pendientes = (await db.Database.GetPendingMigrationsAsync()).ToList();
            if (pendientes.Count > 0)
                return Abortar($"La base UAF tiene migraciones pendientes ({string.Join(", ", pendientes)}). " +
                               "Esta importación no migra: abra la app normalmente primero o use otra base.");

            if (!await roleManager.RoleExistsAsync(RolVendedor))
                return Abortar($"No existe el rol '{RolVendedor}' en UAF.");

            foreach (var v in vendedores)
                await ValidarEnUafAsync(v, vendedores, userManager);
        }

        // ── 5. Resumen (sin contraseñas) ───────────────────────────────────────
        Console.WriteLine();
        Console.WriteLine($"{"Login",-20} {"RUT",-12} {"Nombre",-20} {"Ap. paterno",-20} {"Email",-35} Acción");
        foreach (var v in vendedores)
            Console.WriteLine($"{v.Login,-20} {v.Rut,-12} {v.Nombre,-20} {v.ApellidoPaterno,-20} {v.Email,-35} {v.Accion}" +
                              (v.Detalle is null ? string.Empty : $" ({v.Detalle})"));
        Console.WriteLine();

        var aCrear = vendedores.Where(v => v.Accion == "CREAR").ToList();
        var hayErrores = vendedores.Any(v => v.Accion == "ERROR");

        if (!confirmar)
        {
            Console.WriteLine($"Verificación terminada: {aCrear.Count} por crear. No se escribió nada.");
            if (aCrear.Count > 0)
                Console.WriteLine("Para crearlos, ejecute de nuevo con IMPORTAR_NETCAR_PRUEBA=CONFIRMAR.");
            return hayErrores ? 1 : 0;
        }

        // ── 6. Creación (una transacción por usuario) ──────────────────────────
        foreach (var v in aCrear)
        {
            if (await CrearUsuarioAsync(services, v))
                Console.WriteLine($"CREADO   {v.Login}");
            else
            {
                hayErrores = true;
                Console.WriteLine($"ERROR    {v.Login}: {v.Detalle}");
            }
        }
        foreach (var v in vendedores.Where(v => v.Accion == "OMITIR"))
            Console.WriteLine($"OMITIDO  {v.Login}: {v.Detalle}");
        foreach (var v in vendedores.Where(v => v.Accion == "ERROR" && !aCrear.Contains(v)))
            Console.WriteLine($"ERROR    {v.Login}: {v.Detalle}");

        Console.WriteLine("Importación terminada. Ejecute VERIFICAR otra vez para comprobar rol, contraseña y que no hay duplicados.");
        return hayErrores ? 1 : 0;
    }

    /// <summary>Aborta si la cuenta de Netcar puede escribir o alterar dbo.T_ACC_USUARIO.</summary>
    private static async Task<string?> VerificarSoloLecturaAsync(SqlConnection conn)
    {
        const string sql = @"
SELECT DB_NAME() AS Base,
       ISNULL(HAS_PERMS_BY_NAME('dbo.T_ACC_USUARIO', 'OBJECT', 'INSERT'), 0)
     + ISNULL(HAS_PERMS_BY_NAME('dbo.T_ACC_USUARIO', 'OBJECT', 'UPDATE'), 0)
     + ISNULL(HAS_PERMS_BY_NAME('dbo.T_ACC_USUARIO', 'OBJECT', 'DELETE'), 0)
     + ISNULL(HAS_PERMS_BY_NAME('dbo.T_ACC_USUARIO', 'OBJECT', 'ALTER'), 0) AS PuedeEscribir";

        await using var cmd = new SqlCommand(sql, conn) { CommandType = CommandType.Text };
        await using var reader = await cmd.ExecuteReaderAsync();
        await reader.ReadAsync();

        Console.WriteLine($"Origen Netcar (NetcarReadOnly): servidor {conn.DataSource} / base {reader.GetString(0)}");
        if (reader.GetInt32(1) > 0)
            return "La cuenta NetcarReadOnly tiene permisos de escritura sobre dbo.T_ACC_USUARIO. Use una cuenta de solo lectura.";

        Console.WriteLine("Conexión Netcar OK, cuenta de solo lectura.");
        return null;
    }

    private static async Task<List<VendedorNetcar>> LeerVendedoresAsync(SqlConnection conn, List<string> logins)
    {
        var parametros = logins.Select((_, i) => $"@login{i}").ToList();
        var sql = $@"
SELECT
    ST_ID_USUARIO,
    ST_RUT_USUARIO,
    ST_NOMBRE_USUARIO,
    ST_APEPAT_USUARIO,
    ST_EMAIL_USUARIO,
    ST_PASSWORD_USUARIO
FROM dbo.T_ACC_USUARIO
WHERE IN_ESVENDEDOR_USUARIO = 1
  AND DT_FECHAVIGENCIA_USUARIO = '20261231'
  AND ST_ID_USUARIO IN ({string.Join(", ", parametros)});";

        await using var cmd = new SqlCommand(sql, conn) { CommandType = CommandType.Text };
        for (var i = 0; i < logins.Count; i++)
            cmd.Parameters.Add(new SqlParameter(parametros[i], SqlDbType.NVarChar, 100) { Value = logins[i] });

        var lista = new List<VendedorNetcar>();
        await using var reader = await cmd.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            var v = new VendedorNetcar
            {
                Login = GetStr(reader, 0) ?? string.Empty,
                Rut = RutHelper.Normalizar(GetStr(reader, 1)),
                Nombre = GetStr(reader, 2),
                ApellidoPaterno = GetStr(reader, 3),
                Email = GetStr(reader, 4)
            };

            // La contraseña se toma tal cual; solo se quita el relleno si la columna es de largo fijo (char/nchar).
            if (!reader.IsDBNull(5))
            {
                var pwd = reader.GetValue(5).ToString() ?? string.Empty;
                var tipo = reader.GetDataTypeName(5).ToLowerInvariant();
                v.Password = tipo is "char" or "nchar" ? pwd.TrimEnd(' ') : pwd;
            }
            lista.Add(v);
        }
        return lista;
    }

    /// <summary>Marca al vendedor como CREAR, OMITIR (ya existe en UAF) o ERROR (datos no válidos). No escribe nada.</summary>
    private static async Task ValidarEnUafAsync(VendedorNetcar v, List<VendedorNetcar> lote, UserManager<ApplicationUser> userManager)
    {
        // Ya existe en UAF → omitir. Si es el mismo login, se informa si quedó con rol y contraseña de Netcar.
        var existente = await userManager.FindByNameAsync(v.Login);
        if (existente is not null)
        {
            var conRol = await userManager.IsInRoleAsync(existente, RolVendedor);
            var mismaPwd = existente.PasswordHash is not null && !string.IsNullOrEmpty(v.Password) &&
                           userManager.PasswordHasher.VerifyHashedPassword(existente, existente.PasswordHash, v.Password)
                               != PasswordVerificationResult.Failed;
            Marcar(v, "OMITIR", $"login ya existe en UAF; rol Vendedor: {SiNo(conRol)}; contraseña Netcar válida: {SiNo(mismaPwd)}");
            return;
        }
        if (!string.IsNullOrEmpty(v.Rut) && await userManager.Users.AnyAsync(u => u.Rut == v.Rut))
        {
            Marcar(v, "OMITIR", "RUT ya existe en UAF con otro login");
            return;
        }
        if (!string.IsNullOrWhiteSpace(v.Email) && await userManager.FindByEmailAsync(v.Email) is not null)
        {
            Marcar(v, "OMITIR", "correo ya existe en UAF con otro login");
            return;
        }

        // Datos de Netcar que no sirven para crear la cuenta
        if (!RutHelper.EsValido(v.Rut)) { Marcar(v, "ERROR", "RUT vacío o con dígito verificador inválido"); return; }
        if (string.IsNullOrWhiteSpace(v.Email)) { Marcar(v, "ERROR", "sin correo en Netcar (UAF exige correo único)"); return; }
        if (string.IsNullOrEmpty(v.Password)) { Marcar(v, "ERROR", "sin contraseña en Netcar"); return; }
        if (lote.Count(o => o.Rut == v.Rut) > 1) { Marcar(v, "ERROR", "RUT repetido dentro del lote"); return; }
        if (lote.Count(o => string.Equals(o.Email, v.Email, StringComparison.OrdinalIgnoreCase)) > 1)
        {
            Marcar(v, "ERROR", "correo repetido dentro del lote");
            return;
        }

        // Mismas reglas que aplicará CreateAsync (caracteres del login, correo, política de contraseña), sin escribir.
        var candidato = NuevoUsuario(v);
        var errores = new List<string>();
        foreach (var validator in userManager.UserValidators)
        {
            var r = await validator.ValidateAsync(userManager, candidato);
            if (!r.Succeeded) errores.AddRange(r.Errors.Select(e => e.Description));
        }
        foreach (var validator in userManager.PasswordValidators)
        {
            var r = await validator.ValidateAsync(userManager, candidato, v.Password);
            if (!r.Succeeded) errores.Add("la contraseña de Netcar no cumple la política de UAF");
        }
        if (errores.Count > 0)
            Marcar(v, "ERROR", string.Join("; ", errores.Distinct()));
    }

    /// <summary>Crea usuario + rol + auditoría en una sola transacción: si algo falla no queda nada a medias.</summary>
    private static async Task<bool> CrearUsuarioAsync(IServiceProvider services, VendedorNetcar v)
    {
        // Scope propio por usuario: un rollback no deja entidades rastreadas que afecten al siguiente.
        using var scope = services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        var audit = scope.ServiceProvider.GetRequiredService<IAuditService>();

        try
        {
            await using var tx = await db.Database.BeginTransactionAsync();
            var user = NuevoUsuario(v);

            var creado = await userManager.CreateAsync(user, v.Password);
            if (!creado.Succeeded)
            {
                v.Detalle = string.Join("; ", creado.Errors.Select(e => e.Description));
                return false;   // dispose sin commit = rollback
            }

            var rol = await userManager.AddToRoleAsync(user, RolVendedor);
            if (!rol.Succeeded)
            {
                v.Detalle = "no se pudo asignar el rol Vendedor (se revirtió la cuenta): " +
                            string.Join("; ", rol.Errors.Select(e => e.Description));
                return false;
            }

            await audit.LogAsync("IMPORTAR_USUARIO_NETCAR", "ApplicationUser", user.Id,
                newValues: new { user.UserName, user.Rut, user.Email, Role = RolVendedor },
                userName: "ImportarVendedoresNetcar");

            await tx.CommitAsync();
            return true;
        }
        catch (Exception ex)
        {
            v.Detalle = $"{ex.GetType().Name}: {ex.Message} (se revirtió la cuenta)";
            return false;
        }
    }

    private static ApplicationUser NuevoUsuario(VendedorNetcar v) => new()
    {
        UserName = v.Login,
        Email = v.Email,
        Rut = v.Rut,
        Nombre = v.Nombre,
        ApellidoPaterno = v.ApellidoPaterno,
        FullName = string.Join(" ", new[] { v.Nombre, v.ApellidoPaterno }.Where(s => !string.IsNullOrWhiteSpace(s))),
        EmailConfirmed = true,
        IsActive = true
    };

    private static void Marcar(VendedorNetcar v, string accion, string detalle)
    {
        v.Accion = accion;
        v.Detalle = detalle;
    }

    private static string SiNo(bool b) => b ? "sí" : "no";

    private static int Abortar(string mensaje)
    {
        Console.WriteLine($"ABORTADO: {mensaje}");
        Console.WriteLine("No se escribió nada en UAF ni en Netcar.");
        return 1;
    }

    private static string? GetStr(SqlDataReader r, int i)
        => r.IsDBNull(i) ? null : r.GetValue(i)?.ToString()?.Trim();
}
