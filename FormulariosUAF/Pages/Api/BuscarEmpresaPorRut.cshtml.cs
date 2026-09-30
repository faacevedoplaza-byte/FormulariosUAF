using FormulariosUAF.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FormulariosUAF.Pages.Api;

[Authorize]
public class BuscarEmpresaPorRutModel : PageModel
{
    private readonly IEmpresaProduccionService _service;

    public BuscarEmpresaPorRutModel(IEmpresaProduccionService service)
    {
        _service = service;
    }

    public async Task<IActionResult> OnGetAsync([FromQuery] string rut)
    {
        if (string.IsNullOrWhiteSpace(rut))
            return BadRequest(new { error = "RUT requerido" });

        var data = await _service.BuscarPorRutAsync(rut.Trim());
        if (data is null)
            return NotFound(new { error = "No se encontraron datos para el RUT indicado" });

        return new JsonResult(new
        {
            rutEmpresa = data.RutEmpresa,
            razonSocial = data.RazonSocial,
            domicilio = data.Domicilio,
            ciudad = data.Ciudad,
            paisConstitucion = data.PaisConstitucion,
            telefono = data.Telefono,
            tipoEntidad = data.TipoEntidad,
            rutRepresentanteLegal = data.RutRepresentanteLegal,
            nombreRepresentanteLegal = data.NombreRepresentanteLegal,
            email = data.Email
        });
    }
}
