using FormulariosUAF.Data;
using FormulariosUAF.Models.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace FormulariosUAF.Pages.Admin;

/// <summary>Informe de visualizaciones de los videos de introducción (lo que registra Pages/Videos/Registrar).</summary>
public class VideosModel : PageModel
{
    private readonly ApplicationDbContext _db;
    private readonly UserManager<ApplicationUser> _userManager;

    public VideosModel(ApplicationDbContext db, UserManager<ApplicationUser> userManager)
    {
        _db = db;
        _userManager = userManager;
    }

    [BindProperty(SupportsGet = true, Name = "video")] public string? VideoClave { get; set; }
    [BindProperty(SupportsGet = true, Name = "usuario")] public string? FiltroUsuario { get; set; }

    public VideosIntroduccion.Info Video { get; private set; } = VideosIntroduccion.PepDof;

    public int Visitas { get; private set; }
    public int Reproducciones { get; private set; }
    public int UsuariosQueLoVieron { get; private set; }
    public int Completas { get; private set; }
    public double? MinutosPromedio { get; private set; }
    public Dictionary<ModoVisto, int> PorModo { get; private set; } = [];

    public List<FilaUsuario> Usuarios { get; private set; } = [];
    public List<VideoVisualizacion> Detalle { get; private set; } = [];
    public string? NombreFiltroUsuario { get; private set; }

    public enum ModoVisto { SinInterrupciones, ConPausas, Saltando, Incompleto }
    public enum EstadoUsuario { Completo, Empezado, SoloAbrio, NoAbierto }

    public record FilaUsuario(string Id, string Nombre, string Roles, EstadoUsuario Estado, int Reproducciones,
        ModoVisto? MejorModo, int MejorEscenas, int TotalEscenas, int Pausas, int Segundos, DateTime? Ultima);

    public static ModoVisto Modo(VideoVisualizacion v) =>
        !v.Completo ? ModoVisto.Incompleto
        : v.Adelantos > 0 || v.CantidadEscenasVistas < v.TotalEscenas ? ModoVisto.Saltando
        : v.Pausas > 0 || v.Retrocesos > 0 ? ModoVisto.ConPausas
        : ModoVisto.SinInterrupciones;

    public static string Texto(ModoVisto m) => m switch
    {
        ModoVisto.SinInterrupciones => "De inicio a fin, sin pausas",
        ModoVisto.ConPausas => "Completo, con pausas o retrocesos",
        ModoVisto.Saltando => "Completo, saltándose escenas",
        _ => "No lo terminó",
    };

    public static string Color(ModoVisto m) => m switch
    {
        ModoVisto.SinInterrupciones => "success",
        ModoVisto.ConPausas => "primary",
        ModoVisto.Saltando => "warning",
        _ => "danger",
    };

    public async Task OnGetAsync(CancellationToken ct)
    {
        Video = VideosIntroduccion.Buscar(VideoClave) ?? VideosIntroduccion.PepDof;

        var visitas = await _db.VideosVisualizacion.AsNoTracking()
            .Include(v => v.Usuario)
            .Where(v => v.Video == Video.Clave)
            .OrderByDescending(v => v.FechaInicio)
            .ToListAsync(ct);
        var reproducidas = visitas.Where(v => v.Reproducido).ToList();

        Visitas = visitas.Count;
        Reproducciones = reproducidas.Count;
        UsuariosQueLoVieron = reproducidas.Select(v => v.UsuarioId).Distinct().Count();
        Completas = reproducidas.Count(v => v.Completo);
        MinutosPromedio = reproducidas.Count > 0 ? reproducidas.Average(v => v.SegundosReproducidos) / 60.0 : null;
        PorModo = Enum.GetValues<ModoVisto>().ToDictionary(m => m, m => reproducidas.Count(v => Modo(v) == m));

        // Usuarios activos de los roles a los que va dirigido el video, más cualquiera que lo haya abierto.
        var usuarios = new Dictionary<string, (ApplicationUser U, SortedSet<string> Roles)>();
        foreach (var rol in Video.Roles)
            foreach (var u in await _userManager.GetUsersInRoleAsync(rol))
            {
                if (!u.IsActive) continue;
                if (!usuarios.TryGetValue(u.Id, out var x)) usuarios[u.Id] = x = (u, []);
                x.Roles.Add(rol);
            }
        foreach (var v in visitas.Where(v => v.Usuario is not null && !usuarios.ContainsKey(v.UsuarioId)))
            usuarios[v.UsuarioId] = (v.Usuario!, []);

        var porUsuario = visitas.ToLookup(v => v.UsuarioId);
        Usuarios = usuarios.Values.Select(x =>
        {
            var suyas = porUsuario[x.U.Id].ToList();
            var rep = suyas.Where(v => v.Reproducido).ToList();
            // La "mejor" visita: completa antes que incompleta, y luego la que más escenas vio enteras.
            var mejor = rep.OrderBy(v => Modo(v)).ThenByDescending(v => v.CantidadEscenasVistas).FirstOrDefault();
            var estado = rep.Any(v => v.Completo) ? EstadoUsuario.Completo
                : rep.Count > 0 ? EstadoUsuario.Empezado
                : suyas.Count > 0 ? EstadoUsuario.SoloAbrio
                : EstadoUsuario.NoAbierto;
            return new FilaUsuario(x.U.Id, string.IsNullOrWhiteSpace(x.U.FullName) ? x.U.UserName ?? x.U.Id : x.U.FullName,
                string.Join(", ", x.Roles), estado, rep.Count, mejor is null ? null : Modo(mejor),
                mejor?.CantidadEscenasVistas ?? 0, mejor?.TotalEscenas ?? 0,
                rep.Sum(v => v.Pausas), rep.Sum(v => v.SegundosReproducidos),
                suyas.Count > 0 ? suyas.Max(v => v.FechaUltimaActividad) : null);
        })
        .OrderByDescending(f => f.Estado).ThenBy(f => f.Nombre)
        .ToList();

        var detalle = reproducidas.AsEnumerable();
        if (!string.IsNullOrEmpty(FiltroUsuario))
        {
            detalle = detalle.Where(v => v.UsuarioId == FiltroUsuario);
            NombreFiltroUsuario = Usuarios.FirstOrDefault(f => f.Id == FiltroUsuario)?.Nombre;
        }
        Detalle = detalle.Take(300).ToList();
    }

    public static string Duracion(int segundos) =>
        segundos < 60 ? $"{segundos} s" : $"{segundos / 60} min {segundos % 60:00} s";
}
