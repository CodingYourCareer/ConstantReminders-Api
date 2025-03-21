using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using ConstantReminders.Contracts.Interfaces.Business;

namespace ConstantReminders.Services
{
    public class EmailService : ConstantReminders.Contracts.Interfaces.Business.IEmailService
    {
        private readonly SmtpClient _smtpClient;
        private readonly string _fromAddress;

        public EmailService(string smtpHost, int smtpPort, string smtpUser, string smtpPass, string fromAddress)
        {
            _smtpClient = new SmtpClient(smtpHost, smtpPort)
            {
                Credentials = new NetworkCredential(smtpUser, smtpPass),
                EnableSsl = true // Ensures secure email transmission
            };
            _fromAddress = fromAddress;
        }

        public async Task SendMailAsync(string to, string subject, string message, string? templateId = null)
        {
            string htmlBody = WrapMessageInHtml(subject, message); // Apply HTML template

            using (var mailMessage = new MailMessage())
            {
                mailMessage.From = new MailAddress(_fromAddress);
                mailMessage.To.Add(to);
                mailMessage.Subject = subject;
                mailMessage.Body = htmlBody;
                mailMessage.IsBodyHtml = true; // Ensures the email is HTML formatted

                try
                {
                    await _smtpClient.SendMailAsync(mailMessage);
                    Console.WriteLine($"[INFO] Email sent to {to} successfully.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[ERROR] Failed to send email to {to}. Exception: {ex.Message}");
                    throw; // Rethrow so the caller can handle it if needed
                }
            }
        }

        private string WrapMessageInHtml(string subject, string message)
        {
            return $@"
                <html>
                <head><title>{subject}</title></head>
                <body>
                    <h2>{subject}</h2>
                    <p>{message}</p>
                </body>
                </html>";
        }
    }
}

