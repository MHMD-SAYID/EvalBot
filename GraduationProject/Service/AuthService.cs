

using GraduationProject.Contracts.Authentication;
using GraduationProject.Helpers;
using GraduationProject.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.WebUtilities;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using BusinessAccount = GraduationProject.Entities.BusinessAccount;
using Education = GraduationProject.Entities.Education;
using Experience = GraduationProject.Entities.Experience;
using Project = GraduationProject.Entities.Project;
using System.Web.Providers.Entities;
using User = GraduationProject.Entities.User;

namespace GraduationProject.Service
{
    public class AuthService(
     UserManager<User> userManager,
     SignInManager<User> signInManager,
     RoleManager<IdentityRole> roleManager,
     IJwtProvider jwtProvider,
     ILogger<AuthService> logger,
     IEmailSender emailSender,
     IHttpContextAccessor httpContextAccessor,
        AppDbContext context ) : IAuthService
    {
        private readonly UserManager<User> _userManager = userManager;
        private readonly SignInManager<User> _signInManager = signInManager;
        private readonly IJwtProvider _jwtProvider = jwtProvider;
        private readonly ILogger<AuthService> _logger = logger;
        private readonly IEmailSender _emailSender = emailSender;
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
        private readonly AppDbContext _context = context;
       
        
        private readonly int _refreshTokenExpiryDays = 14;

      

        public async Task<Result<AuthResponse>> GetTokenAsync(string email, string password, CancellationToken cancellationToken = default)
        {
            
            if (await _userManager.FindByEmailAsync(email) is not { } user)
                return Result.Failure<AuthResponse>(UserErrors.InvalidCredentials);

            var result = await _signInManager.PasswordSignInAsync(user, password, false, false);

            if (result.Succeeded)
            {
                var (token, expiresIn) = _jwtProvider.GenerateToken(user);
                var refreshToken = GenerateRefreshToken();
                var refreshTokenExpiration = DateTime.UtcNow.AddDays(_refreshTokenExpiryDays);

                user.RefreshTokens.Add(new RefreshToken
                {
                    Token = refreshToken,
                    ExpiresOn = refreshTokenExpiration
                });

                await _userManager.UpdateAsync(user);

                var response = new AuthResponse(user.Id, user.Email, user.UserName, token, expiresIn, refreshToken, refreshTokenExpiration);

                return Result.Success(response);
            }

            return Result.Failure<AuthResponse>(result.IsNotAllowed ? UserErrors.EmailNotConfirmed : UserErrors.InvalidCredentials);
        }
        
        //public async Task<OneOf<AuthResponse, Error>> GetTokenAsync(string email, string password, CancellationToken cancellationToken = default)
        //{
        //    var user = await _userManager.FindByEmailAsync(email);

        //    if (user is null)
        //        return UserErrors.InvalidCredentials;

        //    var isValidPassword = await _userManager.CheckPasswordAsync(user, password);

        //    if (!isValidPassword)
        //        return UserErrors.InvalidCredentials;

        //    var (token, expiresIn) = _jwtProvider.GenerateToken(user);
        //    var refreshToken = GenerateRefreshToken();
        //    var refreshTokenExpiration = DateTime.UtcNow.AddDays(_refreshTokenExpiryDays);

        //    user.RefreshTokens.Add(new RefreshToken
        //    {
        //        Token = refreshToken,
        //        ExpiresOn = refreshTokenExpiration
        //    });

        //    await _userManager.UpdateAsync(user);

        //    return new AuthResponse(user.Id, user.Email, user.FirstName, user.LastName, token, expiresIn, refreshToken, refreshTokenExpiration);
        //}
       public async Task CreateRoles(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            string[] roles = { "User", "Company" };

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }
        }

        public async Task<Result<AuthResponse>> GetRefreshTokenAsync(string token, string refreshToken, CancellationToken cancellationToken = default)
        {
            var userId = _jwtProvider.ValidateToken(token);

            if (userId is null)
                return Result.Failure<AuthResponse>(UserErrors.InvalidJwtToken);

            var user = await _userManager.FindByIdAsync(userId);

            if (user is null)
                return Result.Failure<AuthResponse>(UserErrors.InvalidJwtToken);

            var userRefreshToken = user.RefreshTokens.SingleOrDefault(x => x.Token == refreshToken && x.IsActive);

            if (userRefreshToken is null)
                return Result.Failure<AuthResponse>(UserErrors.InvalidRefreshToken);

            userRefreshToken.RevokedOn = DateTime.UtcNow;

            var (newToken, expiresIn) = _jwtProvider.GenerateToken(user);
            var newRefreshToken = GenerateRefreshToken();
            var refreshTokenExpiration = DateTime.UtcNow.AddDays(_refreshTokenExpiryDays);

            user.RefreshTokens.Add(new RefreshToken
            {
                Token = newRefreshToken,
                ExpiresOn = refreshTokenExpiration
            });

            await _userManager.UpdateAsync(user);

            var response = new AuthResponse(user.Id, user.Email, user.UserName, newToken, expiresIn, newRefreshToken, refreshTokenExpiration);

            return Result.Success(response);
        }

        public async Task<Result> RevokeRefreshTokenAsync(string token, string refreshToken, CancellationToken cancellationToken = default)
        {
            var userId = _jwtProvider.ValidateToken(token);

            if (userId is null)
                return Result.Failure(UserErrors.InvalidJwtToken);

            var user = await _userManager.FindByIdAsync(userId);

            if (user is null)
                return Result.Failure(UserErrors.InvalidJwtToken);

            var userRefreshToken = user.RefreshTokens.SingleOrDefault(x => x.Token == refreshToken && x.IsActive);

            if (userRefreshToken is null)
                return Result.Failure(UserErrors.InvalidRefreshToken);

            userRefreshToken.RevokedOn = DateTime.UtcNow;

            await _userManager.UpdateAsync(user);

            return Result.Success();
        }
        
