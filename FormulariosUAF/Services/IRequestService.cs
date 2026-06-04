using FormulariosUAF.Models.Domain;
using FormulariosUAF.Models.Enums;

namespace FormulariosUAF.Services;

public interface IRequestService
{
    Task<Request> CreateRequestAsync(string rutCliente, string razonSocial, RequestType tipo, string vendorUserId);
    Task<Request?> GetByTokenAsync(string token);
    Task<Request?> GetByIdAsync(Guid id);
    Task UpdateStatusAsync(Guid requestId, RequestStatus newStatus, string changedBy, string? notes = null);
    Task<string> GenerateRequestLinkAsync(Guid requestId, string baseUrl);
    Task<bool> UpdateStepAsync(Guid requestId, int step);
    string GetNextStepUrl(int currentStep, Guid requestId);
}
