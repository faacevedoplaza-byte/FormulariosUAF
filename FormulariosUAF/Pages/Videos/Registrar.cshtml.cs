using System.Security.Claims;
using FormulariosUAF.Data;
using FormulariosUAF.Models.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace FormulariosUAF.Pages.Videos;

/// <summary>
/// POST /Videos/Registrar: el reproductor (js/video-intro.js) envía aquí el estado de la visita al video
/// (al abrir, al reproducir, pausar, cambiar de escena, terminar y salir). Cada envío trae el estado completo,
/// así que se guarda tal cual (upsert por Sesion). Antiforgery: encabezado X-CSRF-TOKEN.
/// </summary>
[Authorize]
public class RegistrarModel : PageModel
{
    private readonly ApplicationDbContext _db;

    public RegistrarModel(ApplicationDbContext db) => _db = db;

    public record EstadoVideo(
        Guid Sesion, string Video, bool Reproducido, int TotalEscenas, int EscenaMaxima, int[]? EscenasVistas,
        int Pausas, int Adelantos, int Retrocesos, int Segundos, bool Completo, bool ConVoz, bool PantallaCompleta);

    public IActionResult OnGet() => NotFound();

    public async Task<IActionResult> OnPostAsync([FromBody] EstadoVideo? e, CancellationToken ct)
    {
        if (e is null || e.Sesion == Guid.Empty || VideosIntroduccion.Buscar(e.Video) is null) return BadRequest();
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId is null) return Challenge();

        // Dos envíos casi simultáneos de una sesión nueva (abrir + reproducir) pueden chocar en el índice único:
        // el segundo reintenta como actualización.
        for (var intento = 1; ; intento++)
        {
            var v = await _db.VideosVisualizacion.FirstOrDefaultAsync(x => x.Sesion == e.Sesion, ct);
            if (v is null)
            {
                v = new VideoVisualizacion { Sesion = e.Sesion, Video = e.Video, UsuarioId = userId };
                _db.VideosVisualizacion.Add(v);
            }
            else if (v.UsuarioId != userId || v.Video != e.Video)
            {
                return BadRequest();
            }
            Aplicar(v, e);
            try
            {
                await _db.SaveChangesAsync(ct);
                return new NoContentResult();
            }
            catch (DbUpdateException) when (intento == 1)
            {
                _db.ChangeTracker.Clear();
            }
        }
    }

    private static void Aplicar(VideoVisualizacion v, EstadoVideo e)
    {
        var total = Math.Clamp(e.TotalEscenas, 0, 100);
        var vistas = (e.EscenasVistas ?? []).Where(n => n >= 1 && n <= total).Distinct().Order();

        // Los contadores solo crecen (un envío atrasado no deshace uno posterior).
        v.FechaUltimaActividad = DateTime.UtcNow;
        v.Reproducido |= e.Reproducido;
        v.TotalEscenas = total;
        v.EscenaMaxima = Math.Max(v.EscenaMaxima, Math.Clamp(e.EscenaMaxima, 0, total));
        var union = v.EscenasVistas.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse)
            .Concat(vistas).Distinct().Order();
        v.EscenasVistas = string.Join(',', union);
        v.Pausas = Math.Max(v.Pausas, Math.Clamp(e.Pausas, 0, 10_000));
        v.Adelantos = Math.Max(v.Adelantos, Math.Clamp(e.Adelantos, 0, 10_000));
        v.Retrocesos = Math.Max(v.Retrocesos, Math.Clamp(e.Retrocesos, 0, 10_000));
        v.SegundosReproducidos = Math.Max(v.SegundosReproducidos, Math.Clamp(e.Segundos, 0, 86_400));
        v.Completo |= e.Completo;
        v.ConVoz = e.ConVoz;
        v.UsoPantallaCompleta |= e.PantallaCompleta;
    }
}
