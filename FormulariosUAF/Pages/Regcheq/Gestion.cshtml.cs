using FormulariosUAF.Data;
using FormulariosUAF.Hubs;
using FormulariosUAF.Models.Domain;
using FormulariosUAF.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace FormulariosUAF.Pages.Regcheq;

/// <summary>
/// Operaciones RegCheq (persona natural) con formulario PEP y/o DOF requerido y sin firmar.
/// Un usuario toma la operación y registra en la bitácora la gestión que realiza.
/// La gestión se cierra sola cuando RegCheq registra la firma de los formularios requeridos.
/// </summary>
public class GestionModel : PageModel
{
    public static DateTime InicioSeguimiento => RegcheqFirmas.InicioSeguimiento;

    private const int TamanoPagina = 25;
    private const int LargoMaximoNota = 2000;
    /// <summary>Roles que pueden tomar y gestionar operaciones. Revisor entra al módulo (RegcheqPolicy) solo en lectura.</summary>
    private static readonly string[] RolesGestion = ["Administrador", "Cumplimiento", "Call Center"];

    private readonly IRegcheqService _regcheq;
    private readonly IGestionSincronizador _sincronizador;
    private readonly IVehiculoOperacionService _vehiculos;
    private readonly ApplicationDbContext _db;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IHubContext<NotificationHub> _hub;
    private readonly ILogger<GestionModel> _logger;

    public GestionModel(IRegcheqService regcheq, IGestionSincronizador sincronizador, IVehiculoOperacionService vehiculos,
                        ApplicationDbContext db, UserManager<ApplicationUser> userManager, IHubContext<NotificationHub> hub,
                        ILogger<GestionModel> logger)
    {
        _regcheq = regcheq;
        _sincronizador = sincronizador;
        _vehiculos = vehiculos;
        _db = db;
        _userManager = userManager;
        _hub = hub;
        _logger = logger;
    }

