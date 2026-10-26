using System.Globalization;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Html;

namespace FormulariosUAF.Pages.Regcheq;

/// <summary>Helpers de presentación para las vistas RegCheq.</summary>
public static class RegcheqUi
{
    private static readonly CultureInfo Chile = new("es-CL");

    /// <summary>Badge Sí / No / — para columnas bit (null = sin dato).</summary>
    public static IHtmlContent SiNo(bool? valor, string claseSi = "bg-success", string claseNo = "bg-secondary-subtle text-secondary-emphasis") =>
        valor switch
        {
            true => new HtmlString($"<span class=\"badge {claseSi}\">Sí</span>"),
            false => new HtmlString($"<span class=\"badge {claseNo}\">No</span>"),
            _ => new HtmlString("<span class=\"text-muted\">—</span>")
        };

    /// <summary>Firma: "Firmado dd/MM/yyyy" en verde, "No" en gris, "—" sin dato.</summary>
    public static IHtmlContent Firma(bool? firmo, DateTime? fecha) =>
        firmo switch
        {
            true => new HtmlString($"<span class=\"badge bg-success\"><i class=\"fa-solid fa-signature me-1\"></i>Firmado</span>"
                                   + (fecha.HasValue ? $"<div class=\"small text-muted\">{fecha.Value:dd/MM/yyyy HH:mm}</div>" : "")),
            false => new HtmlString("<span class=\"badge bg-warning text-dark\">Sin firma</span>"),
            _ => new HtmlString("<span class=\"text-muted\">—</span>")
        };

    public static IHtmlContent Pep(bool? pep, int? nivel = null) =>
        pep == true
            ? new HtmlString($"<span class=\"badge bg-danger\"><i class=\"fa-solid fa-triangle-exclamation me-1\"></i>PEP{(nivel.HasValue ? $" (nivel {nivel})" : "")}</span>")
            : SiNo(pep);

    /// <summary>Lista pepChile: "Coincide" en rojo, "No coincide" en verde, "—" si no hay registro pepChile.</summary>
    public static IHtmlContent ListaPep(bool? coincide) =>
        coincide switch
        {
            true => new HtmlString("<span class=\"badge bg-danger\"><i class=\"fa-solid fa-list-check me-1\"></i>Coincide</span>"),
            false => new HtmlString("<span class=\"badge bg-success-subtle text-success-emphasis\">No coincide</span>"),
            _ => new HtmlString("<span class=\"text-muted\" title=\"Sin registro de lista pepChile\">—</span>")
        };

    /// <summary>Estado de firmas PEP/DOF (misma regla que la vista Gestión).</summary>
    public static IHtmlContent EstadoFirmas(Services.EstadoFirmas? estado) => estado switch
    {
        Services.EstadoFirmas.Completa => new HtmlString("<span class=\"badge bg-success\"><i class=\"fa-solid fa-circle-check me-1\"></i>Completa</span>"),
        Services.EstadoFirmas.Pep => new HtmlString("<span class=\"badge bg-danger-subtle text-danger-emphasis\" title=\"Requiere PEP sin firmar; no requiere DOF\">Caso 1 · Falta PEP</span>"),
        Services.EstadoFirmas.Dof => new HtmlString("<span class=\"badge bg-warning-subtle text-warning-emphasis\" title=\"Requiere DOF sin firmar; no requiere PEP\">Caso 2 · Falta DOF</span>"),
        Services.EstadoFirmas.PepYDof => new HtmlString("<span class=\"badge\" style=\"background:#ede2fe;color:#5a2ca0\" title=\"Requiere PEP y DOF; falta al menos uno\">Caso 3 · PEP y DOF</span>"),
        Services.EstadoFirmas.SinSeguimiento => new HtmlString("<span class=\"badge bg-secondary-subtle text-secondary-emphasis\" title=\"Operación anterior al seguimiento automático de firmas: no se puede saber si se firmó\">Sin seguimiento</span>"),
        _ => new HtmlString("<span class=\"text-muted\">—</span>")
    };

    /// <summary>Días a partir de los cuales una operación pendiente se marca en ámbar / rojo (igual que "Carga por usuario").</summary>
    public const int DiasAviso = 7, DiasCritico = 30;

