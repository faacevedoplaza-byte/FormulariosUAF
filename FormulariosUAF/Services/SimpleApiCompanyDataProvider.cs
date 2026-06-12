using System.Text.Json;

namespace FormulariosUAF.Services;

public class SimpleApiCompanyDataProvider : ICompanyDataProvider
{
    private readonly HttpClient _http;
    private readonly ILogger<SimpleApiCompanyDataProvider> _logger;
    private readonly IConfiguration _config;

    public SimpleApiCompanyDataProvider(HttpClient http, ILogger<SimpleApiCompanyDataProvider> logger, IConfiguration config)
    {
        _http = http;
        _logger = logger;
        _config = config;
    }

    public async Task<CompanyData?> GetByRutAsync(string rut)
    {
        try
        {
            var apiKey = _config["CompanyDataProvider:SimpleApi:ApiKey"];
            var baseUrl = _config["CompanyDataProvider:SimpleApi:BaseUrl"] ?? "https://api.simpleapi.cl/api/v1";
            var normalizedRut = rut.Replace(".", "").Replace("-", "").Trim();

            var request = new HttpRequestMessage(HttpMethod.Get, $"{baseUrl}/rut/{Uri.EscapeDataString(normalizedRut)}");
            if (!string.IsNullOrEmpty(apiKey))
                request.Headers.Add("Authorization", $"Bearer {apiKey}");

            var response = await _http.SendAsync(request);
            if (!response.IsSuccessStatusCode) return null;

            var json = await response.Content.ReadAsStringAsync();
            using var doc = JsonDocument.Parse(json);
            var root = doc.RootElement;

            var businessName = GetString(root, "razon_social") ?? GetString(root, "nombre");
            if (string.IsNullOrEmpty(businessName)) return null;

            return new CompanyData(
                Rut: rut,
                BusinessName: businessName,
                Address: GetString(root, "direccion") ?? GetString(root, "domicilio"),
                City: GetString(root, "ciudad") ?? GetString(root, "comuna"),
                Activity: GetString(root, "actividad") ?? GetString(root, "giro"),
                LegalRepresentative: GetString(root, "representante_legal"),
                EntityType: GetString(root, "tipo_empresa") ?? GetString(root, "tipo_contribuyente")
            );
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "SimpleAPI lookup falló para RUT {Rut}", rut);
            return null;
        }
    }

    private static string? GetString(JsonElement el, string property)
    {
        if (el.TryGetProperty(property, out var val) && val.ValueKind == JsonValueKind.String)
            return val.GetString();
        return null;
    }
}
