using FormulariosUAF.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FormulariosUAF.Pages.Api;

[Authorize]
public class CompanyLookupModel : PageModel
{
    private readonly ICompanyDataProvider _provider;

    public CompanyLookupModel(ICompanyDataProvider provider)
    {
        _provider = provider;
    }

    public async Task<IActionResult> OnGetAsync([FromQuery] string rut)
    {
        if (string.IsNullOrWhiteSpace(rut))
            return BadRequest(new { error = "RUT requerido" });

        var data = await _provider.GetByRutAsync(rut.Trim());
        if (data is null)
            return NotFound(new { error = "No se encontraron datos para el RUT indicado" });

        return new JsonResult(new
        {
            rut = data.Rut,
            businessName = data.BusinessName,
            address = data.Address,
            city = data.City,
            activity = data.Activity,
            legalRepresentative = data.LegalRepresentative,
            entityType = data.EntityType
        });
    }
}
