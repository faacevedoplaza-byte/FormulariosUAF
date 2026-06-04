namespace FormulariosUAF.Services;

public interface IFileStorageService
{
    Task<StoredFileResult> SaveFileAsync(IFormFile file, string rutCliente, string solicitudId, string tipoDocumento);
    Task<Stream> GetFileAsync(string storagePath);
    Task DeleteFileAsync(string storagePath);
    bool FileExists(string storagePath);
}

public record StoredFileResult(
    string StoredFileName,
    string StoragePath,
    string StorageKey,
    long FileSizeBytes
);
