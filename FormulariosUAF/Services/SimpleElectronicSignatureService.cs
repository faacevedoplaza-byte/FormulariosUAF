using System.Security.Cryptography;
using System.Text;
using FormulariosUAF.Data;
using FormulariosUAF.Models.Domain;
using FormulariosUAF.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace FormulariosUAF.Services;

public class SimpleElectronicSignatureService : IElectronicSignatureService
{
    private readonly ApplicationDbContext _db;

    public SimpleElectronicSignatureService(ApplicationDbContext db) => _db = db;

    public SignatureType CurrentSignatureType => SignatureType.Simple;

    public async Task<SignatureRecord> RegisterSimpleSignatureAsync(SimpleSignatureRequest request)
    {
        var hash = request.DocumentBytes is not null
            ? Convert.ToHexString(SHA256.HashData(request.DocumentBytes))
            : ComputeTextHash($"{request.SignerName}|{request.SignerIdNumber}|{request.FolioNumber}|{DateTime.UtcNow:o}");

        var existing = await _db.SignatureRecords
            .FirstOrDefaultAsync(s => s.RequestId == request.RequestId);

        if (existing is not null)
        {
            existing.SignerName = request.SignerName;
            existing.SignerIdNumber = request.SignerIdNumber;
            existing.SignedAt = DateTime.UtcNow;
            existing.IpAddress = request.IpAddress;
            existing.UserAgent = request.UserAgent;
            existing.DocumentHash = hash;
            existing.Status = SignatureStatus.Firmada;
            existing.UpdatedAt = DateTime.UtcNow;
            await _db.SaveChangesAsync();
            return existing;
        }

        var record = new SignatureRecord
        {
            RequestId = request.RequestId,
            Type = SignatureType.Simple,
            Status = SignatureStatus.Firmada,
            SignerName = request.SignerName,
            SignerIdNumber = request.SignerIdNumber,
            FolioNumber = request.FolioNumber,
            SignedAt = DateTime.UtcNow,
            IpAddress = request.IpAddress,
            UserAgent = request.UserAgent,
            DocumentHash = hash
        };

        _db.SignatureRecords.Add(record);
        await _db.SaveChangesAsync();
        return record;
    }

    public Task<SignatureRecord?> GetByRequestIdAsync(Guid requestId)
        => _db.SignatureRecords.FirstOrDefaultAsync(s => s.RequestId == requestId);

    private static string ComputeTextHash(string input)
        => Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes(input)));
}
