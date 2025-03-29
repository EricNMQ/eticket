using Microsoft.Extensions.Hosting.Internal;
using System;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace eTickets.Services
{
    public class EmailService
    {
        private readonly string _smtpServer = "sandbox.smtp.mailtrap.io";
        private readonly int _smtpPort = 587;
        private readonly string _smtpUser = "92a79509dc3a22";
        private readonly string _smtpPass = "c94a7aff8d3f25";

            public async Task<string> LoadEmailTemplateAsync(string filePath, string name, string link)
            {
                if (!File.Exists(filePath))
                {
                    throw new FileNotFoundException("Không tìm thấy template email.", filePath);
                }

                string content = await File.ReadAllTextAsync(filePath);
                content = content.Replace("{{Name}}", name)
                                 .Replace("{{Link}}", link);

                return content;
            }

        public async Task SendEmailAsync(string toEmail, string subject, string body)
        {
            using (SmtpClient client = new SmtpClient(_smtpServer, _smtpPort))
            {
                client.Credentials = new NetworkCredential(_smtpUser, _smtpPass);
                client.EnableSsl = true;

                MailMessage mailMessage = new MailMessage
                {
                    From = new MailAddress("noreply@yourdomain.com"),
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true
                };

                mailMessage.To.Add(toEmail);

                await client.SendMailAsync(mailMessage);
            }
        }
    }
}
