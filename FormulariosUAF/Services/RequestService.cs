using FormulariosUAF.Data;
using FormulariosUAF.Models.Domain;
using FormulariosUAF.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace FormulariosUAF.Services;

public class RequestService : IRequestService
{
    private readonly ApplicationDbContext _db;
    private readonly ITokenService _tokenService;
    private readonly IConfiguration _config;

    public RequestService(ApplicationDbContext db, ITokenService tokenService, IConfiguration config)
    {
        _db = db;
        _tokenService = tokenService;
        _config = config;
    }

    public async Task<Request> CreateRequestAsync(string rutCliente, string razonSocial, RequestType tipo, string vendorUserId)
    {
        var client = await _db.Clients.FirstOrDefaultAsync(c => c.RUT == rutCliente);
        if (client is null)
        {
            client = new Client { RUT = rutCliente, BusinessName = razonSocial, CreatedBy = vendorUserId };
            _db.Clients.Add(client);
            await _db.SaveChangesAsync();
        }

        var expirationDays = _config.GetValue<int>("TokenSettings:ExpirationDays", 30);
        var request = new Request
        {
            ClientId = client.Id,
            VendorUserId = vendorUserId,
            RequestType = tipo,
            Status = RequestStatus.Borrador,
            ClientToken = _tokenService.GenerateClientToken(),
            TokenExpiry = DateTime.UtcNow.AddDays(expirationDays),
            CreatedBy = vendorUserId,
            DueDate = DateTime.UtcNow.AddDays(expirationDays)
        };

        request.RequestNumber = await GenerateRequestNumberAsync();
        _db.Requests.Add(request);
        await _db.SaveChangesAsync();
        return request;
    }

    public async Task<Request?> GetByTokenAsync(string token)
    {
        return await _db.Requests
            .Include(r => r.Client)
            .Include(r => r.LegalEntityDeclaration)
            .Include(r => r.BeneficialOwners)
            .Include(r => r.EffectiveControllers)
            .Include(r => r.PepDeclaration)
            .Include(r => r.Declarant)
            .Include(r => r.Documents.Where(d => d.IsActive))
            .FirstOrDefaultAsync(r => r.ClientToken == token && !r.IsDeleted);
    }

    public async Task<Request?> GetByIdAsync(Guid id)
    {
        return await _db.Requests
            .Include(r => r.Client)
            .Include(r => r.VendorUser)
            .Include(r => r.LegalEntityDeclaration)
            .Include(r => r.BeneficialOwners)
            .Include(r => r.EffectiveControllers)
            .Include(r => r.PepDeclaration)
            .Include(r => r.Declarant)
            .Include(r => r.Documents.Where(d => d.IsActive))
            .Include(r => r.StatusHistory.OrderByDescending(h => h.ChangedAt))
            .FirstOrDefaultAsync(r => r.Id == id);
    }

    public async Task UpdateStatusAsync(Guid requestId, RequestStatus newStatus, string changedBy, string? notes = null)
    {
        var request = await _db.Requests.FindAsync(requestId) ?? throw new InvalidOperationException("Solicitud no encontrada");
        var oldStatus = request.Status;
        request.Status = newStatus;
        request.UpdatedAt = DateTime.UtcNow;
        request.UpdatedBy = changedBy;

        if (newStatus == RequestStatus.CompletadaPorCliente)
            request.CompletedAt = DateTime.UtcNow;

        _db.RequestStatusHistories.Add(new RequestStatusHistory
        {
            RequestId = requestId,
            OldStatus = oldStatus,
            NewStatus = newStatus,
            ChangedBy = changedBy,
            Notes = notes
        });

        await _db.SaveChangesAsync();
    }

    public Task<string> GenerateRequestLinkAsync(Guid requestId, string baseUrl)
    {
        // Implemented in page directly; keeping interface for future email service
        return Task.FromResult(string.Empty);
    }

    public async Task<bool> UpdateStepAsync(Guid requestId, int step)
    {
        var request = await _db.Requests.FindAsync(requestId);
        if (request is null) return false;
        if (step > request.CurrentStep)
        {
            request.CurrentStep = step;
            request.UpdatedAt = DateTime.UtcNow;
            await _db.SaveChangesAsync();
        }
        return true;
    }

    public string GetNextStepUrl(int currentStep, Guid requestId) => currentStep switch
    {
        1 => "/Cliente/Paso2",
        2 => "/Cliente/Paso3",
        3 => "/Cliente/Paso4",
        4 => "/Cliente/Paso5",
        5 => "/Cliente/Paso6",
        6 => "/Cliente/Paso7",
        7 => "/Cliente/Completado",
        _ => "/Cliente/Paso1"
    };

    private async Task<string> GenerateRequestNumberAsync()
    {
        var year = DateTime.Now.Year;
        var count = await _db.Requests.CountAsync() + 1;
        return $"UAF-{year}-{count:D5}";
    }
}
