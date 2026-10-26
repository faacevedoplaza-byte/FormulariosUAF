namespace FormulariosUAF.Models.Domain;

/// <summary>
/// Una capacitación con su enlace público de registro de asistencia (Pages/Capacitacion/Registro?t=Token).
/// La crea un administrador en Admin/Capacitaciones; los asistentes no necesitan cuenta en el sistema.
/// </summary>
public class Capacitacion
{
    public int Id { get; set; }

    public string Titulo { get; set; } = string.Empty;
    public string? Descripcion { get; set; }
    public DateTime Fecha { get; set; }
    public string? Lugar { get; set; }
    public string? Relator { get; set; }

    /// <summary>Identificador aleatorio del enlace público (no se puede adivinar a partir del Id).</summary>
    public string Token { get; set; } = string.Empty;

    /// <summary>False = el enlace ya no acepta registros (lo cierra el administrador).</summary>
    public bool RegistroAbierto { get; set; } = true;

    public string? CreadoPor { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public ICollection<CapacitacionAsistencia> Asistencias { get; set; } = new List<CapacitacionAsistencia>();
}

/// <summary>
/// Registro de asistencia que deja una persona en el enlace público de una capacitación.
/// El RUT y el correo se cruzan después con los usuarios del sistema (ApplicationUser.Rut / Email) en Admin/Capacitaciones/Detalle.
/// </summary>
public class CapacitacionAsistencia
{
    public int Id { get; set; }

    public int CapacitacionId { get; set; }
    public Capacitacion? Capacitacion { get; set; }

    public string NombreCompleto { get; set; } = string.Empty;
    /// <summary>RUT normalizado (sin puntos ni guión, ver RutHelper), igual que ApplicationUser.Rut.</summary>
    public string Rut { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? Telefono { get; set; }
    public string? Cargo { get; set; }
    public string? Sucursal { get; set; }

    /// <summary>Empresa del grupo a la que pertenece (catálogo T_EMPRESA). Null solo en registros anteriores al campo.</summary>
    public int? EmpresaId { get; set; }
    public Empresa? Empresa { get; set; }

    /// <summary>Marcó "Declaro haber asistido a la capacitación". El formulario no se acepta sin esto.</summary>
    public bool AceptaAsistencia { get; set; }

    public DateTime FechaRegistro { get; set; } = DateTime.UtcNow;
    public string? IpAddress { get; set; }
    public string? UserAgent { get; set; }
}
