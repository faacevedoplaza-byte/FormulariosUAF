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

    public async Task<Request> CreateRequestAsync(NewRequestData data, string vendorUserId)
    {
        var client = await _db.Clients.FirstOrDefaultAsync(c => c.RUT == data.RutCliente);
        if (client is null)
        {
            client = new Client { RUT = data.RutCliente, BusinessName = data.RazonSocial, CreatedBy = vendorUserId };
            _db.Clients.Add(client);
            await _db.SaveChangesAsync();
        }

        var request = new Request
        {
            ClientId = client.Id,
            VendorUserId = vendorUserId,
            RequestType = data.TipoSolicitud,
            Status = RequestStatus.Borrador,
            ClientToken = _tokenService.GenerateClientToken(),
            TokenExpiry = DateTime.UtcNow.AddDays(data.DiasVigencia),
            InternalNotes = data.Observaciones,
            ClientEmail = data.ClientEmail,
            ClientPhone = data.ClientPhone,
            NetcarBusinessNumber = data.NetcarBusinessNumber,
            CreatedBy = vendorUserId,
            DueDate = DateTime.UtcNow.AddDays(data.DiasVigencia)
        };

        request.RequestNumber = await GenerateRequestNumberAsync();
        _db.Requests.Add(request);
        await _db.SaveChangesAsync();

        _db.LegalEntityDeclarations.Add(new LegalEntityDeclaration
        {
            RequestId = request.Id,
            RUT = data.RutCliente,
            BusinessName = data.RazonSocial,
            Address = data.Address,
            City = data.City,
            CountryOfIncorporation = data.CountryOfIncorporation,
            Phone = data.Phone,
            EntityType = data.EntityType,
            EntityTypeOther = data.EntityTypeOther,
            LegalRepresentativeIdNumber = data.LegalRepresentativeIdNumber,
            LegalRepresentativeName = data.LegalRepresentativeName
        });
        await _db.SaveChangesAsync();

        return request;
    }

    public async Task<Request?> GetByTokenAsync(string token)
    {
        return await _db.Requests
            .Include(r => r.Client)
            .Include(r => r.LegalEntityDeclaration)
            .Include(r => r.DeclaredPersons.OrderBy(d => d.SortOrder))
            .Include(r => r.BeneficialOwners)
            .Include(r => r.EffectiveControllers)
            .Include(r => r.PepDeclaration)
            .Include(r => r.Declarant)
            .Include(r => r.Documents.Where(d => d.IsActive))
            .Include(r => r.TaxFolderAnalysis!).ThenInclude(t => t.Alerts)
            .FirstOrDefaultAsync(r => r.ClientToken == token && !r.IsDeleted);
    }

    public async Task<Request?> GetByIdAsync(Guid id)
    {
        return await _db.Requests
            .Include(r => r.Client)
            .Include(r => r.VendorUser)
            .Include(r => r.LegalEntityDeclaration)
            .Include(r => r.DeclaredPersons.OrderBy(d => d.SortOrder))
            .Include(r => r.BeneficialOwners)
            .Include(r => r.EffectiveControllers)
            .Include(r => r.PepDeclaration)
            .Include(r => r.Declarant)
            .Include(r => r.Documents.Where(d => d.IsActive))
            .Include(r => r.TaxFolderAnalysis!).ThenInclude(t => t.Alerts)
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
        1 => "/Cliente/Adjuntos",
        2 => "/Cliente/Envio",
        3 => "/Cliente/Completado",
        _ => "/Cliente/Declaracion"
    };

    private async Task<string> GenerateRequestNumberAsync()
    {
        var year = DateTime.Now.Year;
        var count = await _db.Requests.CountAsync() + 1;
        return $"UAF-{year}-{count:D5}";
    }
}
