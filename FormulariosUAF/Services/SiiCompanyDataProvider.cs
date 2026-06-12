using System.Text.Json;

namespace FormulariosUAF.Services;

public class SiiCompanyDataProvider : ICompanyDataProvider
{
    private readonly HttpClient _http;
    private readonly ILogger<SiiCompanyDataProvider> _logger;

    public SiiCompanyDataProvider(HttpClient http, ILogger<SiiCompanyDataProvider> logger)
    {
        _http = http;
        _logger = logger;
    }

    public async Task<CompanyData?> GetByRutAsync(string rut)
    {
        try
        {
            var normalizedRut = rut.Replace(".", "").Trim();
            // Endpoint público SII (contribuyente)
            var url = $"https://zeus.sii.cl/cvc_cgi/stc/getstc?RUT={Uri.EscapeDataString(normalizedRut)}";
            var response = await _http.GetAsync(url);
            if (!response.IsSuccessStatusCode) return null;

            var content = await response.Content.ReadAsStringAsync();
            // La respuesta del SII es XML/HTML — parseamos los campos básicos
            var businessName = ExtractField(content, "razon_social");
            if (string.IsNullOrEmpty(businessName)) return null;

            return new CompanyData(
                Rut: normalizedRut,
                BusinessName: businessName,
                Address: ExtractField(content, "domicilio"),
                City: ExtractField(content, "ciudad"),
                Activity: ExtractField(content, "actividad"),
                LegalRepresentative: null,
                EntityType: null
            );
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "SII lookup falló para RUT {Rut}", rut);
            return null;
        }
    }

    private static string? ExtractField(string html, string fieldName)
    {
        var marker = $"{fieldName}\":\"";
        var idx = html.IndexOf(marker, StringComparison.OrdinalIgnoreCase);
        if (idx < 0) return null;
        var start = idx + marker.Length;
        var end = html.IndexOf('"', start);
        return end > start ? html[start..end] : null;
    }
}
