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
        // Identificación empresa
        [Required(ErrorMessage = "El RUT es obligatorio")]
        [MaxLength(20)]
        public string RutCliente { get; set; } = string.Empty;

        [Required(ErrorMessage = "La razón social es obligatoria")]
        [MaxLength(500)]
        public string RazonSocial { get; set; } = string.Empty;

        [Required(ErrorMessage = "El domicilio es obligatorio")]
        [MaxLength(500)]
        public string Address { get; set; } = string.Empty;

        [Required(ErrorMessage = "La ciudad es obligatoria")]
        [MaxLength(200)]
        public string City { get; set; } = string.Empty;

        [Required]
        [MaxLength(200)]
        public string CountryOfIncorporation { get; set; } = "Chile";

        [MaxLength(50)]
        public string? Phone { get; set; }

        [Required]
        public EntityType EntityType { get; set; }

        [MaxLength(200)]
        public string? EntityTypeOther { get; set; }

        // Representante legal
        [Required(ErrorMessage = "El RUT del representante es obligatorio")]
        [MaxLength(50)]
        public string LegalRepresentativeIdNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "El nombre del representante es obligatorio")]
        [MaxLength(500)]
        public string LegalRepresentativeName { get; set; } = string.Empty;

        // Contacto cliente
        [EmailAddress]
        [MaxLength(200)]
        public string? ClientEmail { get; set; }

        [MaxLength(50)]
        public string? ClientPhone { get; set; }

        // Solicitud
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
            var data = new NewRequestData(
                Input.RutCliente,
                Input.RazonSocial,
                Input.TipoSolicitud,
                Input.DiasVigencia,
                Input.Observaciones,
                Input.Address,
                Input.City,
                Input.CountryOfIncorporation,
                Input.Phone,
                Input.EntityType,
                Input.EntityTypeOther,
                Input.LegalRepresentativeIdNumber,
                Input.LegalRepresentativeName,
                Input.ClientEmail,
                Input.ClientPhone
            );

            var request = await _requestService.CreateRequestAsync(data, userId);

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