    /// <summary>
    /// Después de cualquier acción (POST) avisa a todas las vistas abiertas para que se recarguen.
    /// Si la acción no cambió nada, el aviso solo provoca una recarga sin cambios.
    /// </summary>
    public override async Task OnPageHandlerExecutionAsync(PageHandlerExecutingContext context, PageHandlerExecutionDelegate next)
    {
        var ejecutado = await next();
        if (!HttpMethods.IsPost(context.HttpContext.Request.Method) || ejecutado.Exception is not null) return;
        try
        {
            await _hub.Clients.All.SendAsync(NotificationHub.GestionRegcheqCambiada);
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "No se pudo avisar por SignalR el cambio en gestiones RegCheq");
        }
    }

    // Filtros (query string)
    /// <summary>"abiertas" (default) o "cerradas".</summary>
    [BindProperty(SupportsGet = true)] public string? Vista { get; set; }
    [BindProperty(SupportsGet = true)] public string? Q { get; set; }
    /// <summary>"pep" | "dof" | "ambos" | vacío = todos.</summary>
    [BindProperty(SupportsGet = true)] public string? Caso { get; set; }
    /// <summary>"sin" (sin tomar) | "mias" | "tomadas" | vacío = todas.</summary>
    [BindProperty(SupportsGet = true)] public string? Asignacion { get; set; }
    /// <summary>Id del usuario que tiene tomadas las operaciones (filtro desde "Carga por usuario").</summary>
    [BindProperty(SupportsGet = true)] public string? Usuario { get; set; }
    [BindProperty(SupportsGet = true)] public int Pagina { get; set; } = 1;
    /// <summary>Operación cuyo panel de gestión se muestra abierto (después de una acción).</summary>
    [BindProperty(SupportsGet = true)] public int? Abrir { get; set; }
    /// <summary>
    /// Columna de orden. Abiertas: fecha | codigo | cliente | caso | tomada | gestion.
    /// Cerradas: codigo | cliente | gestor | cierre | gestiones.
    /// </summary>
    [BindProperty(SupportsGet = true)] public string? Orden { get; set; }
    /// <summary>"asc" | "desc". Vacío = sentido por defecto de la columna.</summary>
    [BindProperty(SupportsGet = true)] public string? Dir { get; set; }
    /// <summary>"detalle": la acción se hizo desde Regcheq/Detalle y se vuelve ahí.</summary>
    [BindProperty(SupportsGet = true)] public string? Origen { get; set; }

    public bool EsCerradas => string.Equals(Vista, "cerradas", StringComparison.OrdinalIgnoreCase);

    /// <summary>Por defecto: abiertas de la más antigua a la más nueva; cerradas de la última cerrada a la primera.</summary>
    public string OrdenActual => string.IsNullOrWhiteSpace(Orden) ? (EsCerradas ? "cierre" : "fecha") : Orden.ToLowerInvariant();
    public bool Ascendente => Dir?.ToLowerInvariant() switch
    {
        "asc" => true,
        "desc" => false,
        _ => !(EsCerradas && OrdenActual == "cierre")
    };
    /// <summary>"Mis operaciones": aplica tanto a abiertas como a cerradas.</summary>
    public bool SoloMias => string.Equals(Asignacion, "mias", StringComparison.OrdinalIgnoreCase);
    public bool Configurado { get; private set; } = true;
    public string? Error { get; private set; }
    public string? UsuarioActualId { get; private set; }
    public bool EsAdmin { get; private set; }
    /// <summary>False para Revisor: ve todo pero no toma ni registra gestiones.</summary>
    public bool PuedeGestionar { get; private set; }

    /// <summary>Operaciones abiertas por usuario (solo administrador).</summary>
    public List<CargaUsuario> Carga { get; private set; } = [];
    public string? NombreUsuarioFiltro { get; private set; }

    public ResumenGestion Totales { get; private set; } = new();
    public List<FilaGestion> Filas { get; private set; } = [];
    /// <summary>Vehículo de la cotización Netcar por Id de operación (solo las filas de la página).</summary>
    public Dictionary<int, VehiculoOperacion> Vehiculos { get; private set; } = [];
    public List<GestionOperacion> Cerradas { get; private set; } = [];
    public int TotalCerradas { get; private set; }
    public int TotalFiltrado { get; private set; }
    public int TotalPaginas => Math.Max(1, (int)Math.Ceiling(TotalFiltrado / (double)TamanoPagina));
    /// <summary>Usuarios a los que un administrador puede reasignar (Id, nombre).</summary>
    public List<(string Id, string Nombre)> UsuariosGestion { get; private set; } = [];

    public async Task OnGetAsync(CancellationToken ct)
    {
        Pagina = Math.Max(1, Pagina);
        UsuarioActualId = _userManager.GetUserId(User);
        EsAdmin = User.IsInRole("Administrador");
        PuedeGestionar = TieneRolGestion();

        List<RegcheqPendienteGestion>? pendientes = null;
        try
        {
            pendientes = await _regcheq.ListarPendientesGestionAsync(InicioSeguimiento, ct);
            Configurado = pendientes is not null;
        }
        catch (Exception)
        {
            Error = "No se pudo consultar RegCheq. Intente nuevamente o revise el log.";
        }

        // Solo se sincronizan cierres con una lectura correcta de RegCheq: si falló, no se cierra nada.
        // (La tarea GestionSincronizacionWorker también lo hace cada 15 min; aquí se aprovecha la lectura ya hecha.)
        if (pendientes is not null)
            await _sincronizador.SincronizarAsync(pendientes, ct);

        var gestiones = await _db.GestionesOperacion.AsNoTracking()
            .Include(g => g.Usuario)
            .ToListAsync(ct);
        var abiertasPorOperacion = gestiones.Where(g => g.FechaCierre == null).ToDictionary(g => g.OperacionId);
        // Cerradas: el usuario que la tenía tomada al cerrarse sigue en UsuarioId.
        bool DelAlcance(GestionOperacion g) =>
            (!SoloMias || g.UsuarioId == UsuarioActualId) && (string.IsNullOrEmpty(Usuario) || g.UsuarioId == Usuario);
        TotalCerradas = gestiones.Count(g => g.FechaCierre != null && DelAlcance(g));

        if (!string.IsNullOrEmpty(Usuario))
            NombreUsuarioFiltro = NombreDe(gestiones.FirstOrDefault(g => g.UsuarioId == Usuario)?.Usuario
                                           ?? await _userManager.FindByIdAsync(Usuario));

        var filas = (pendientes ?? [])
            .Select(p => new FilaGestion(p, abiertasPorOperacion.GetValueOrDefault(p.Id)))
            .ToList();

        Totales = new ResumenGestion
        {
            Pendientes = filas.Count,
            SoloPep = filas.Count(f => f.Operacion.Caso == CasoGestion.Pep),
            SoloDof = filas.Count(f => f.Operacion.Caso == CasoGestion.Dof),
            PepYDof = filas.Count(f => f.Operacion.Caso == CasoGestion.PepYDof),
            SinTomar = filas.Count(f => f.Gestion?.UsuarioId is null),
            Mias = filas.Count(f => f.Gestion?.UsuarioId is { } u && u == UsuarioActualId)
        };

        if (EsCerradas)
        {
            var cerradas = gestiones.Where(g => g.FechaCierre != null && DelAlcance(g)
                                                && CoincideBusqueda(Q, g.CodigoOperacion, g.ClienteRut, g.ClienteNombre, null))
                .ToList();
            var gestionesPorCerrada = OrdenActual == "gestiones" ? await NotasPorGestionAsync(ct) : null;
            cerradas = (OrdenActual switch
                {
                    "codigo" => Ordenar(cerradas, g => g.CodigoOperacion),
                    "cliente" => Ordenar(cerradas, g => g.ClienteNombre ?? ""),
                    "gestor" => Ordenar(cerradas, g => g.Usuario is null ? "" : NombreDe(g.Usuario)),
                    "gestiones" => Ordenar(cerradas, g => gestionesPorCerrada!.GetValueOrDefault(g.Id).Cantidad),
                    _ => Ordenar(cerradas, g => g.FechaCierre)
                })
                .ThenByDescending(g => g.FechaCierre)
                .ToList();
            TotalFiltrado = cerradas.Count;
            Cerradas = cerradas.Skip((Pagina - 1) * TamanoPagina).Take(TamanoPagina).ToList();
            await CargarNotasAsync(Cerradas, ct);
            return;
        }

        var filtradas = filas.Where(f =>
                (Caso?.ToLowerInvariant() switch
                {
                    "pep" => f.Operacion.Caso == CasoGestion.Pep,
                    "dof" => f.Operacion.Caso == CasoGestion.Dof,
                    "ambos" => f.Operacion.Caso == CasoGestion.PepYDof,
                    _ => true
                })
                && (Asignacion?.ToLowerInvariant() switch
                {
                    "sin" => f.Gestion?.UsuarioId is null,
                    "mias" => f.Gestion?.UsuarioId is { } u && u == UsuarioActualId,
                    "tomadas" => f.Gestion?.UsuarioId is not null,
                    _ => true
                })
                && (string.IsNullOrEmpty(Usuario) || f.Gestion?.UsuarioId == Usuario)
                && CoincideBusqueda(Q, f.Operacion.Codigo, f.Operacion.Rut, f.Operacion.Nombre, f.Operacion.Referencia))
            .ToList();

        var notasPorGestion = OrdenActual == "gestion" ? await NotasPorGestionAsync(ct) : null;
        filtradas = (OrdenActual switch
            {
                "codigo" => Ordenar(filtradas, f => f.Operacion.Codigo),
                "cliente" => Ordenar(filtradas, f => f.Operacion.Nombre ?? ""),
                "caso" => Ordenar(filtradas, f => (int)f.Operacion.Caso),
                // Sin tomar al final en ascendente (al inicio en descendente).
                "tomada" => Ordenar(filtradas, f => (f.Gestion?.UsuarioId is null, f.Gestion?.UsuarioId is null ? "" : NombreDe(f.Gestion.Usuario))),
                // Sin gestiones = la más antigua posible.
                "gestion" => Ordenar(filtradas, f => f.Gestion is null ? DateTime.MinValue
                                                     : notasPorGestion!.GetValueOrDefault(f.Gestion.Id).Ultima ?? DateTime.MinValue),
                _ => Ordenar(filtradas, f => f.Operacion.FechaCreacion)
            })
            .ThenBy(f => f.Operacion.FechaCreacion).ThenBy(f => f.Operacion.Id)
            .ToList();

        TotalFiltrado = filtradas.Count;
        Filas = filtradas.Skip((Pagina - 1) * TamanoPagina).Take(TamanoPagina).ToList();
        await CargarNotasAsync(Filas.Where(f => f.Gestion is not null).Select(f => f.Gestion!).ToList(), ct);
        if (Filas.Count > 0)
            Vehiculos = await _vehiculos.ObtenerAsync(RegcheqTipo.Natural, Filas.Select(f => f.Operacion.Id).ToList(), ct);

        if (EsAdmin)
        {
            UsuariosGestion = await UsuariosAsignablesAsync();
            Carga = await CalcularCargaAsync(filas, ct);
        }
    }

    public async Task<IActionResult> OnPostTomarAsync(int id, CancellationToken ct)
    {
        if (!TieneRolGestion()) return Forbid();
        var yo = await _userManager.GetUserAsync(User);
        if (yo is null) return Challenge();

        var op = await BuscarPendienteAsync(id, ct);
        if (op is null) return Volver(id);

        var g = await _db.GestionesOperacion.AsNoTracking().Include(x => x.Usuario).FirstOrDefaultAsync(x => x.OperacionId == id, ct);
        if (g?.UsuarioId is not null && g.UsuarioId != yo.Id)
        {
            TempData["Error"] = $"La operación {op.Codigo} ya fue tomada por {NombreDe(g.Usuario)}.";
            return Volver(id);
        }
        if (g?.UsuarioId == yo.Id) return Volver(id);

        if (g is null)
        {
            // Primera toma: el índice único por operación impide que dos usuarios la creen a la vez.
            var nueva = _db.GestionesOperacion.Add(new GestionOperacion { OperacionId = id }).Entity;
            CopiarDatos(nueva, op);
            nueva.UsuarioId = yo.Id;
            nueva.FechaToma = DateTime.UtcNow;
            nueva.Notas.Add(NotaSistema(yo, "Tomó la operación."));
            try
            {
                await _db.SaveChangesAsync(ct);
            }
            catch (DbUpdateException ex)
            {
                _logger.LogWarning(ex, "Conflicto al tomar la operación RegCheq {Id}", id);
                return await ConflictoTomaAsync(id, op, ct);
            }
        }
        else
        {
            // Ya existía (liberada o reabierta): UPDATE condicional y atómico. Si otro usuario la tomó
            // entre la lectura y este punto, la condición UsuarioId == null no se cumple y no se pisa.
            await using var tx = await _db.Database.BeginTransactionAsync(ct);
            var ahora = DateTime.UtcNow;
            var copia = new GestionOperacion();
            CopiarDatos(copia, op);
            var filas = await _db.GestionesOperacion
                .Where(x => x.Id == g.Id && x.UsuarioId == null)
                .ExecuteUpdateAsync(s => s
                    .SetProperty(x => x.UsuarioId, yo.Id)
                    .SetProperty(x => x.FechaToma, ahora)
                    .SetProperty(x => x.FechaCierre, (DateTime?)null)
                    .SetProperty(x => x.CodigoOperacion, copia.CodigoOperacion)
                    .SetProperty(x => x.ClienteRut, copia.ClienteRut)
                    .SetProperty(x => x.ClienteNombre, copia.ClienteNombre), ct);
            if (filas == 0)
            {
                _logger.LogWarning("Conflicto al tomar la operación RegCheq {Id}: ya estaba tomada", id);
                return await ConflictoTomaAsync(id, op, ct);
            }

            var nota = NotaSistema(yo, "Tomó la operación.");
            nota.GestionOperacionId = g.Id;
            _db.GestionesOperacionNotas.Add(nota);
            await _db.SaveChangesAsync(ct);
            await tx.CommitAsync(ct);
        }

        TempData["Success"] = $"Tomaste la operación {op.Codigo}.";
        return Volver(id);
    }

    /// <summary>Otro usuario tomó la operación en el mismo instante: informa quién la tiene.</summary>
    private async Task<IActionResult> ConflictoTomaAsync(int id, RegcheqPendienteGestion op, CancellationToken ct)
    {
        _db.ChangeTracker.Clear();
        var actual = await _db.GestionesOperacion.AsNoTracking().Include(x => x.Usuario)
            .FirstOrDefaultAsync(x => x.OperacionId == id, ct);
        TempData["Error"] = actual?.Usuario is not null
            ? $"La operación {op.Codigo} acaba de ser tomada por {NombreDe(actual.Usuario)}."
            : $"Otro usuario tomó la operación {op.Codigo} al mismo tiempo. Revise quién la tiene.";
        return Volver(id);
    }

    public async Task<IActionResult> OnPostSoltarAsync(int id, CancellationToken ct)
    {
        if (!TieneRolGestion()) return Forbid();
        var yo = await _userManager.GetUserAsync(User);
        if (yo is null) return Challenge();

        var g = await _db.GestionesOperacion.FirstOrDefaultAsync(x => x.OperacionId == id && x.FechaCierre == null, ct);
        if (g?.UsuarioId is null) return Volver(id);
        if (g.UsuarioId != yo.Id && !User.IsInRole("Administrador"))
        {
            TempData["Error"] = "Solo quien tomó la operación o un administrador puede liberarla.";
            return Volver(id);
        }

        g.UsuarioId = null;
        g.FechaToma = null;
        g.Notas.Add(NotaSistema(yo, "Liberó la operación."));
        await _db.SaveChangesAsync(ct);
        TempData["Success"] = $"La operación {g.CodigoOperacion} quedó sin tomar.";
        return Volver(id);
    }

    public async Task<IActionResult> OnPostReasignarAsync(int id, string? usuarioId, CancellationToken ct)
    {
        if (!User.IsInRole("Administrador")) return Forbid();
        var yo = await _userManager.GetUserAsync(User);
        if (yo is null) return Challenge();

        var destino = await ValidarDestinoAsync(usuarioId);
        if (destino is null) return Volver(id);

        var op = await BuscarPendienteAsync(id, ct);
        if (op is null) return Volver(id);

        var g = await _db.GestionesOperacion.FirstOrDefaultAsync(x => x.OperacionId == id, ct);
        if (g?.UsuarioId == destino.Id) return Volver(id);

        Asignar(g, op, destino, yo);
        await _db.SaveChangesAsync(ct);
        TempData["Success"] = $"Operación {op.Codigo} asignada a {NombreDe(destino)}.";
        return Volver(id);
    }

    /// <summary>Administrador: asigna (o libera) las operaciones marcadas en la tabla.</summary>
    public async Task<IActionResult> OnPostSeleccionAsync(int[] ids, string? accion, string? usuarioId, CancellationToken ct)
    {
        if (!User.IsInRole("Administrador")) return Forbid();
        var yo = await _userManager.GetUserAsync(User);
        if (yo is null) return Challenge();

        ids = ids.Distinct().ToArray();
        if (ids.Length == 0)
        {
            TempData["Error"] = "Marque al menos una operación.";
            return Volver(null);
        }

        var gestiones = await _db.GestionesOperacion
            .Where(g => ids.Contains(g.OperacionId))
            .ToDictionaryAsync(g => g.OperacionId, ct);

        if (string.Equals(accion, "liberar", StringComparison.OrdinalIgnoreCase))
        {
            var liberadas = 0;
            foreach (var g in gestiones.Values.Where(g => g.FechaCierre == null && g.UsuarioId != null))
            {
                g.UsuarioId = null;
                g.FechaToma = null;
                g.Notas.Add(NotaSistema(yo, "Liberó la operación (acción masiva)."));
                liberadas++;
            }
            await _db.SaveChangesAsync(ct);
            TempData["Success"] = $"{liberadas} operación(es) liberada(s).";
            return Volver(null);
        }

        var destino = await ValidarDestinoAsync(usuarioId);
        if (destino is null) return Volver(null);

        var pendientes = await PendientesPorIdAsync(ct);
        if (pendientes is null) return Volver(null);

        int asignadas = 0, omitidas = 0;
        foreach (var id in ids)
        {
            if (!pendientes.TryGetValue(id, out var op)) { omitidas++; continue; }
            var g = gestiones.GetValueOrDefault(id);
            if (g?.UsuarioId == destino.Id) continue;
            Asignar(g, op, destino, yo, " (acción masiva)");
            asignadas++;
        }
        await _db.SaveChangesAsync(ct);
        TempData["Success"] = $"{asignadas} operación(es) asignada(s) a {NombreDe(destino)}."
                              + (omitidas > 0 ? $" {omitidas} ya no tenían firmas pendientes y se omitieron." : "");
        return Volver(null);
    }

    /// <summary>Administrador: mueve todas las operaciones abiertas de un usuario a otro (vacaciones, salida, etc.).</summary>
    public async Task<IActionResult> OnPostMoverTodasAsync(string? origenId, string? usuarioId, CancellationToken ct)
    {
        if (!User.IsInRole("Administrador")) return Forbid();
        var yo = await _userManager.GetUserAsync(User);
        if (yo is null) return Challenge();
        if (string.IsNullOrWhiteSpace(origenId)) return Volver(null);

        var destino = await ValidarDestinoAsync(usuarioId);
        if (destino is null) return Volver(null);
        if (destino.Id == origenId)
        {
            TempData["Error"] = "El usuario de destino es el mismo que el de origen.";
            return Volver(null);
        }

        var origen = await _userManager.FindByIdAsync(origenId);
        var gestiones = await _db.GestionesOperacion
            .Where(g => g.UsuarioId == origenId && g.FechaCierre == null)
            .ToListAsync(ct);
        foreach (var g in gestiones)
        {
            g.UsuarioId = destino.Id;
            g.FechaToma = DateTime.UtcNow;
            g.Notas.Add(NotaSistema(yo, $"Reasignó la operación de {NombreDe(origen)} a {NombreDe(destino)} (traspaso de cartera)."));
        }
        await _db.SaveChangesAsync(ct);
        TempData["Success"] = $"{gestiones.Count} operación(es) de {NombreDe(origen)} traspasada(s) a {NombreDe(destino)}.";
        if (Usuario == origenId) Usuario = destino.Id;
        return Volver(null);
    }

    public async Task<IActionResult> OnPostNotaAsync(int id, string? texto, CancellationToken ct)
    {
        if (!TieneRolGestion()) return Forbid();
        var yo = await _userManager.GetUserAsync(User);
        if (yo is null) return Challenge();

        texto = texto?.Trim();
        if (string.IsNullOrEmpty(texto))
        {
            TempData["Error"] = "Escriba la gestión realizada.";
            return Volver(id);
        }
        if (texto.Length > LargoMaximoNota)
        {
            TempData["Error"] = $"La gestión no puede superar {LargoMaximoNota} caracteres.";
            return Volver(id);
        }

        var g = await _db.GestionesOperacion.FirstOrDefaultAsync(x => x.OperacionId == id && x.FechaCierre == null, ct);
        if (g?.UsuarioId is null)
        {
            TempData["Error"] = "Primero tome la operación para registrar gestiones.";
            return Volver(id);
        }
        if (g.UsuarioId != yo.Id && !User.IsInRole("Administrador"))
        {
            TempData["Error"] = "Solo quien tomó la operación o un administrador puede registrar gestiones.";
            return Volver(id);
        }

        g.Notas.Add(new GestionOperacionNota { UsuarioId = yo.Id, UsuarioNombre = NombreDe(yo), Texto = texto });
        await _db.SaveChangesAsync(ct);
        TempData["Success"] = "Gestión registrada.";
        return Volver(id);
    }

    /// <summary>Query string para paginar o cambiar de vista manteniendo los filtros.</summary>
    public Dictionary<string, string?> Ruta(int? pagina = null, string? vista = null, int? abrir = null) => new()
    {
        ["vista"] = vista ?? (EsCerradas ? "cerradas" : null),
        ["q"] = Q,
        ["caso"] = Caso,
        ["asignacion"] = Asignacion,
        ["usuario"] = Usuario,
        ["orden"] = Orden,
        ["dir"] = Dir,
        ["pagina"] = (pagina ?? 1).ToString(),
        ["abrir"] = abrir?.ToString()
    };

    /// <summary>Enlace de un encabezado: ordena por esa columna; si ya lo está, invierte el sentido. Vuelve a la página 1.</summary>
    public Dictionary<string, string?> RutaOrden(string columna)
    {
        var ruta = Ruta();
        ruta["orden"] = columna;
        ruta["dir"] = OrdenActual == columna ? (Ascendente ? "desc" : "asc") : "asc";
        return ruta;
    }

    private IOrderedEnumerable<T> Ordenar<T, TClave>(IEnumerable<T> items, Func<T, TClave> clave) =>
        Ascendente ? items.OrderBy(clave) : items.OrderByDescending(clave);

    /// <summary>Por gestión: fecha de la última gestión escrita por un usuario y cuántas hay (sin eventos del sistema).</summary>
    private async Task<Dictionary<int, (DateTime? Ultima, int Cantidad)>> NotasPorGestionAsync(CancellationToken ct) =>
        (await _db.GestionesOperacionNotas.AsNoTracking()
            .Where(n => !n.EsSistema)
            .GroupBy(n => n.GestionOperacionId)
            .Select(g => new { g.Key, Ultima = g.Max(n => n.CreatedAt), Cantidad = g.Count() })
            .ToListAsync(ct))
        .ToDictionary(x => x.Key, x => ((DateTime?)x.Ultima, x.Cantidad));

    public static string NombreDe(ApplicationUser? u) =>
        u is null ? "—" : !string.IsNullOrWhiteSpace(u.FullName) ? u.FullName : u.UserName ?? "—";

    public static string Iniciales(string nombre)
    {
        var partes = nombre.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        return partes.Length switch
        {
            0 => "?",
            1 => partes[0][..1].ToUpperInvariant(),
            _ => $"{partes[0][0]}{partes[1][0]}".ToUpperInvariant()
        };
    }

    public static (string Texto, string Clase) Etiqueta(CasoGestion caso) => caso switch
    {
        CasoGestion.Pep => ("Caso 1 · Solo PEP", "gs-caso-pep"),
        CasoGestion.Dof => ("Caso 2 · Solo DOF", "gs-caso-dof"),
        _ => ("Caso 3 · PEP y DOF", "gs-caso-ambos")
    };

    private IActionResult Volver(int? id)
    {
        if (id.HasValue && string.Equals(Origen, "detalle", StringComparison.OrdinalIgnoreCase))
            return RedirectToPage("./Detalle", null, new { tipo = "natural", id }, "gestion");

        var ruta = Ruta(Pagina, abrir: id);
        return RedirectToPage(ruta.Where(kv => kv.Value is not null).ToDictionary(kv => kv.Key, kv => (object?)kv.Value));
    }

    private bool TieneRolGestion() => RolesGestion.Any(User.IsInRole);

    /// <summary>Usuario de destino válido (activo y con rol de gestión); si no, deja el error en TempData y devuelve null.</summary>
    private async Task<ApplicationUser?> ValidarDestinoAsync(string? usuarioId)
    {
        var destino = string.IsNullOrWhiteSpace(usuarioId) ? null : await _userManager.FindByIdAsync(usuarioId);
        if (destino is null || !destino.IsActive || !(await _userManager.GetRolesAsync(destino)).Any(RolesGestion.Contains))
        {
            TempData["Error"] = "Seleccione un usuario activo de Cumplimiento, Call Center o Administrador.";
            return null;
        }
        return destino;
    }

    /// <summary>Asigna la operación a <paramref name="destino"/>, creando la gestión si no existía. No guarda.</summary>
    private void Asignar(GestionOperacion? g, RegcheqPendienteGestion op, ApplicationUser destino, ApplicationUser autor, string sufijo = "")
    {
        g ??= _db.GestionesOperacion.Add(new GestionOperacion { OperacionId = op.Id }).Entity;
        CopiarDatos(g, op);
        g.UsuarioId = destino.Id;
        g.FechaToma = DateTime.UtcNow;
        g.FechaCierre = null;
        g.Notas.Add(NotaSistema(autor, $"Reasignó la operación a {NombreDe(destino)}{sufijo}."));
    }

    private async Task<Dictionary<int, RegcheqPendienteGestion>?> PendientesPorIdAsync(CancellationToken ct)
    {
        try
        {
            var lista = await _regcheq.ListarPendientesGestionAsync(InicioSeguimiento, ct);
            if (lista is null) TempData["Error"] = "La conexión RegCheqConnection no está configurada.";
            return lista?.ToDictionary(p => p.Id);
        }
        catch (Exception)
        {
            TempData["Error"] = "No se pudo consultar RegCheq. Intente nuevamente.";
            return null;
        }
    }

    /// <summary>
    /// Operaciones abiertas por usuario. Incluye a los usuarios con rol de gestión sin operaciones (para repartir)
    /// y a quienes tienen operaciones pero ya no pueden gestionarlas (inactivos o Revisor), para reasignarlas.
    /// </summary>
    private async Task<List<CargaUsuario>> CalcularCargaAsync(List<FilaGestion> filas, CancellationToken ct)
    {
        var tomadas = filas.Where(f => f.Gestion?.UsuarioId is not null).ToList();
        var gestionIds = tomadas.Select(f => f.Gestion!.Id).ToList();
        var conGestiones = gestionIds.Count == 0
            ? new HashSet<int>()
            : (await _db.GestionesOperacionNotas.AsNoTracking()
                .Where(n => !n.EsSistema && gestionIds.Contains(n.GestionOperacionId))
                .Select(n => n.GestionOperacionId).Distinct().ToListAsync(ct)).ToHashSet();

        var asignables = UsuariosGestion.ToDictionary(u => u.Id, u => u.Nombre);
        var carga = tomadas
            .GroupBy(f => f.Gestion!.UsuarioId!)
            .Select(grp => new CargaUsuario(
                grp.Key,
                NombreDe(grp.First().Gestion!.Usuario),
                grp.Count(),
                grp.Count(f => f.Operacion.Caso == CasoGestion.Pep),
                grp.Count(f => f.Operacion.Caso == CasoGestion.Dof),
                grp.Count(f => f.Operacion.Caso == CasoGestion.PepYDof),
                grp.Min(f => f.Gestion!.FechaToma),
                grp.Count(f => !conGestiones.Contains(f.Gestion!.Id)),
                asignables.ContainsKey(grp.Key)))
            .ToList();

        carga.AddRange(asignables
            .Where(u => carga.All(c => c.UsuarioId != u.Key))
            .Select(u => new CargaUsuario(u.Key, u.Value, 0, 0, 0, 0, null, 0, true)));

        return carga.OrderByDescending(c => c.Total).ThenBy(c => c.Nombre).ToList();
    }

    private async Task CargarNotasAsync(List<GestionOperacion> gestiones, CancellationToken ct)
    {
        if (gestiones.Count == 0) return;
        var ids = gestiones.Select(g => g.Id).ToList();
        var notas = await _db.GestionesOperacionNotas.AsNoTracking()
            .Where(n => ids.Contains(n.GestionOperacionId))
            .OrderByDescending(n => n.CreatedAt).ThenByDescending(n => n.Id)
            .ToListAsync(ct);
        var porGestion = notas.ToLookup(n => n.GestionOperacionId);
        foreach (var g in gestiones) g.Notas = porGestion[g.Id].ToList();
    }

    private async Task<RegcheqPendienteGestion?> BuscarPendienteAsync(int id, CancellationToken ct)
    {
        try
        {
            var op = (await _regcheq.ListarPendientesGestionAsync(InicioSeguimiento, ct))?.FirstOrDefault(p => p.Id == id);
            if (op is null)
                TempData["Error"] = "La operación ya no tiene firmas pendientes (o no existe).";
            return op;
        }
        catch (Exception)
        {
            TempData["Error"] = "No se pudo consultar RegCheq. Intente nuevamente.";
            return null;
        }
    }

    private async Task<List<(string Id, string Nombre)>> UsuariosAsignablesAsync()
    {
        var usuarios = new Dictionary<string, ApplicationUser>();
        foreach (var rol in RolesGestion)
            foreach (var u in await _userManager.GetUsersInRoleAsync(rol))
                if (u.IsActive) usuarios.TryAdd(u.Id, u);
        return usuarios.Values.Select(u => (u.Id, NombreDe(u))).OrderBy(u => u.Item2).ToList();
    }

    private static void CopiarDatos(GestionOperacion g, RegcheqPendienteGestion op)
    {
        g.CodigoOperacion = op.Codigo;
        g.ClienteRut = op.Rut is { Length: > 20 } r ? r[..20] : op.Rut;
        g.ClienteNombre = op.Nombre is { Length: > 300 } n ? n[..300] : op.Nombre;
    }

    private static GestionOperacionNota NotaSistema(ApplicationUser autor, string texto) =>
        new() { UsuarioId = autor.Id, UsuarioNombre = NombreDe(autor), Texto = texto, EsSistema = true };

    /// <summary>Busca por código, referencia, RUT (sin puntos ni guion) o nombre.</summary>
    private static bool CoincideBusqueda(string? q, int? codigo, string? rut, string? nombre, string? referencia)
    {
        if (string.IsNullOrWhiteSpace(q)) return true;
        q = q.Trim();
        static string Limpio(string s) => s.Replace(".", "").Replace("-", "").Replace(" ", "").ToUpperInvariant();
        var ql = Limpio(q);
        return (codigo?.ToString() == ql)
               || (referencia?.Contains(q, StringComparison.OrdinalIgnoreCase) ?? false)
               || (ql.Length >= 4 && rut is not null && Limpio(rut).Contains(ql))
               || (nombre?.Contains(q, StringComparison.CurrentCultureIgnoreCase) ?? false);
    }
}

public record FilaGestion(RegcheqPendienteGestion Operacion, GestionOperacion? Gestion);

/// <param name="SinGestiones">Operaciones tomadas sin ninguna gestión escrita por el usuario.</param>
/// <param name="PuedeGestionar">False si el usuario ya no está activo o no tiene rol de gestión: conviene traspasar su cartera.</param>
public record CargaUsuario(string UsuarioId, string Nombre, int Total, int SoloPep, int SoloDof, int PepYDof,
                           DateTime? TomaMasAntigua, int SinGestiones, bool PuedeGestionar);

public class ResumenGestion
{
    public int Pendientes { get; set; }
    public int SoloPep { get; set; }
    public int SoloDof { get; set; }
    public int PepYDof { get; set; }
    public int SinTomar { get; set; }
    public int Mias { get; set; }
}
