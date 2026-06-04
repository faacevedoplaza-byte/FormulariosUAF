using FormulariosUAF.Models.Enums;

namespace FormulariosUAF.Models.Domain;

public class FileStorageRecord
{
    public int Id { get; set; }
    public int DocumentId { get; set; }
    public Document Document { get; set; } = null!;

    public StorageProvider StorageProvider { get; set; }
    public string StoragePath { get; set; } = string.Empty;
    public string StorageKey { get; set; } = string.Empty;
    public DateTime UploadedAt { get; set; } = DateTime.UtcNow;
    public string? Metadata { get; set; }
}
