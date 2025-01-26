using GraduationProject.Models;
using GraduationProject.Service;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;

namespace GraduationProject.Container
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings emailSettings;
        public EmailService(IOptions<EmailSettings> options)
        {
            this.emailSettings = options.Value;
        }
        public async Task SendEmail(MailRequest mailrequest)
        {
            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(emailSettings.Email);
            email.To.Add(MailboxAddress.Parse(mailrequest.Email));
            email.Subject = mailrequest.Subject;
            var builder = new BodyBuilder();
            builder.HtmlBody = mailrequest.EmailBody;
            email.Body = builder.ToMessageBody();

            using var smptp = new SmtpClient();
            smptp.Connect(emailSettings.Host, emailSettings.Port, SecureSocketOptions.StartTls);
            smptp.Authenticate(emailSettings.Email, emailSettings.Password);
            await smptp.SendAsync(email);
            smptp.Disconnect(true);
        }

        public async Task SendEmailAsync(MailRequest mailRequest)
        {
            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(emailSettings.Email);
            email.To.Add(MailboxAddress.Parse(mailRequest.Email));
            email.Subject = mailRequest.Subject;
            var builder = new BodyBuilder();
            builder.HtmlBody = mailRequest.EmailBody;
            email.Body = builder.ToMessageBody();

            using var smptp = new SmtpClient();
            smptp.Connect(emailSettings.Host, emailSettings.Port, SecureSocketOptions.StartTls);
            smptp.Authenticate(emailSettings.Email, emailSettings.Password);
            await smptp.SendAsync(email);
            smptp.Disconnect(true);
        }
    }
}
