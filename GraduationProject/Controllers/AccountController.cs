using GraduationProject.DTO;
using GraduationProject.Models;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Mail;
using System.Net;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.WebUtilities;

namespace GraduationProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private IEmailSender _emailSender;
        private readonly UserManager<ApplicationUser> userManager;
        
       
        public AccountController(UserManager<ApplicationUser> UserManager,IEmailSender emailSender)
        {
            userManager = UserManager;
            _emailSender = emailSender;

        }
        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterDTO UserFromRequest)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = new ApplicationUser();
                user.UserName = UserFromRequest.UserName;
                user.Email = UserFromRequest.Email;
                IdentityResult result =
                    await userManager.CreateAsync(user, UserFromRequest.Password);
                if (result.Succeeded)
                {
                    return Created();
                }


                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("password", item.Description);
                }

            }

            return BadRequest(ModelState);
        }
       
        [HttpPost("Login")]
        public async Task<IActionResult> Login(LogInDTO UserFromRequest)
        {

            if (ModelState.IsValid)
            {
                ApplicationUser UserFromDb = await userManager.FindByEmailAsync(UserFromRequest.Email);
                bool found =
                    await userManager.CheckPasswordAsync(UserFromDb, UserFromRequest.Password);
                if (found)
                {
                    List<Claim> UserClaims = new List<Claim>();

                    UserClaims.Add(new Claim(ClaimTypes.NameIdentifier, UserFromDb.Id));
                    UserClaims.Add(new Claim(ClaimTypes.Name, UserFromDb.Email));
                    var userRoles = await userManager.GetRolesAsync(UserFromDb);
                    foreach (var role in userRoles)
                    {
                        UserClaims.Add(new Claim(ClaimTypes.Role, role));
                    }
                    var SignInKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("ksdlkjljskj2325#!#!vnl1jk2!#!@3213!#kjvljicojckl"));
                    SigningCredentials UserCred = new SigningCredentials(SignInKey, SecurityAlgorithms.HmacSha256);
                    JwtSecurityToken mytoken = new JwtSecurityToken
                        (
                            issuer: "http://localhost:5203",
                            audience: "http://localhost:54200",
                            expires: DateTime.Now.AddHours(1),
                            claims: UserClaims,
                            signingCredentials: UserCred


                        );

                    return Ok
                    (
                        new
                        {
                            token = new JwtSecurityTokenHandler().WriteToken(mytoken),
                            expires = DateTime.Now.AddHours(1)
                        }
                    );

                }
                ModelState.AddModelError("Email", "Email or password invalid");
            }

            return BadRequest(ModelState);


        }
        [HttpPost("ForgetPassword")]
        public async Task<IActionResult> ForgetPassword(ForgetPasswordDTO UserFromRequest)
        {
            if (ModelState.IsValid)
            {
                // Check if the user exists in the database
                ApplicationUser UserFromDb = await userManager.FindByEmailAsync(UserFromRequest.Email!);
                if (UserFromDb != null)
                {
                    // Generate password reset token
                    string token = await userManager.GeneratePasswordResetTokenAsync(UserFromDb);//builtIn function
                    var param = new Dictionary<string, string?>
                    {

                        { "token",token},
                        {"email",UserFromRequest.Email  }

                    };

                    var callback = QueryHelpers.AddQueryString(UserFromRequest.ClientUri!, param);
                    //var message = new Message([UserFromDb.Email], "Reset password token", callback, null);
                    await _emailSender.SendEmailAsync(UserFromDb.Email, "Reset password token", callback);
                    return Ok();
                }
            }
            return BadRequest(ModelState);
        }
        private async Task<bool> SendResetPasswordEmail(string email, string resetLink)
        {
            try
            {
                var fromAddress = new MailAddress("leaguetrollacc7@gmail.com", "EvalBot Support");
                var toAddress = new MailAddress(email);
                const string subject = "Password Reset Request";
                string body = $"Please reset your password by clicking <a href='{resetLink}'>here</a>.";

                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com", // Your SMTP server
                    Port = 587, // Port for SMTP
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential("leaguetrollacc7@gmail.com", "42211615mhmd")
                };

                using (var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true
                })
                {
                    await smtp.SendMailAsync(message);
                }

                return true;
            }
            catch (Exception ex)
            {
                // Log the exception (e.g., using a logging framework)
                Console.WriteLine($"Error sending email: {ex.Message}");
                return false;
            }
        }
        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPassword(ResetPasswordDTO model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(model.Email);
                if (user != null)
                {
                    var result = await userManager.ResetPasswordAsync(user, model.Token, model.NewPassword);
                    if (result.Succeeded)
                    {
                        return Ok("Password has been reset successfully.");
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError("Email", "User not found.");
                }
            }
            return BadRequest(ModelState);
        }
        

    }
}
