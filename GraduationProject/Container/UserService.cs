using GraduationProject.DTO;
using GraduationProject.Helper;
using GraduationProject.Models;
using GraduationProject.Models.GraduationProject.Models;
using GraduationProject.Service;

namespace GraduationProject.Container
{
    public class UserService : IUserService
    {
        private readonly AppDbContext context;
        private readonly IEmailService emailService;

        public UserService(AppDbContext context, IEmailService emailService)
        {
            this.context = context;
            this.emailService = emailService;
        }

        public async Task<APIResponse> ConfirmRegister(int userid, string username, string otptext)
        {
            throw new NotImplementedException();
        }

        public async Task<APIResponse> ForgetPassword(string username)
        {
            throw new NotImplementedException();
        }

        public async Task<APIResponse> ResetPassword(string username, string oldpassword, string newpassword)
        {
            throw new NotImplementedException();
        }

        public async Task<APIResponse> UpdatePassword(string username, string Password, string Otptext)
        {
            throw new NotImplementedException();
        }

        public async Task<APIResponse> UserRegisteration(RegisterDTO userRegister)
        {
            throw new NotImplementedException();
        }
        private string GenerateRandomNumber() 
        {
            Random random = new Random();
            string randomNo=random.Next(0, 1000000).ToString("D6");//to return 6 digit number
            return randomNo;
        }
        public async Task SendOTPMail(string UserEmail, string username, string OtpText)
        {
            var mailRequest = new MailRequest();

            mailRequest.Email = UserEmail;
            mailRequest.Subject = "Thanks for registering : OTP";
            mailRequest.EmailBody = GenerateEmailBody(username, OtpText);
            await this.emailService.SendEmailAsync(mailRequest);
        }
        private string GenerateEmailBody(string name, string otptext)
        {
            string EmailBody = string.Empty;
            EmailBody = "<div> style='width:100%;background-color: grey; padding: 30px 10px;'>";
            EmailBody += "<h1>Hi " + name + ", Thanks for registering</h1>";
            EmailBody += "<h2>Please enter OTP text and complete the registeration</h2>";
            EmailBody += "<h2>OTP Text is :" + otptext + "</h2>";
            EmailBody += "</div>";
            return EmailBody;
        }
    }
}
