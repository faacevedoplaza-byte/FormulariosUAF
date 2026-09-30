using FormulariosUAF.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FormulariosUAF.Pages.Api;

[Authorize(Roles = "Administrador")]
public class BuscarUsuarioNetcarPorRutModel : PageModel
{
    private readonly INetcarUsuarioService _service;

    public BuscarUsuarioNetcarPorRutModel(INetcarUsuarioService service)
    {
        _service = service;
    }

    public async Task<IActionResult> OnGetAsync([FromQuery] string rut)
    {
        if (string.IsNullOrWhiteSpace(rut))
            return BadRequest(new { error = "RUT requerido" });

        var data = await _service.BuscarPorRutAsync(rut.Trim());
        if (data is null)
            return NotFound(new { error = "El usuario no existe o no se encuentra en Netcar" });

        return new JsonResult(new
        {
            rut = data.Rut,
            nombreCompleto = data.NombreCompleto,
            email = data.Email,
            grupo = data.Grupo,
            vigente = data.Vigente
        });
    }
}
