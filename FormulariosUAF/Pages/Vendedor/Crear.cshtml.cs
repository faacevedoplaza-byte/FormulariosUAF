using System.ComponentModel.DataAnnotations;
using FormulariosUAF.Extensions;
using FormulariosUAF.Helpers;
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

        [MaxLength(100)]
        public string? NetcarBusinessNumber { get; set; }

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

    // GET /Vendedor/Crear?handler=SolicitudesPrevias&rut=... → solicitudes ya registradas para esa empresa.
    public async Task<IActionResult> OnGetSolicitudesPreviasAsync(string? rut)
    {
        var previas = await _requestService.GetSolicitudesPreviasAsync(rut);
        return new JsonResult(new
        {
            total = previas.Count,
            tiposPermitidos = Enum.GetValues<RequestType>()
                .Where(t => previas.Count == 0 || t.PermitidoConSolicitudPrevia())
                .Select(t => (int)t),
            solicitudes = previas.Take(5).Select(p => new
            {
                numero = p.RequestNumber,
                fecha = p.CreatedAtUtc.ToLocalTime().ToString("dd/MM/yyyy"),
                tipo = p.Tipo.ToDisplayString(),
                estado = p.Estado.ToDisplayString(),
                badge = p.Estado.ToBadgeClass()
            })
        });
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid) return Page();

        // Regla: si la empresa ya tiene una solicitud, solo se permite una actualización.
        // (La vista ya bloquea las otras opciones; esto evita saltársela.)
        if (!Input.TipoSolicitud.PermitidoConSolicitudPrevia())
        {
            var previas = await _requestService.GetSolicitudesPreviasAsync(Input.RutCliente);
            if (previas.Count > 0)
            {
                ModelState.AddModelError("Input.TipoSolicitud",
                    $"Esta empresa ya tiene {previas.Count} solicitud(es) (la última es {previas[0].RequestNumber}). " +
                    $"Solo puede crear una {RequestType.ActualizacionDatos.ToDisplayString()} o una {RequestType.ActualizacionSinCambios.ToDisplayString()}.");
                return Page();
            }
        }

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
                Input.ClientPhone,
                Input.NetcarBusinessNumber
            );

            var request = await _requestService.CreateRequestAsync(data, userId);

            // Enviar el enlace al correo del cliente y marcar como "Enviada al cliente" de inmediato.
            var vendorName = User.DisplayName() ?? "Ejecutivo";
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
                    VendorEmail: User.CorreoUsuario(),
                    SecureLink: link,
                    ExpirationDate: request.TokenExpiry,
                    RequestNumber: request.RequestNumber,
                    SupportContact: _config["AppSettings:SupportEmail"]
                ));

                await _requestService.UpdateStatusAsync(request.Id, RequestStatus.EnviadaAlCliente, vendorName,
                    $"Enlace enviado automáticamente al correo {Input.ClientEmail} al crear la solicitud.");
                await _audit.LogAsync("ENVIAR_EMAIL_CLIENTE", "Request", request.Id.ToString(),
                    newValues: new { Input.ClientEmail }, requestId: request.Id, userName: User.Identity?.Name);

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
