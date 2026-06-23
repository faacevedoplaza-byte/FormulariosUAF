using System.ComponentModel.DataAnnotations;
using FormulariosUAF.Models.Enums;
using FormulariosUAF.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FormulariosUAF.Pages.Vendedor;

public class CrearModel : PageModel
{
    private readonly IRequestService _requestService;
    private readonly IEmailService _email;
    private readonly IAuditService _audit;
    private readonly IConfiguration _config;

    public CrearModel(IRequestService requestService, IEmailService email, IAuditService audit, IConfiguration config)
    {
        _requestService = requestService;
        _email = email;
        _audit = audit;
        _config = config;
    }

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
        [Required(ErrorMessage = "El correo del cliente es obligatorio (se usa para enviar el enlace)")]
        [EmailAddress(ErrorMessage = "El correo del cliente no es válido")]
        [MaxLength(200)]
        public string ClientEmail { get; set; } = string.Empty;

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

            // Enviar el enlace al correo del cliente y marcar como "Enviada al cliente" de inmediato.
            var vendorName = User.Identity?.Name ?? "Ejecutivo";
            var configuredBase = _config["AppSettings:BaseUrl"];
            var baseUrl = (!string.IsNullOrWhiteSpace(configuredBase) && !configuredBase.Contains("localhost", StringComparison.OrdinalIgnoreCase))
                ? configuredBase.TrimEnd('/')
                : $"{Request.Scheme}://{Request.Host}";
            var link = $"{baseUrl}/Cliente/Inicio?token={request.ClientToken}";
            try
            {
                await _email.SendClientInvitationAsync(new ClientInvitationEmail(
                    ToEmail: Input.ClientEmail,
                    ClientName: Input.RazonSocial,
                    VendorName: vendorName,
                    VendorEmail: vendorName,
                    SecureLink: link,
                    ExpirationDate: request.TokenExpiry,
                    RequestNumber: request.RequestNumber,
                    SupportContact: _config["AppSettings:SupportEmail"]
                ));

                await _requestService.UpdateStatusAsync(request.Id, RequestStatus.EnviadaAlCliente, vendorName,
                    $"Enlace enviado automáticamente al correo {Input.ClientEmail} al crear la solicitud.");
                await _audit.LogAsync("ENVIAR_EMAIL_CLIENTE", "Request", request.Id.ToString(),
                    newValues: new { Input.ClientEmail }, requestId: request.Id, userName: vendorName);

                TempData["Success"] = $"Solicitud {request.RequestNumber} creada y enviada a {Input.ClientEmail}.";
            }
            catch (Exception emailEx)
            {
                TempData["Error"] = $"Solicitud {request.RequestNumber} creada, pero no se pudo enviar el correo: {emailEx.Message}. Puede reenviarlo desde el detalle.";
            }

            return RedirectToPage("/Vendedor/Detalle", new { id = request.Id });
        }
        catch (Exception ex)
        {
            ErrorMessage = "Error al crear la solicitud: " + ex.Message;
            return Page();
        }
    }
}
