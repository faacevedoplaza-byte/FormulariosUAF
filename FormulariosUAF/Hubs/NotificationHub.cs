using System.Collections.Concurrent;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace FormulariosUAF.Hubs;

[Authorize]
public class NotificationHub : Hub
{
    /// <summary>
    /// Aviso (sin datos) de que cambió algo en Regcheq/Gestion: tomar, liberar, asignar, nota, cierre por firma.
    /// La página lo recibe y recarga su contenido.
    /// </summary>
    public const string GestionRegcheqCambiada = "GestionRegcheqCambiada";

    /// <summary>Aviso (sin datos) de que cambiaron datos en RegCheq (lo detecta <c>RegcheqVivoWorker</c>).</summary>
    public const string RegcheqCambiada = "RegcheqCambiada";
    public const string GrupoRegcheq = "regcheq";

    private static readonly string[] RolesRegcheq = ["Administrador", "Cumplimiento", "Revisor"];

    // Conexiones con una vista RegCheq abierta. El vigilante solo consulta RegCheq si hay alguna.
    // (En memoria: válido mientras la app corra en un solo proceso.)
    private static readonly ConcurrentDictionary<string, byte> SiguiendoRegcheq = new();
    public static bool HayQuienSigaRegcheq => !SiguiendoRegcheq.IsEmpty;

    /// <summary>Lo llaman las vistas /Regcheq al conectarse, para recibir <see cref="RegcheqCambiada"/>.</summary>
    public async Task SeguirRegcheq()
    {
        if (!RolesRegcheq.Any(Context.User!.IsInRole)) return;
        await Groups.AddToGroupAsync(Context.ConnectionId, GrupoRegcheq);
        SiguiendoRegcheq[Context.ConnectionId] = 0;
    }

    private readonly ILogger<NotificationHub> _logger;

    public NotificationHub(ILogger<NotificationHub> logger) => _logger = logger;

    public override async Task OnConnectedAsync()
    {
        _logger.LogDebug("SignalR conectado: {User}", Context.User?.Identity?.Name);
        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        SiguiendoRegcheq.TryRemove(Context.ConnectionId, out _);
        _logger.LogDebug("SignalR desconectado: {User}", Context.User?.Identity?.Name);
        await base.OnDisconnectedAsync(exception);
    }
}
