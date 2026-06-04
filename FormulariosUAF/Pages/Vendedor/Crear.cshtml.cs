using System.ComponentModel.DataAnnotations;
using FormulariosUAF.Models.Enums;
using FormulariosUAF.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FormulariosUAF.Pages.Vendedor;

public class CrearModel : PageModel
{
    private readonly IRequestService _requestService;

    public CrearModel(IRequestService requestService) => _requestService = requestService;

    [BindProperty]
    public InputModel Input { get; set; } = new();
    public string? ErrorMessage { get; set; }

    public class InputModel
    {
        [Required(ErrorMessage = "El RUT es obligatorio")]
        [MaxLength(20)]
        public string RutCliente { get; set; } = string.Empty;

        [Required(ErrorMessage = "La razón social es obligatoria")]
        [MaxLength(500)]
        public string RazonSocial { get; set; } = string.Empty;

        [Required]
        public RequestType TipoSolicitud { get; set; } = RequestType.ClienteNuevo;

        [Range(1, 90)]
        public int DiasVigencia { get; set; } = 30;

        [MaxLength(2000)]
        public string? Observaciones { get; set; }
    }

    public void OnGet() { }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid) return Page();

        var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
        if (userId is null) return Challenge();

        try
        {
            var request = await _requestService.CreateRequestAsync(
                Input.RutCliente, Input.RazonSocial, Input.TipoSolicitud, userId);

            if (!string.IsNullOrEmpty(Input.Observaciones))
            {
                // Update notes via DbContext directly
                // (simplified — could be a separate service method)
            }

            TempData["Success"] = $"Solicitud {request.RequestNumber} creada correctamente.";
            return RedirectToPage("/Vendedor/Detalle", new { id = request.Id });
        }
        catch (Exception ex)
        {
            ErrorMessage = "Error al crear la solicitud: " + ex.Message;
            return Page();
        }
    }
}
