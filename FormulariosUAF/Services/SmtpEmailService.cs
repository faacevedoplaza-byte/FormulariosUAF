using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace FormulariosUAF.Services;

public class SmtpOptions
{
    public string Host { get; set; } = string.Empty;
    public int Port { get; set; } = 587;
    public bool UseSsl { get; set; } = true;
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string SenderEmail { get; set; } = string.Empty;
    public string SenderName { get; set; } = "FormulariosUAF";
}

public class SmtpEmailService : IEmailService
{
    private readonly SmtpOptions _options;
    private readonly ILogger<SmtpEmailService> _logger;
    private readonly IConfiguration _config;

    public SmtpEmailService(IConfiguration config, ILogger<SmtpEmailService> logger)
    {
        _config = config;
        _logger = logger;
        _options = config.GetSection("Email:Smtp").Get<SmtpOptions>() ?? new SmtpOptions();
    }

    public Task<bool> IsConfiguredAsync()
    {
        var configured = !string.IsNullOrEmpty(_options.Host) && !string.IsNullOrEmpty(_options.SenderEmail);
        return Task.FromResult(configured);
    }

    public async Task SendClientInvitationAsync(ClientInvitationEmail email)
    {
        var companyName = _config["AppSettings:CompanyName"] ?? "Mi Empresa";
        var html = BuildInvitationHtml(email, companyName);

        await SendAsync(email.ToEmail, email.ClientName,
            $"Formulario de Declaración — {companyName}", html);
    }

    public async Task SendStatusNotificationAsync(string toEmail, string toName, string subject, string htmlBody)
    {
        await SendAsync(toEmail, toName, subject, htmlBody);
    }

    private async Task SendAsync(string toEmail, string toName, string subject, string htmlBody)
    {
        if (string.IsNullOrEmpty(_options.Host))
        {
            _logger.LogWarning("Servicio de email no configurado. Email no enviado a {Email}", toEmail);
            return;
        }

        try
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(_options.SenderName, _options.SenderEmail));
            message.To.Add(new MailboxAddress(toName, toEmail));
            message.Subject = subject;
            message.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = htmlBody };

            using var client = new SmtpClient();
            // Puerto 465 = SSL implícito (SslOnConnect); 587 = STARTTLS; sin SSL = None.
            var secureOption = _options.Port == 465
                ? SecureSocketOptions.SslOnConnect
                : (_options.UseSsl ? SecureSocketOptions.StartTls : SecureSocketOptions.None);
            await client.ConnectAsync(_options.Host, _options.Port, secureOption);

            if (!string.IsNullOrEmpty(_options.Username))
                await client.AuthenticateAsync(_options.Username, _options.Password);

            await client.SendAsync(message);
            await client.DisconnectAsync(true);

            _logger.LogInformation("Email enviado a {Email}: {Subject}", toEmail, subject);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error enviando email a {Email}", toEmail);
            throw;
        }
    }

    private static string BuildInvitationHtml(ClientInvitationEmail email, string companyName) => $"""
        <!DOCTYPE html>
        <html lang="es">
        <head><meta charset="utf-8"><meta name="viewport" content="width=device-width,initial-scale=1"></head>
        <body style="font-family:Segoe UI,Arial,sans-serif;background:#f4f6f8;margin:0;padding:0">
          <table width="100%" cellpadding="0" cellspacing="0" style="background:#f4f6f8;padding:30px 0">
            <tr><td align="center">
              <table width="600" cellpadding="0" cellspacing="0" style="background:#fff;border-radius:12px;overflow:hidden;box-shadow:0 4px 16px rgba(0,0,0,.08)">
                <!-- Header -->
                <tr><td style="background:#1a3a5c;padding:28px 40px;text-align:center">
                  <h1 style="color:#fff;margin:0;font-size:22px;font-weight:700">&#x1F6E1;&#xFE0F; {companyName}</h1>
                  <p style="color:#aac4e0;margin:6px 0 0;font-size:14px">Declaración Digital Segura</p>
                </td></tr>

                <!-- Body -->
                <tr><td style="padding:36px 40px">
                  <p style="color:#333;font-size:16px;margin:0 0 16px">Estimado/a <strong>{email.ClientName}</strong>,</p>
                  <p style="color:#555;font-size:14px;line-height:1.6;margin:0 0 20px">
                    Le informamos que <strong>{email.VendorName}</strong> ha generado una solicitud de declaración
                    para su empresa con folio <strong>{email.RequestNumber}</strong>.
                    Para completarla, haga clic en el botón a continuación:
                  </p>

                  <div style="text-align:center;margin:30px 0">
                    <a href="{email.SecureLink}"
                       style="background:#1a3a5c;color:#fff;text-decoration:none;padding:14px 36px;border-radius:8px;font-size:16px;font-weight:600;display:inline-block">
                      Completar Declaración
                    </a>
                  </div>

                  <div style="background:#fff8e1;border:1px solid #ffe082;border-radius:8px;padding:16px;margin:20px 0">
                    <p style="margin:0;font-size:13px;color:#7a5c00">
                      &#x26A0;&#xFE0F; <strong>Importante:</strong>
                      Este enlace vence el <strong>{email.ExpirationDate:dd/MM/yyyy} a las {email.ExpirationDate:HH:mm}</strong>.
                      Si necesita un nuevo enlace, contacte a su ejecutivo.
                    </p>
                  </div>

                  <p style="color:#555;font-size:13px;line-height:1.6">
                    <strong>Instrucciones:</strong>
                  </p>
                  <ol style="color:#555;font-size:13px;line-height:1.8;padding-left:20px">
                    <li>Haga clic en el botón "Completar Declaración".</li>
                    <li>Complete los datos de su empresa, beneficiarios y representante legal.</li>
                    <li>Adjunte la carpeta tributaria y los documentos solicitados.</li>
                    <li>Revise la información y envíe la declaración.</li>
                  </ol>

                  {(string.IsNullOrEmpty(email.SupportContact) ? "" : $"""
                  <p style="color:#555;font-size:13px;margin-top:20px">
                    Si tiene dudas, puede contactar a su ejecutivo: <a href="mailto:{email.SupportContact}" style="color:#1a3a5c">{email.SupportContact}</a>
                  </p>
                  """)}
                </td></tr>

                <!-- Footer -->
                <tr><td style="background:#f4f6f8;padding:20px 40px;text-align:center;border-top:1px solid #e0e0e0">
                  <p style="color:#888;font-size:12px;margin:0">
                    La información declarada será utilizada para procesos de debida diligencia y conocimiento del cliente.<br>
                    Este mensaje es confidencial. Si lo recibió por error, elimínelo.
                  </p>
                </td></tr>
              </table>
            </td></tr>
          </table>
        </body>
        </html>
        """;
}