        public async Task<Result> RegisterAsync(RegisterRequest request, CancellationToken cancellationToken = default)
        {
            
           
            var emailIsExists = await _userManager.Users.AnyAsync(x => x.Email == request.Email, cancellationToken);
            if (emailIsExists)
                return Result.Failure(UserErrors.DuplicatedEmail);

            var UserNameExists = await _userManager.Users.AnyAsync(x => x.UserName == request.UserName, cancellationToken);
            if (UserNameExists)
                return Result.Failure(UserErrors.DuplicatedUserName);
            var user = request.Adapt<User>();
           
             //_userManager.AddToRoleAsync(user,"User").Wait();

            //user.UserName = $"{request.FirstName}_{request.LastName}".ToLower();
            
            user.Skills = request.Skills;
            var result = await _userManager.CreateAsync(user, request.Password);
             // Convert list to JSON
            
            if (request.Projects?.Any() == true)
            {
                var projects = request.Projects.Select(p => new Project
                {
                    Name = p.name,
                    Link = p.link,
                    UserId = user.Id
                }).ToList();

                await _context.Projects.AddRangeAsync(projects, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);
            }
            if (request.Experiences?.Any() == true)
            {
                var Experience = request.Experiences.Select(p => new Experience
                {
                    JobTitle = p.JobTitle,
                    CompanyName = p.CompanyName,
                    StillWorkingThere = p.StillWorkingThere,
                    StartDate = p.StartDate,
                    EndDate = p.EndDate,
                    UserId = user.Id
                }).ToList();

                await _context.Experience.AddRangeAsync(Experience, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);
            }
            if (request.Educations?.Any() == true)
            {
                var Education = request.Educations.Select(p => new Education
                {
                    Institution = p.Institution,
                    Degree=p.Degree,
                    FieldOfStudy = p.FieldOfStudy,
                    IsUnderGraduate = p.IsUnderGraduate,
                    StartDate=p.StartDate,
                    EndDate=p.EndDate,
                    UserId = user.Id
                }).ToList();

                await _context.Education.AddRangeAsync(Education, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);
            }
            if (request.Accounts?.Any() == true)
            {
                var Accounts = request.Accounts.Select(p => new BusinessAccount
                {
                    Type=p.AccountType,
                    Link=p.AccountLink,
                    UserId = user.Id
                }).ToList();

                await _context.businessAccounts.AddRangeAsync(Accounts, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);
            }

            //await _context.SaveChangesAsync();



            if (result.Succeeded)
            {

                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

                _logger.LogInformation("Confirmation code: {code}", code);

                await SendConfirmationEmail(user, code);

                return Result.Success();
            }

            var error = result.Errors.First();

            return Result.Failure(new Error(error.Code, error.Description, StatusCodes.Status400BadRequest));
        }

        public async Task<Result> ConfirmEmailAsync(ConfirmEmailRequest request)
        {
            if (await _userManager.FindByIdAsync(request.UserId) is not { } user)
                return Result.Failure(UserErrors.InvalidCode);

            if (user.EmailConfirmed)
                return Result.Failure(UserErrors.DuplicatedConfirmation);

            var code = request.Code;

            try
            {
                code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
            }
            catch (FormatException)
            {
                return Result.Failure(UserErrors.InvalidCode);
            }

            var result = await _userManager.ConfirmEmailAsync(user, code);

            if (result.Succeeded)
                return Result.Success();

            var error = result.Errors.First();

            return Result.Failure(new Error(error.Code, error.Description, StatusCodes.Status400BadRequest));
        }

        public async Task<Result> ResendConfirmationEmailAsync(ReSendConfirmationEmail request)
        {
            if (await _userManager.FindByEmailAsync(request.Email) is not { } user)
                return Result.Success();

            if (user.EmailConfirmed)
                return Result.Failure(UserErrors.DuplicatedConfirmation);

            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

            _logger.LogInformation("Confirmation code: {code}", code);

            await SendConfirmationEmail(user, code);

            return Result.Success();
        }

        private static string GenerateRefreshToken()
        {
            return Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
        }
       
        private async Task SendConfirmationEmail(User user, string code)
        {
            var origin = _httpContextAccessor.HttpContext?.Request.Headers.Origin;

            var emailBody = EmailBodyBuilder.GenerateEmailBody("EmailConfirmation",
                templateModel: new Dictionary<string, string>
                {
                { "{{name}}", user.UserName },
                    { "{{action_url}}", $"{origin}/auth/emailConfirmation?userId={user.Id}&code={code}" }
                }
            );

            await _emailSender.SendEmailAsync(user.Email!, "✅ EvalBot: Email Confirmation", emailBody);
        }

        public async Task<Result> ResetPassword(ResetPasswordRequest request)
        {
            bool EmailExists=await _userManager.Users.AnyAsync(x => x.Email == request.Email);
            if (EmailExists)
            { 
                var user = await _userManager.FindByEmailAsync(request.Email);
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

                _logger.LogInformation("Confirmation code: {code}", code);

                await SendConfirmationEmail(user, code);

                return Result.Success();
            }
                return Result.Failure(UserErrors.EmailNotFound);
            

        }

        //public Task<Result> CreateUserRoleAsync()
        //{
        //    AppDbContext context;
        //    var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
        //    var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

        //    if (!roleManager.RoleExists("Manager"))
        //    {
        //        var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
        //        role.Name = "Manager";
        //        roleManager.Create(role);

        //    }
        //}
    }
}
