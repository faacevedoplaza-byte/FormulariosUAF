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
        Services.EstadoFirmas.NoRequiere => new HtmlString("<span class=\"badge bg-light text-secondary border\">No requiere</span>"),
        Services.EstadoFirmas.SinSeguimiento => new HtmlString("<span class=\"badge bg-secondary-subtle text-secondary-emphasis\" title=\"Operación anterior al seguimiento automático de firmas: no se puede saber si se firmó\">Sin seguimiento</span>"),
        _ => new HtmlString("<span class=\"text-muted\">—</span>")
    };

    /// <summary>Texto tal como viene de RegCheq (estados sin catálogo conocido), o "—".</summary>
    public static IHtmlContent Texto(string? valor) =>
        string.IsNullOrWhiteSpace(valor)
            ? new HtmlString("<span class=\"text-muted\">—</span>")
            : new HtmlString(WebUtility.HtmlEncode(valor));

    public static string Monto(int? monto, string? formateado = null, string? divisa = null)
    {
        if (!string.IsNullOrWhiteSpace(formateado)) return formateado!;
        if (!monto.HasValue) return "—";
        var texto = monto.Value.ToString("N0", Chile);
        return string.IsNullOrWhiteSpace(divisa) ? $"$ {texto}" : $"{texto} {divisa}";
    }

    public static string Pesos(decimal? monto) => monto.HasValue ? $"$ {monto.Value.ToString("N0", Chile)}" : "—";

    /// <summary>"Marca Modelo" bajo el cliente en los listados; nada si no hay vehículo.</summary>
    public static IHtmlContent VehiculoCorto(Services.VehiculoOperacion? v) =>
        v?.Vehiculo is { } veh && veh.Resumen.Length > 0
            ? new HtmlString($"<div class=\"small text-muted text-truncate\" style=\"max-width:240px\" title=\"{WebUtility.HtmlEncode($"{veh.Resumen} {veh.Version} · Cotización {veh.Cotizacion} ({veh.Empresa})")}\">"
                             + $"<i class=\"fa-solid fa-car-side me-1\"></i>{WebUtility.HtmlEncode(veh.Resumen)}</div>")
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
