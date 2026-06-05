using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

namespace FormulariosUAF.Services;

/// <summary>
/// Implementación de IFileStorageService usando Azure Blob Storage.
/// Activar configurando FileStorage:Provider = "AzureBlob" en appsettings.json.
/// Requiere FileStorage:Azure:ConnectionString y FileStorage:Azure:ContainerName.
/// </summary>
public class AzureBlobStorageService : IFileStorageService
{
    private readonly BlobContainerClient _container;
    private readonly string _prefix;
    private readonly ILogger<AzureBlobStorageService> _logger;
    private readonly IConfiguration _config;

    public AzureBlobStorageService(IConfiguration config, ILogger<AzureBlobStorageService> logger)
    {
        _config = config;
        _logger = logger;

        var connStr = config["FileStorage:Azure:ConnectionString"]
            ?? throw new InvalidOperationException("FileStorage:Azure:ConnectionString no configurado.");
        var container = config["FileStorage:Azure:ContainerName"] ?? "uaf-documentos";
        _prefix = config["FileStorage:Azure:Prefix"] ?? config["ASPNETCORE_ENVIRONMENT"] ?? "prod";

        _container = new BlobContainerClient(connStr, container);
        _container.CreateIfNotExists(PublicAccessType.None);
    }

    public async Task<StoredFileResult> SaveFileAsync(IFormFile file, string rutCliente, string solicitudId, string tipoDocumento)
    {
        var ext = Path.GetExtension(file.FileName).ToLowerInvariant();
        var fecha = DateTime.Now.ToString("yyyyMMdd_HHmmss");
        var storedFileName = $"{rutCliente}_{solicitudId}_{tipoDocumento}_{fecha}{ext}";
        var blobPath = $"{_prefix}/Clientes/{rutCliente}/{solicitudId}/{storedFileName}";

        var blobClient = _container.GetBlobClient(blobPath);

        await using var stream = file.OpenReadStream();
        await blobClient.UploadAsync(stream, new BlobHttpHeaders { ContentType = file.ContentType });

        _logger.LogInformation("Archivo subido a Azure Blob: {Path}", blobPath);

        return new StoredFileResult(storedFileName, blobPath, blobPath, file.Length);
    }

    public async Task<Stream> GetFileAsync(string storagePath)
    {
        var blobClient = _container.GetBlobClient(storagePath);
        var response = await blobClient.DownloadStreamingAsync();
        return response.Value.Content;
    }

    public async Task DeleteFileAsync(string storagePath)
    {
        var blobClient = _container.GetBlobClient(storagePath);
        await blobClient.DeleteIfExistsAsync();
    }

    public bool FileExists(string storagePath)
    {
        var blobClient = _container.GetBlobClient(storagePath);
        return blobClient.Exists();
    }
}
