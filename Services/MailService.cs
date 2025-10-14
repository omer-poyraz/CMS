using System.Net;
using System.Net.Mail;

namespace Services
{
    public interface IMailService
    {
        Task SendMailAsync(string to, string subject, string body, bool isBodyHtml = true, string? from = null, string? displayName = null, Attachment[]? attachments = null);
    }

    public class MailService : IMailService
    {
        private readonly SmtpClient _smtpClient;
        private readonly string _defaultFrom;
        private readonly string? _defaultDisplayName;

        public MailService(string host, int port, string username, string password, string defaultFrom, string? defaultDisplayName = null, bool enableSsl = true)
        {
            _smtpClient = new SmtpClient(host, port)
            {
                Credentials = new NetworkCredential(username, password),
                EnableSsl = enableSsl
            };
            _defaultFrom = defaultFrom;
            _defaultDisplayName = defaultDisplayName;
        }

        public async Task SendMailAsync(string to, string subject, string body, bool isBodyHtml = true, string? from = null, string? displayName = null, Attachment[]? attachments = null)
        {
            try
            {
                using var message = new MailMessage();
                message.To.Add(to);
                message.Subject = subject;
                message.Body = body;
                message.IsBodyHtml = isBodyHtml;
                message.From = new MailAddress(from ?? _defaultFrom, displayName ?? _defaultDisplayName);
                if (attachments != null)
                {
                    foreach (var att in attachments)
                        message.Attachments.Add(att);
                }
                await _smtpClient.SendMailAsync(message);
            }
            catch (Exception ex)
            {
                throw new Exception("Email gönderilirken bir hata oluştu: " + ex.Message);
            }
        }
    }
}
