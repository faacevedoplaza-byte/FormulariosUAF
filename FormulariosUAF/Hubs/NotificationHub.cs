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

    // Mismos roles que RegcheqPolicy (Program.cs).
    private static readonly string[] RolesRegcheq = ["Administrador", "Cumplimiento", "Revisor", "Call Center", "Vendedor"];

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

    /// <summary>
    /// Aviso de que cambió una capacitación (dato: su Id): alguien registró asistencia en el enlace público,
    /// se eliminó un registro, se abrió/cerró el registro o se creó una capacitación.
    /// </summary>
    public const string CapacitacionCambiada = "CapacitacionCambiada";
    /// <summary>Aviso (dato: su Id) de que se eliminó una capacitación: su detalle abierto vuelve al listado.</summary>
    public const string CapacitacionEliminada = "CapacitacionEliminada";
    public const string GrupoCapacitaciones = "capacitaciones";

    /// <summary>Lo llaman Admin/Capacitaciones (listado y detalle) al conectarse, para recibir <see cref="CapacitacionCambiada"/>.</summary>
    public async Task SeguirCapacitaciones()
    {
        if (!Context.User!.IsInRole("Administrador")) return; // mismo rol que AdminPolicy
        await Groups.AddToGroupAsync(Context.ConnectionId, GrupoCapacitaciones);
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

public static class NotificationHubExtensions
{
    /// <summary>
    /// Avisa a Admin/Capacitaciones que cambió la capacitación <paramref name="capacitacionId"/>.
    /// Se llama después de guardar: si el aviso falla, el cambio ya quedó hecho y solo se registra en el log.
    /// </summary>
    public static async Task AvisarCapacitacionCambiadaAsync(this IHubContext<NotificationHub> hub, int capacitacionId, ILogger logger,
        string evento = NotificationHub.CapacitacionCambiada)
    {
        try
        {
            await hub.Clients.Group(NotificationHub.GrupoCapacitaciones).SendAsync(evento, capacitacionId);
        }
        catch (Exception ex)
        {
            logger.LogWarning(ex, "No se pudo avisar el cambio de la capacitación {Id}", capacitacionId);
        }
    }
}
