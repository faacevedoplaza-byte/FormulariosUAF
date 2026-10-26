using FormulariosUAF.Data;
using FormulariosUAF.Hubs;
using FormulariosUAF.Models.Domain;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace FormulariosUAF.Services;

/// <summary>
/// Cierra las gestiones de operaciones RegCheq cuyos formularios requeridos ya están firmados
/// (y reabre las que vuelven a estar pendientes). Lo usan la página Regcheq/Gestion y
/// <see cref="GestionSincronizacionWorker"/>.
/// </summary>
public interface IGestionSincronizador
{
    /// <summary>Consulta RegCheq y sincroniza. Null si RegCheqConnection no está configurada. Lanza si RegCheq falla.</summary>
    Task<ResultadoSincronizacion?> SincronizarAsync(CancellationToken ct = default);

    /// <summary>Sincroniza con una lista de pendientes ya leída de RegCheq (debe ser una lectura correcta y completa).</summary>
    Task<ResultadoSincronizacion> SincronizarAsync(IReadOnlyCollection<RegcheqPendienteGestion> pendientes, CancellationToken ct = default);
}

public record ResultadoSincronizacion(int Cerradas, int Reabiertas, bool Omitida = false);

public class GestionSincronizador : IGestionSincronizador
{
    // La página y la tarea en segundo plano pueden sincronizar a la vez: sin esto se duplicarían las notas de cierre.
    private static readonly SemaphoreSlim Candado = new(1, 1);

    private readonly IRegcheqService _regcheq;
    private readonly ApplicationDbContext _db;
    private readonly IHubContext<NotificationHub> _hub;
    private readonly ILogger<GestionSincronizador> _logger;

    public GestionSincronizador(IRegcheqService regcheq, ApplicationDbContext db, IHubContext<NotificationHub> hub,
                                ILogger<GestionSincronizador> logger)
    {
        _regcheq = regcheq;
        _db = db;
        _hub = hub;
        _logger = logger;
    }

    public async Task<ResultadoSincronizacion?> SincronizarAsync(CancellationToken ct = default)
    {
        var pendientes = await _regcheq.ListarPendientesGestionAsync(RegcheqFirmas.InicioSeguimiento, ct);
        return pendientes is null ? null : await SincronizarAsync(pendientes, ct);
    }

    public async Task<ResultadoSincronizacion> SincronizarAsync(IReadOnlyCollection<RegcheqPendienteGestion> pendientes, CancellationToken ct = default)
    {
        // Si otra sincronización está en curso, esta se omite: la otra deja el mismo resultado.
        if (!await Candado.WaitAsync(0, ct))
            return new ResultadoSincronizacion(0, 0, Omitida: true);

        try
        {
            var ids = pendientes.Select(p => p.Id).ToHashSet();
            var gestiones = await _db.GestionesOperacion.ToListAsync(ct);
            var ahora = DateTime.UtcNow;
            int cerradas = 0, reabiertas = 0;

            foreach (var g in gestiones)
            {
                var pendiente = ids.Contains(g.OperacionId);
                if (g.FechaCierre is null && !pendiente)
                {
                    g.FechaCierre = ahora;
                    _db.GestionesOperacionNotas.Add(NotaSistema(g.Id, ahora,
                        "Gestión cerrada: RegCheq registra firmados los formularios requeridos."));
                    cerradas++;
                }
                else if (g.FechaCierre is not null && pendiente)
                {
                    g.FechaCierre = null;
                    _db.GestionesOperacionNotas.Add(NotaSistema(g.Id, ahora,
                        "Gestión reabierta: RegCheq vuelve a tener la operación con firma pendiente."));
                    reabiertas++;
                }
            }

            if (cerradas + reabiertas > 0)
            {
                await _db.SaveChangesAsync(ct);
                _logger.LogInformation("Gestión RegCheq: {Cerradas} gestiones cerradas y {Reabiertas} reabiertas según firmas", cerradas, reabiertas);
                await AvisarCambioAsync();
            }
            return new ResultadoSincronizacion(cerradas, reabiertas);
        }
        finally
        {
            Candado.Release();
        }
    }

    /// <summary>Avisa a las vistas abiertas; si SignalR falla, la sincronización ya quedó guardada y no se revierte.</summary>
    private async Task AvisarCambioAsync()
    {
        try
        {
            await _hub.Clients.All.SendAsync(NotificationHub.GestionRegcheqCambiada);
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "No se pudo avisar por SignalR el cambio en gestiones RegCheq");
        }
    }

    private static GestionOperacionNota NotaSistema(int gestionId, DateTime fecha, string texto) => new()
    {
        GestionOperacionId = gestionId, UsuarioNombre = "Sistema", EsSistema = true, CreatedAt = fecha, Texto = texto
    };
}

/// <summary>
/// Sincroniza los cierres de gestión cada <c>GestionRegcheq:IntervaloMinutos</c> (15 por defecto),
/// para que no dependan de que alguien abra la vista. No se puede desactivar.
/// </summary>
public class GestionSincronizacionWorker : BackgroundService
{
    private static readonly TimeSpan EsperaInicial = TimeSpan.FromMinutes(1);

    private readonly IServiceScopeFactory _scopes;
    private readonly IConfiguration _config;
    private readonly ILogger<GestionSincronizacionWorker> _logger;

    public GestionSincronizacionWorker(IServiceScopeFactory scopes, IConfiguration config, ILogger<GestionSincronizacionWorker> logger)
    {
        _scopes = scopes;
        _config = config;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        // Siempre activa, en todos los ambientes y contra la base que use la app (Qa2, producción, local).
        var intervalo = TimeSpan.FromMinutes(Math.Max(1, _config.GetValue("GestionRegcheq:IntervaloMinutos", 15)));
        _logger.LogInformation("Sincronización de gestiones RegCheq cada {Minutos} minutos", intervalo.TotalMinutes);

        try
        {
            // No competir con el arranque (migraciones, seed).
            await Task.Delay(EsperaInicial, stoppingToken);

            using var timer = new PeriodicTimer(intervalo);
            do
            {
                await EjecutarAsync(stoppingToken);
            }
            while (await timer.WaitForNextTickAsync(stoppingToken));
        }
        catch (OperationCanceledException) when (stoppingToken.IsCancellationRequested)
        {
            // Apagado de la aplicación.
        }
    }

    private async Task EjecutarAsync(CancellationToken ct)
    {
        try
        {
            using var scope = _scopes.CreateScope();
            var sincronizador = scope.ServiceProvider.GetRequiredService<IGestionSincronizador>();
            var resultado = await sincronizador.SincronizarAsync(ct);
            if (resultado is null)
                _logger.LogWarning("Sincronización de gestiones RegCheq omitida: RegCheqConnection no está configurada");
        }
        catch (OperationCanceledException) when (ct.IsCancellationRequested)
        {
            throw;
        }
        catch (Exception ex)
        {
            // Un fallo (RegCheq caído, timeout) no debe detener la tarea: se reintenta en el siguiente ciclo.
            _logger.LogError(ex, "Error sincronizando gestiones RegCheq; se reintentará en el próximo ciclo");
        }
    }
}
