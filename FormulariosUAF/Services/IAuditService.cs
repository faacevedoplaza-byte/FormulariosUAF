namespace FormulariosUAF.Services;

public interface IAuditService
{
    Task LogAsync(string action, string entityType, string? entityId = null,
        object? oldValues = null, object? newValues = null,
        Guid? requestId = null, string? userId = null, string? userName = null);
}
