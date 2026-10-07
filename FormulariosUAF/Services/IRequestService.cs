using FormulariosUAF.Models.Domain;
using FormulariosUAF.Models.Enums;

namespace FormulariosUAF.Services;

public record NewRequestData(
    string RutCliente,
    string RazonSocial,
    RequestType TipoSolicitud,
    int DiasVigencia,
    string? Observaciones,
    string Address,
    string City,
    string CountryOfIncorporation,
    string? Phone,
    EntityType EntityType,
    string? EntityTypeOther,
    string LegalRepresentativeIdNumber,
    string LegalRepresentativeName,
    string? ClientEmail = null,
    string? ClientPhone = null,
    string? NetcarBusinessNumber = null
);

/// <summary>Solicitud ya registrada para un RUT (para /Vendedor/Crear).</summary>
public record SolicitudPrevia(Guid Id, string RequestNumber, RequestType Tipo, RequestStatus Estado, DateTime CreatedAtUtc);

public interface IRequestService
{
    Task<Request> CreateRequestAsync(NewRequestData data, string vendorUserId);
    /// <summary>Solicitudes no eliminadas de la empresa con ese RUT (con o sin puntos/guión), más recientes primero.</summary>
    Task<List<SolicitudPrevia>> GetSolicitudesPreviasAsync(string? rut);
    Task<Request?> GetByTokenAsync(string token);
    Task<Request?> GetByIdAsync(Guid id);
    Task UpdateStatusAsync(Guid requestId, RequestStatus newStatus, string changedBy, string? notes = null);
    Task<string> GenerateRequestLinkAsync(Guid requestId, string baseUrl);
    Task<bool> UpdateStepAsync(Guid requestId, int step);
    string GetNextStepUrl(int currentStep, Guid requestId);
}
