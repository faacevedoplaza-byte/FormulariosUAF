using Microsoft.Extensions.Options;

namespace FormulariosUAF.Services;

public class FileStorageOptions
{
    public string LocalPath { get; set; } = "Repository";
    public int MaxFileSizeMB { get; set; } = 20;
    public string[] AllowedExtensions { get; set; } = [".pdf", ".jpg", ".jpeg", ".png", ".docx", ".xlsx"];
}

public class LocalFileStorageService : IFileStorageService
{
    private readonly string _basePath;
    private readonly IWebHostEnvironment _env;
    private readonly ILogger<LocalFileStorageService> _logger;

    public LocalFileStorageService(IWebHostEnvironment env, IConfiguration config, ILogger<LocalFileStorageService> logger)
    {
        _env = env;
        _logger = logger;
        var relativePath = config["FileStorage:LocalPath"] ?? "Repository";
        _basePath = Path.Combine(_env.ContentRootPath, relativePath);
        Directory.CreateDirectory(_basePath);
    }

    public async Task<StoredFileResult> SaveFileAsync(IFormFile file, string rutCliente, string solicitudId, string tipoDocumento)
    {
        var ext = Path.GetExtension(file.FileName).ToLowerInvariant();
        var fecha = DateTime.Now.ToString("yyyyMMdd_HHmmss");
        var storedFileName = $"{rutCliente}_{solicitudId}_{tipoDocumento}_{fecha}{ext}";

        var clientDir = Path.Combine(_basePath, "Clientes", rutCliente, solicitudId);
        Directory.CreateDirectory(clientDir);

        var fullPath = Path.Combine(clientDir, storedFileName);
        await using var stream = new FileStream(fullPath, FileMode.Create);
        await file.CopyToAsync(stream);

        var relativePath = Path.Combine("Clientes", rutCliente, solicitudId, storedFileName);
        _logger.LogInformation("Archivo guardado: {Path}", relativePath);

        return new StoredFileResult(storedFileName, relativePath, relativePath, file.Length);
    }

    public Task<Stream> GetFileAsync(string storagePath)
    {
        var fullPath = Path.Combine(_basePath, storagePath);
        if (!File.Exists(fullPath))
            throw new FileNotFoundException("Archivo no encontrado", storagePath);

        Stream stream = new FileStream(fullPath, FileMode.Open, FileAccess.Read);
        return Task.FromResult(stream);
    }

    public Task DeleteFileAsync(string storagePath)
    {
        var fullPath = Path.Combine(_basePath, storagePath);
        if (File.Exists(fullPath))
            File.Delete(fullPath);
        return Task.CompletedTask;
    }

    public bool FileExists(string storagePath)
    {
        return File.Exists(Path.Combine(_basePath, storagePath));
    }
}
