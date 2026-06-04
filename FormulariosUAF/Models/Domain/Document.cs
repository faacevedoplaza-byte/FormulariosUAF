using FormulariosUAF.Models.Enums;

namespace FormulariosUAF.Models.Domain;

public class Document
{
    public int Id { get; set; }
    public Guid RequestId { get; set; }
    public Request Request { get; set; } = null!;

    public DocumentType DocumentType { get; set; }
    public string? DocumentTypeOther { get; set; }

    public string OriginalFileName { get; set; } = string.Empty;
    public string StoredFileName { get; set; } = string.Empty;
    public string StoragePath { get; set; } = string.Empty;
    public string MimeType { get; set; } = string.Empty;
    public long FileSizeBytes { get; set; }

    public bool IsActive { get; set; } = true;
    public int Version { get; set; } = 1;

    public StorageProvider StorageProvider { get; set; } = StorageProvider.Local;

    public DateTime UploadedAt { get; set; } = DateTime.UtcNow;
    public string UploadedByIp { get; set; } = string.Empty;

    public ICollection<FileStorageRecord> StorageRecords { get; set; } = new List<FileStorageRecord>();
}