    /// <summary>
    /// Antigüedad de una operación pendiente: "hoy" / "hace N días", en ámbar desde <see cref="DiasAviso"/>
    /// y en rojo desde <see cref="DiasCritico"/>. Las fechas de RegCheq vienen en hora local del servidor.
    /// </summary>
    public static IHtmlContent Antiguedad(DateTime? creada)
    {
        if (creada is null) return HtmlString.Empty;
        var dias = (int)(DateTime.Now.Date - creada.Value.Date).TotalDays;
        var (clase, icono) = dias >= DiasCritico ? ("gs-edad critica", "fa-fire")
                           : dias >= DiasAviso ? ("gs-edad aviso", "fa-hourglass-half")
                           : ("gs-edad", "fa-clock");
        var texto = dias <= 0 ? "hoy" : dias == 1 ? "hace 1 día" : $"hace {dias.ToString("N0", Chile)} días";
        return new HtmlString($"<span class=\"{clase}\" title=\"Días desde que RegCheq creó la operación\"><i class=\"fa-solid {icono} me-1\"></i>{texto}</span>");
    }

    /// <summary>Texto tal como viene de RegCheq (estados sin catálogo conocido), o "—".</summary>
    public static IHtmlContent Texto(string? valor) =>
        string.IsNullOrWhiteSpace(valor)
            ? new HtmlString("<span class=\"text-muted\">—</span>")
            : new HtmlString(WebUtility.HtmlEncode(valor));

    /// <summary>
    /// Monto con el mismo formato en todas las vistas: "$12.490.000" en pesos, "12.490 USD" en otra divisa.
    /// Se prefiere el número; el texto formateado de RegCheq solo si no viene el número.
    /// </summary>
    public static string Monto(int? monto, string? formateado = null, string? divisa = null)
    {
        if (!monto.HasValue) return string.IsNullOrWhiteSpace(formateado) ? "—" : formateado!;
        var texto = monto.Value.ToString("N0", Chile);
        return string.IsNullOrWhiteSpace(divisa) || divisa.Trim().Equals("CLP", StringComparison.OrdinalIgnoreCase)
            ? $"${texto}" : $"{texto} {divisa.Trim().ToUpperInvariant()}";
    }

    public static string Pesos(decimal? monto) => monto.HasValue ? $"${monto.Value.ToString("N0", Chile)}" : "—";

    /// <summary>
    /// RUT con formato "8.690.723-2". RegCheq lo guarda sin puntos ni guion (con el DV al final); si el
    /// dígito verificador no cuadra se muestra tal cual, para no inventar un RUT.
    /// </summary>
    public static string Rut(string? rut)
    {
        if (string.IsNullOrWhiteSpace(rut)) return "—";
        var n = Helpers.RutHelper.Normalizar(rut);
        if (!Helpers.RutHelper.EsValido(n)) return rut.Trim();
        var cuerpo = long.Parse(n[..^1]).ToString("N0", Chile);
        return $"{cuerpo}-{n[^1]}";
    }

    // Partículas que van en minúscula dentro de un nombre ("Iván de la Cruz") y siglas de sociedades.
    private static readonly HashSet<string> Particulas = new(StringComparer.OrdinalIgnoreCase) { "de", "del", "la", "las", "los", "y", "e", "da", "do", "dos", "van", "von" };
    private static readonly HashSet<string> Siglas = new(StringComparer.OrdinalIgnoreCase) { "SPA", "LTDA", "EIRL", "SA", "S.A.", "S.A", "SAC", "SRL", "CIA", "CÍA" };

    /// <summary>
    /// Nombre de persona o empresa en mayúsculas/minúsculas ("IVAN DE LA CRUZ" → "Ivan de la Cruz").
    /// Las tildes no se pueden recuperar (no vienen en el dato). Las siglas de sociedad quedan en mayúscula.
    /// </summary>
    public static string NombrePropio(string? nombre)
    {
        if (string.IsNullOrWhiteSpace(nombre)) return "—";
        var palabras = nombre.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);
        for (var i = 0; i < palabras.Length; i++)
        {
            var p = palabras[i];
            if (Siglas.Contains(p)) palabras[i] = p.ToUpperInvariant();
            else if (i > 0 && Particulas.Contains(p)) palabras[i] = p.ToLower(Chile);
            else palabras[i] = MayusculaTrasApostrofo(Chile.TextInfo.ToTitleCase(p.ToLower(Chile))); // "Pérez-Cotapos", "O'Ryan"
        }
        return string.Join(' ', palabras);

