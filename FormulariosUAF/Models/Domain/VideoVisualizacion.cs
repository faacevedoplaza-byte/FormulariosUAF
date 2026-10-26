namespace FormulariosUAF.Models.Domain;

/// <summary>
/// Una visita de un usuario a un video de introducción (Regcheq/Introduccion, Vendedor/Introduccion).
/// La crea y la actualiza el reproductor (js/video-intro.js) vía Pages/Videos/Registrar; cada fila es una carga de la
/// página, o un "Volver a ver" dentro de ella. Los contadores los informa el navegador.
/// </summary>
public class VideoVisualizacion
{
    public int Id { get; set; }

    /// <summary>Identificador que genera el navegador para esta visita (upsert idempotente).</summary>
    public Guid Sesion { get; set; }

    /// <summary>Clave del video (ver <see cref="VideosIntroduccion"/>).</summary>
    public string Video { get; set; } = string.Empty;

    public string UsuarioId { get; set; } = string.Empty;
    public ApplicationUser? Usuario { get; set; }

    public DateTime FechaInicio { get; set; } = DateTime.UtcNow;
    public DateTime FechaUltimaActividad { get; set; } = DateTime.UtcNow;

    /// <summary>False = abrió la página pero nunca presionó reproducir.</summary>
    public bool Reproducido { get; set; }

    public int TotalEscenas { get; set; }
    /// <summary>Escena más avanzada a la que llegó (1..TotalEscenas; 0 = ninguna).</summary>
    public int EscenaMaxima { get; set; }
    /// <summary>Escenas reproducidas enteras, separadas por coma (p. ej. "1,2,3,5").</summary>
    public string EscenasVistas { get; set; } = string.Empty;

    public int Pausas { get; set; }
    /// <summary>Veces que pasó a una escena posterior a mano (botón, flecha o barra).</summary>
    public int Adelantos { get; set; }
    /// <summary>Veces que volvió a una escena anterior.</summary>
    public int Retrocesos { get; set; }
    /// <summary>Tiempo con el video en reproducción (sin contar pausas).</summary>
    public int SegundosReproducidos { get; set; }

    /// <summary>Terminó la última escena.</summary>
    public bool Completo { get; set; }
    public bool ConVoz { get; set; }
    public bool UsoPantallaCompleta { get; set; }

    public int CantidadEscenasVistas =>
        string.IsNullOrEmpty(EscenasVistas) ? 0 : EscenasVistas.Split(',', StringSplitOptions.RemoveEmptyEntries).Length;
}

/// <summary>Videos de introducción disponibles y a qué roles están dirigidos (para saber quién falta por verlos).</summary>
public static class VideosIntroduccion
{
    public record Info(string Clave, string Titulo, string Url, string[] Roles);

    public static readonly Info PepDof = new("pepdof", "¿Qué es PEP y DOF?", "/Regcheq/Introduccion",
        ["Administrador", "Cumplimiento", "Revisor", "Call Center", "Vendedor"]);

    public static readonly Info Solicitudes = new("solicitudes", "¿Cómo funcionan las solicitudes?", "/Vendedor/Introduccion",
        ["Administrador", "Vendedor"]);

    public static readonly IReadOnlyList<Info> Todos = [PepDof, Solicitudes];

    public static Info? Buscar(string? clave) => Todos.FirstOrDefault(v => v.Clave == clave);
}
