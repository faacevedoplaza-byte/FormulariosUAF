namespace FormulariosUAF.Services;

public record CompanyData(
    string Rut,
    string BusinessName,
    string? Address,
    string? City,
    string? Activity,
    string? LegalRepresentative,
    string? EntityType
);

public interface ICompanyDataProvider
{
    Task<CompanyData?> GetByRutAsync(string rut);
}