        static string MayusculaTrasApostrofo(string s)
        {
            var i = s.IndexOf('\'');
            return i < 0 || i == s.Length - 1 ? s : s[..(i + 1)] + char.ToUpper(s[i + 1], Chile) + s[(i + 2)..];
        }
    }

    /// <summary>
    /// Marca/modelo legible ("SONET" → "Sonet") sin romper siglas: palabras cortas (MG, BMW, CX) o con
    /// números (CX-5, 3008) quedan como vienen.
    /// </summary>
    public static string Modelo(string? texto)
    {
        if (string.IsNullOrWhiteSpace(texto)) return "";
        return string.Join(' ', texto.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(p =>
            p.Length <= 3 || p.Any(char.IsDigit) || p.Any(char.IsLower) ? p : Chile.TextInfo.ToTitleCase(p.ToLower(Chile))));
    }

    /// <summary>
    /// Teléfono chileno agrupado para leerlo y dictarlo ("56912345678" → "+56 9 1234 5678") y el número para
    /// el enlace tel:. Si no se reconoce el formato, se muestra tal cual.
    /// </summary>
    public static (string Texto, string Enlace) Telefono(string? telefono)
    {
        if (string.IsNullOrWhiteSpace(telefono)) return ("", "");
        var d = new string(telefono.Where(char.IsDigit).ToArray());
        if (d.StartsWith("56") && d.Length == 11) d = d[2..];
        if (d.Length != 9) return (telefono.Trim(), new string(telefono.Where(c => char.IsDigit(c) || c == '+').ToArray()));
        var texto = d[0] switch
        {
            '9' => $"+56 9 {d[1..5]} {d[5..]}",   // celular
            '2' => $"+56 2 {d[1..5]} {d[5..]}",   // fijo Santiago
            _ => $"+56 {d[..2]} {d[2..5]} {d[5..]}" // fijo regiones (código de 2 dígitos)
        };
        return (texto, "+56" + d);
    }

    // Ficha de la cotización en Netcar; {0} = N° de cotización (ST_REFERENCENUMBER).
    private const string UrlNetcarSiglo21 = "https://netcar3.xxi.cl/cotizacion/cotShownegocio.aspx?k={0}&mt$opener=true";
    private const string UrlNetcarVyD = "http://netcar3.valdelarze.cl/cotizacion/cotShownegocio.aspx?k={0}&mt$opener=true";

    /// <summary>
    /// N° de cotización seguido de un botón "Ver en Netcar" (Siglo 21 o VyD según la empresa de la operación).
    /// Sin empresa o sin N° válido se muestra solo el número.
    /// </summary>
    public static IHtmlContent Cotizacion(string? referencia, Services.VehiculoOperacion? v, string clase = "", bool boton = true)
    {
        if (string.IsNullOrWhiteSpace(referencia) && v?.Cotizacion is null) return HtmlString.Empty;
        var texto = WebUtility.HtmlEncode(string.IsNullOrWhiteSpace(referencia) ? v!.Cotizacion.ToString() : referencia.Trim());
        return new HtmlString($"<span class=\"{clase}\">{texto}</span>" + (boton ? " " + BotonNetcarHtml(v, "rq-netcar") : ""));
    }

    /// <summary>Botón "Ver en Netcar" que abre la cotización en una pestaña nueva; nada si no se sabe la empresa.</summary>
    public static IHtmlContent BotonNetcar(Services.VehiculoOperacion? v, string clase = "btn-sm") =>
        new HtmlString(BotonNetcarHtml(v, clase));

    private static string BotonNetcarHtml(Services.VehiculoOperacion? v, string clase)
    {
        if (v?.Cotizacion is not { } numero || string.IsNullOrWhiteSpace(v.Empresa)) return "";
        var esSiglo21 = string.Equals(v.Empresa, "Siglo 21", StringComparison.OrdinalIgnoreCase);
        var url = string.Format(esSiglo21 ? UrlNetcarSiglo21 : UrlNetcarVyD, numero);
        return $"<a href=\"{WebUtility.HtmlEncode(url)}\" target=\"_blank\" class=\"btn btn-outline-primary {clase} text-nowrap\" "
               + $"title=\"Abrir la cotización {numero} en Netcar {(esSiglo21 ? "Siglo 21" : "VyD")}\">"
               + "<i class=\"fa-solid fa-arrow-up-right-from-square me-1\"></i>Ver en Netcar</a>";
    }

    /// <summary>"Marca Modelo" y vendedor bajo el cliente en los listados; nada si no hay vehículo.</summary>
    public static IHtmlContent VehiculoCorto(Services.VehiculoOperacion? v)
    {
        if (v?.Vehiculo is not { } veh || veh.Resumen.Length == 0) return HtmlString.Empty;
        var resumen = Modelo(veh.Resumen);
        var vendedor = string.IsNullOrWhiteSpace(veh.Vendedor) ? null : NombrePropio(veh.Vendedor);
        var titulo = $"{resumen} {Modelo(veh.Version)} · Cotización {veh.Cotizacion} ({veh.Empresa})" + (vendedor is null ? "" : $" · Vendedor: {vendedor}");
        return new HtmlString($"<div class=\"small text-muted text-truncate\" style=\"max-width:260px\" title=\"{WebUtility.HtmlEncode(titulo)}\">"
                              + $"<i class=\"fa-solid fa-car-side me-1\"></i>{WebUtility.HtmlEncode(resumen)}"
                              + (vendedor is null ? "" : $" <span class=\"opacity-75\">· {WebUtility.HtmlEncode(vendedor)}</span>")
                              + "</div>");
    }

    /// <summary>Badge "Efectivo" con el monto pagado en efectivo, para destacarlo (dato relevante para la UAF).</summary>
    public static IHtmlContent Efectivo(bool? efectivo, int? montoEfectivo, string? divisa) =>
        efectivo == true
            ? new HtmlString("<span class=\"badge bg-warning text-dark\" title=\"Operación con pago en efectivo\"><i class=\"fa-solid fa-money-bill-wave me-1\"></i>Efectivo"
                             + (montoEfectivo.HasValue ? $" {WebUtility.HtmlEncode(Monto(montoEfectivo, null, divisa))}" : "") + "</span>")
            : HtmlString.Empty;

    public static string Fecha(DateTime? fecha, bool conHora = false) =>
        fecha?.ToString(conHora ? "dd/MM/yyyy HH:mm" : "dd/MM/yyyy") ?? "—";

    /// <summary>Enlace si el valor es una URL http(s); si no, el texto.</summary>
    public static IHtmlContent EnlaceOTexto(string? valor, string etiqueta)
    {
        if (string.IsNullOrWhiteSpace(valor)) return new HtmlString("<span class=\"text-muted\">—</span>");
        if (Uri.TryCreate(valor, UriKind.Absolute, out var uri) && (uri.Scheme == Uri.UriSchemeHttps || uri.Scheme == Uri.UriSchemeHttp))
            return new HtmlString($"<a href=\"{WebUtility.HtmlEncode(uri.AbsoluteUri)}\" target=\"_blank\" rel=\"noopener noreferrer\">"
                                  + $"<i class=\"fa-solid fa-arrow-up-right-from-square me-1\"></i>{WebUtility.HtmlEncode(etiqueta)}</a>");
        return new HtmlString(WebUtility.HtmlEncode(valor));
    }

    /// <summary>JSON indentado para mostrar en &lt;pre&gt;; si no es JSON válido, el texto original.</summary>
    public static string JsonLegible(string? texto)
    {
        if (string.IsNullOrWhiteSpace(texto)) return "";
        try
        {
            using var doc = JsonDocument.Parse(texto);
            return JsonSerializer.Serialize(doc.RootElement, new JsonSerializerOptions
            {
                WriteIndented = true,
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            });
        }
        catch (JsonException)
        {
            return texto;
        }
    }

    // ---------- JSON visual (fichas en vez de texto JSON) ----------

    private const int ProfundidadMaxima = 6;
    private static readonly HtmlString SinValor = new("<span class=\"text-muted\">—</span>");
    private static readonly Regex FechaIso = new(@"^\d{4}-\d{2}-\d{2}([T ]\d{2}:\d{2}(:\d{2}(\.\d+)?)?(Z|[+-]\d{2}:?\d{2})?)?$", RegexOptions.Compiled);

    private static readonly Dictionary<string, string> Etiquetas = new(StringComparer.OrdinalIgnoreCase)
    {
        ["name"] = "Nombre", ["fullName"] = "Nombre completo", ["firstName"] = "Nombres", ["names"] = "Nombres",
        ["lastName"] = "Apellido", ["surname"] = "Apellido", ["position"] = "Cargo", ["positions"] = "Cargos",
        ["institution"] = "Institución", ["organization"] = "Organización", ["entity"] = "Entidad",
        ["startDate"] = "Fecha inicio", ["endDate"] = "Fecha término", ["date"] = "Fecha", ["period"] = "Período",
        ["dni"] = "RUT / DNI", ["rut"] = "RUT", ["pepLevel"] = "Nivel PEP", ["relationship"] = "Relación",
        ["relation"] = "Relación", ["relatives"] = "Relacionados", ["related"] = "Relacionados", ["family"] = "Familiares",
        ["type"] = "Tipo", ["country"] = "País", ["nationality"] = "Nacionalidad", ["source"] = "Fuente",
        ["description"] = "Descripción", ["region"] = "Región", ["commune"] = "Comuna", ["city"] = "Ciudad",
        ["address"] = "Dirección", ["phone"] = "Teléfono", ["status"] = "Estado", ["active"] = "Activo",
        ["url"] = "Enlace", ["link"] = "Enlace", ["id"] = "ID", ["observations"] = "Observaciones",
        ["comments"] = "Comentarios", ["birthDate"] = "Fecha de nacimiento", ["gender"] = "Género",
        ["risk"] = "Riesgo", ["coincidence"] = "Coincidencia", ["lastChecked"] = "Última revisión",
        ["personType"] = "Tipo de persona", ["listResult"] = "Resultado", ["additionalData"] = "Datos adicionales",
        ["info"] = "Información", ["data"] = "Datos", ["matches"] = "Coincidencias", ["match"] = "Coincidencia",
        ["score"] = "Puntaje", ["reason"] = "Motivo", ["category"] = "Categoría"
    };

    /// <summary>
    /// Muestra un JSON como fichas (etiqueta / valor, secciones y tarjetas) en vez de texto JSON.
    /// Si el texto no es JSON, lo muestra tal cual.
    /// </summary>
    public static IHtmlContent JsonVisual(string? texto)
    {
        if (string.IsNullOrWhiteSpace(texto)) return SinValor;
        if (!IntentarParsear(texto, out var raiz))
            return new HtmlString($"<div class=\"rj-texto\">{WebUtility.HtmlEncode(texto)}</div>");

        var sb = new StringBuilder();
        Valor(sb, raiz, 0);
        return new HtmlString(sb.ToString());
    }

    private static bool IntentarParsear(string texto, out JsonElement elemento)
    {
        try
        {
            using var doc = JsonDocument.Parse(texto);
            elemento = doc.RootElement.Clone();
            return true;
        }
        catch (JsonException)
        {
            elemento = default;
            return false;
        }
    }

    /// <summary>RegCheq a veces guarda JSON dentro de un string (doble serialización): lo desenvuelve.</summary>
    private static JsonElement Normalizar(JsonElement e)
    {
        if (e.ValueKind == JsonValueKind.String && e.GetString() is { } s)
        {
            var t = s.TrimStart();
            if ((t.StartsWith('{') || t.StartsWith('[')) && IntentarParsear(s, out var interno)) return interno;
        }
        return e;
    }

    private static bool EsComplejo(JsonElement e) =>
        e.ValueKind is JsonValueKind.Object or JsonValueKind.Array;

    private static void Valor(StringBuilder sb, JsonElement e, int nivel)
    {
        e = Normalizar(e);
        if (nivel > ProfundidadMaxima && EsComplejo(e))
        {
            sb.Append("<pre class=\"rj-pre\">").Append(WebUtility.HtmlEncode(JsonLegible(e.GetRawText()))).Append("</pre>");
            return;
        }
        switch (e.ValueKind)
        {
            case JsonValueKind.Object: Objeto(sb, e, nivel); break;
            case JsonValueKind.Array: Arreglo(sb, e, nivel); break;
            default: sb.Append(Primitivo(e)); break;
        }
    }

    private static void Objeto(StringBuilder sb, JsonElement e, int nivel)
    {
        var props = e.EnumerateObject().Select(p => (p.Name, Valor: Normalizar(p.Value))).ToList();
        if (props.Count == 0) { sb.Append(SinValor); return; }

        var simples = props.Where(p => !EsComplejo(p.Valor)).ToList();
        if (simples.Count > 0)
        {
            sb.Append("<dl class=\"rj-grid\">");
            foreach (var (nombre, valor) in simples)
                sb.Append("<div class=\"rj-item\"><dt>").Append(WebUtility.HtmlEncode(Etiqueta(nombre)))
                  .Append("</dt><dd>").Append(Primitivo(valor)).Append("</dd></div>");
            sb.Append("</dl>");
        }

        foreach (var (nombre, valor) in props.Where(p => EsComplejo(p.Valor)))
        {
            var esLista = valor.ValueKind == JsonValueKind.Array;
            sb.Append("<section class=\"rj-seccion\"><div class=\"rj-titulo\"><i class=\"fa-solid ")
              .Append(esLista ? "fa-layer-group" : "fa-folder-open").Append("\"></i>")
              .Append(WebUtility.HtmlEncode(Etiqueta(nombre)));
            if (esLista) sb.Append("<span class=\"rj-count\">").Append(valor.GetArrayLength()).Append("</span>");
            sb.Append("</div>");
            Valor(sb, valor, nivel + 1);
            sb.Append("</section>");
        }
    }

    private static void Arreglo(StringBuilder sb, JsonElement e, int nivel)
    {
        var items = e.EnumerateArray().Select(Normalizar).ToList();
        if (items.Count == 0) { sb.Append("<span class=\"text-muted small\">Sin elementos</span>"); return; }

        if (items.All(i => !EsComplejo(i)))
        {
            sb.Append("<div class=\"rj-chips\">");
            foreach (var i in items) sb.Append("<span class=\"rj-chip\">").Append(Primitivo(i)).Append("</span>");
            sb.Append("</div>");
            return;
        }

        sb.Append("<div class=\"rj-lista\">");
        for (var n = 0; n < items.Count; n++)
        {
            sb.Append("<div class=\"rj-card\">");
            if (items.Count > 1) sb.Append("<span class=\"rj-card-num\">#").Append(n + 1).Append("</span>");
            Valor(sb, items[n], nivel + 1);
            sb.Append("</div>");
        }
        sb.Append("</div>");
    }

    private static string Primitivo(JsonElement e)
    {
        switch (e.ValueKind)
        {
            case JsonValueKind.True: return "<span class=\"badge bg-success-subtle text-success-emphasis\">Sí</span>";
            case JsonValueKind.False: return "<span class=\"badge bg-secondary-subtle text-secondary-emphasis\">No</span>";
            case JsonValueKind.Number: return WebUtility.HtmlEncode(e.GetRawText());
            case JsonValueKind.String:
                var s = e.GetString();
                if (string.IsNullOrWhiteSpace(s)) return SinValor.Value!;
                if (Uri.TryCreate(s, UriKind.Absolute, out var uri) && (uri.Scheme == Uri.UriSchemeHttps || uri.Scheme == Uri.UriSchemeHttp))
                    return $"<a href=\"{WebUtility.HtmlEncode(uri.AbsoluteUri)}\" target=\"_blank\" rel=\"noopener noreferrer\">"
                           + $"<i class=\"fa-solid fa-arrow-up-right-from-square me-1\"></i>{WebUtility.HtmlEncode(uri.Host)}</a>";
                if (FechaIso.IsMatch(s) && DateTime.TryParse(s, CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind, out var fecha))
                {
                    if (fecha.Kind == DateTimeKind.Utc) fecha = fecha.ToLocalTime();
                    return fecha.TimeOfDay == TimeSpan.Zero ? fecha.ToString("dd/MM/yyyy") : fecha.ToString("dd/MM/yyyy HH:mm");
                }
                return WebUtility.HtmlEncode(s);
            default: return SinValor.Value!;
        }
    }

    /// <summary>"startDate" / "start_date" → "Fecha inicio" (diccionario) o "Start date" (genérico).</summary>
    private static string Etiqueta(string clave)
    {
        if (Etiquetas.TryGetValue(clave, out var conocida)) return conocida;
        var texto = Regex.Replace(clave, "(?<=[a-z0-9])(?=[A-Z])", " ").Replace('_', ' ').Replace('-', ' ').Trim();
        return texto.Length == 0 ? clave : char.ToUpper(texto[0], Chile) + texto[1..].ToLower(Chile);
    }
}
