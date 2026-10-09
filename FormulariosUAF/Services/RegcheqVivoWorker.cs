using FormulariosUAF.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace FormulariosUAF.Services;

/// <summary>
/// Vigila RegCheq para que las vistas /Regcheq se actualicen en vivo. Los datos los escribe otro sistema
/// (integracionesNetcar), así que no hay un evento propio: cada <c>RegcheqVivo:IntervaloSegundos</c> (20 por
/// defecto) se compara la huella de las tablas con la anterior y, si cambió, se sincronizan las gestiones y se
/// avisa por SignalR al grupo <see cref="NotificationHub.GrupoRegcheq"/>.
/// Solo consulta mientras alguien tiene una vista RegCheq abierta: una consulta para todo el servidor,
/// no una por usuario.
/// </summary>
public class RegcheqVivoWorker : BackgroundService
{
    private readonly IServiceScopeFactory _scopes;
    private readonly IHubContext<NotificationHub> _hub;
    private readonly IConfiguration _config;
    private readonly ILogger<RegcheqVivoWorker> _logger;

    public RegcheqVivoWorker(IServiceScopeFactory scopes, IHubContext<NotificationHub> hub, IConfiguration config,
                             ILogger<RegcheqVivoWorker> logger)
    {
        _scopes = scopes;
        _hub = hub;
        _config = config;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var intervalo = TimeSpan.FromSeconds(Math.Max(5, _config.GetValue("RegcheqVivo:IntervaloSegundos", 20)));
        string? anterior = null;

        try
        {
            using var timer = new PeriodicTimer(intervalo);
            while (await timer.WaitForNextTickAsync(stoppingToken))
            {
                if (!NotificationHub.HayQuienSigaRegcheq)
                {
                    // Sin nadie mirando no se consulta; al volver alguien, la primera huella solo se guarda.
                    anterior = null;
                    continue;
                }
                anterior = await RevisarAsync(anterior, stoppingToken);
            }
        }
        catch (OperationCanceledException) when (stoppingToken.IsCancellationRequested)
        {
            // Apagado de la aplicación.
        }
    }

    private async Task<string?> RevisarAsync(string? anterior, CancellationToken ct)
    {
        try
        {
            using var scope = _scopes.CreateScope();
            var huella = await scope.ServiceProvider.GetRequiredService<IRegcheqService>().ObtenerHuellaAsync(ct);
            if (huella is null || anterior is null || huella == anterior) return huella;

            _logger.LogDebug("RegCheq cambió: se avisa a las vistas abiertas");
            // Primero cerrar/reabrir gestiones según las firmas nuevas (avisa a Gestión si cambió algo).
            try
            {
                await scope.ServiceProvider.GetRequiredService<IGestionSincronizador>().SincronizarAsync(ct);
            }
            catch (Exception ex) when (ex is not OperationCanceledException)
            {
                _logger.LogWarning(ex, "No se pudieron sincronizar las gestiones tras un cambio en RegCheq");
            }
            await _hub.Clients.Group(NotificationHub.GrupoRegcheq).SendAsync(NotificationHub.RegcheqCambiada, ct);
            return huella;
        }
        catch (OperationCanceledException) when (ct.IsCancellationRequested)
        {
            throw;
        }
        catch (Exception ex)
        {
            // RegCheq caído o lento: se reintenta en el próximo ciclo sin avisar nada.
            _logger.LogWarning(ex, "No se pudo revisar si RegCheq cambió; se reintentará");
            return anterior;
        }
    }
}
