namespace FormulariosUAF.Services;

public class ManualCompanyDataProvider : ICompanyDataProvider
{
    public Task<CompanyData?> GetByRutAsync(string rut) => Task.FromResult<CompanyData?>(null);
}
