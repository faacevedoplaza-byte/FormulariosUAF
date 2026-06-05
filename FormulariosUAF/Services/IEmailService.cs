namespace FormulariosUAF.Services;

public interface IEmailService
{
    Task SendClientInvitationAsync(ClientInvitationEmail email);
    Task SendStatusNotificationAsync(string toEmail, string toName, string subject, string htmlBody);
    Task<bool> IsConfiguredAsync();
}

public record ClientInvitationEmail(
    string ToEmail,
    string ClientName,
    string VendorName,
    string VendorEmail,
    string SecureLink,
    DateTime ExpirationDate,
    string RequestNumber,
    string? SupportContact = null
);
