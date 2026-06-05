using FormulariosUAF.Models.Domain;
using FormulariosUAF.Models.Enums;

namespace FormulariosUAF.Services;

/// <summary>
/// Abstracción para firma electrónica.
/// Implementación actual: firma simple (nombre + RUT + IP + hash).
/// Implementación futura: proveedor FEA externo (Acepta, Signer, etc.).
/// </summary>
public interface IElectronicSignatureService
{
    Task<SignatureRecord> RegisterSimpleSignatureAsync(SimpleSignatureRequest request);
    Task<SignatureRecord?> GetByRequestIdAsync(Guid requestId);
    SignatureType CurrentSignatureType { get; }
}

public record SimpleSignatureRequest(
    Guid RequestId,
    string SignerName,
    string SignerIdNumber,
    string FolioNumber,
    string? IpAddress,
    string? UserAgent,
    byte[]? DocumentBytes = null
);
