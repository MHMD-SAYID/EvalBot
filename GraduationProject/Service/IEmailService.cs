using GraduationProject.Models;

namespace GraduationProject.Service
{
    public interface IEmailService
    {
        Task SendEmailAsync(MailRequest mailRequest);
    }
}
