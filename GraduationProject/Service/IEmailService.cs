using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using GraduationProject.Models;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace GraduationProject.Services
{
    public class EmailSender : IEmailSender
    {
        private readonly EmailConfiguration _emailConfig;

        public EmailSender(EmailConfiguration emailConfig)
        {
            _emailConfig = emailConfig;
        }

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            using (var client = new SmtpClient(_emailConfig.SmtpHost, _emailConfig.Port))
            {
                client.Credentials = new NetworkCredential(_emailConfig.Email, _emailConfig.Password);
                client.EnableSsl = _emailConfig.EnableSsl;

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(_emailConfig.Email),
                    Subject = subject,
                    Body = htmlMessage,
                    IsBodyHtml = true
                };

                mailMessage.To.Add(email);

                await client.SendMailAsync(mailMessage);
            }
        }
    }
}
