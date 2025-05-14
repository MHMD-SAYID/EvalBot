
using GraduationProject.Helpers;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.WebUtilities;
using System.Security.Cryptography;
using System.Text;
using User = GraduationProject.Entities.User;
using Hangfire;

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
        AppDbContext context) : IAuthService
    {
        private readonly UserManager<User> _userManager = userManager;
        private readonly SignInManager<User> _signInManager = signInManager;
        private readonly IJwtProvider _jwtProvider = jwtProvider;
        private readonly ILogger<AuthService> _logger = logger;
        private readonly IEmailSender _emailSender = emailSender;
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
        private readonly AppDbContext _context = context;


        private readonly int _refreshTokenExpiryDays = 14;

        public async Task<Result<RegisterResponse>> RegisterWepAsync(RegisterRequest request,  CancellationToken cancellationToken = default)
        {


            var emailIsExists = await _userManager.Users.AnyAsync(x => x.Email == request.Email, cancellationToken);
            if (emailIsExists)
                return Result.Failure<RegisterResponse>(UserErrors.DuplicatedEmail);

            var UserNameExists = await _userManager.Users.AnyAsync(x => x.UserName == request.UserName, cancellationToken);
            if (UserNameExists)
                return Result.Failure<RegisterResponse>(UserErrors.DuplicatedUserName);
            var user = new User
            {
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                UserName = request.UserName
            };
            //var user = request.Adapt<UserProfile>();

            var result = await _userManager.CreateAsync(user, request.Password);
            
            

            //var usera=await _userManager.FindByEmailAsync(request.Email);
            var usera = await _context.UserProfile.Where(x => x.user.Email == request.Email)
                .FirstOrDefaultAsync();
            usera.Skills = request.Skills;
            if (result.Succeeded)
            {

                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

                _logger.LogInformation("Confirmation code: {code}", code);

                await SendConfirmationEmailWep(user, code);
                
                var  response=new RegisterResponse(usera.userId, usera.user.UserName, usera.CountryOfResidence
                    , usera.user.PhoneNumber, usera.Skills, usera.YearsOfExperience, usera.ExpectedSalary,
                    usera.Nationality, usera.DateOfBirth);
                return Result.Success(response);
            }

            var error = result.Errors.First();

            return Result.Failure<RegisterResponse>(new Error(error.Code, error.Description, StatusCodes.Status400BadRequest));
        }


        public async Task<Result<RegisterResponse>> RegisterCompanyWepAsync(RegisterRequest request, CancellationToken cancellationToken = default)
        {


            var emailIsExists = await _userManager.Users.AnyAsync(x => x.Email == request.Email, cancellationToken);
            if (emailIsExists)
                return Result.Failure<RegisterResponse>(UserErrors.DuplicatedEmail);

            var UserNameExists = await _userManager.Users.AnyAsync(x => x.UserName == request.UserName, cancellationToken);
            if (UserNameExists)
                return Result.Failure<RegisterResponse>(UserErrors.DuplicatedUserName);
            var user = new User
            {
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                UserName = request.UserName
            };
            //var user = request.Adapt<UserProfile>();

            var result = await _userManager.CreateAsync(user, request.Password);



            //var usera=await _userManager.FindByEmailAsync(request.Email);
            var usera = await _context.UserProfile.Where(x => x.user.Email == request.Email)
                .FirstOrDefaultAsync();
            usera.Skills = request.Skills;
            if (result.Succeeded)
            {

                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

                _logger.LogInformation("Confirmation code: {code}", code);

                await SendConfirmationEmailWep(user, code);

                var response = new RegisterResponse(usera.userId, usera.user.UserName, usera.CountryOfResidence
                    , usera.user.PhoneNumber, usera.Skills, usera.YearsOfExperience, usera.ExpectedSalary,
                    usera.Nationality, usera.DateOfBirth);
                return Result.Success(response);
            }

            var error = result.Errors.First();

            return Result.Failure<RegisterResponse>(new Error(error.Code, error.Description, StatusCodes.Status400BadRequest));
        }



        public async Task<Result<RegisterResponse>> RegisterFlutterAsync(RegisterRequest request, CancellationToken cancellationToken = default)
        {

            //var emailIsExists = await _userManager.Users.AnyAsync(x => x.Email == request.Email, cancellationToken);
            //if (emailIsExists)
            //    return Result.Failure<RegisterResponse>(UserErrors.DuplicatedEmail);

            //var UserNameExists = await _userManager.Users.AnyAsync(x => x.UserName == request.UserName, cancellationToken);
            //if (UserNameExists)
            //    return Result.Failure<RegisterResponse>(UserErrors.DuplicatedUserName);
            //var user = request.Adapt<User>();
            ////var user = new User
            ////{
            ////    Email = request.Email,
            ////    PhoneNumber = request.PhoneNumber,
            ////    UserName = request.UserName
            ////};


            //user.EmailConfirmed = true;
            //var result = await _userManager.CreateAsync(user, request.Password);
            //var id=await _userManager.Users.Where(x => x.UserName == request.UserName).Select(x=>x.Id).FirstOrDefaultAsync( cancellationToken);
            //var profileu=new UserProfile { userId = user.Id };
            //_context.UserProfile.Add(profileu);
            //await _context.SaveChangesAsync(cancellationToken);
            //var profile=await _context.UserProfile.FirstOrDefaultAsync(x=>x.userId==id);
            ////var profile = new UserProfile
            //{
            //    //userId = user.Id,
            //    profile.CountryOfResidence = request.CountryOfResidence;
            //    profile.Skills = request.Skills;
            //    profile.Nationality = request.Nationality;
            //    profile.DateOfBirth = request.DateOfBirth;
            //    profile.ExpectedSalary = request.ExpectedSalary;
            //    profile.YearsOfExperience = request.YearsOfExperience;
            //};
            //profile.Skills = request.Skills;
            ////var usera = await _userManager.FindByEmailAsync(request.Email);
            ////_context.UserProfile.Add(profile);
            //_context.Update(profile);
            //await _context.SaveChangesAsync(cancellationToken);
            //var usera = await _context.UserProfile.Include(x => x.user)
            //    .FirstOrDefaultAsync(x => x.userId == user.Id);

            //if (result.Succeeded)
            //{

            //    //var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            //    //code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

            //    //_logger.LogInformation("Confirmation code: {code}", code);

            //    //await SendConfirmationEmailFlutter(user, code);


            //    var response = new RegisterResponse(usera.userId,usera.user.UserName,usera.CountryOfResidence
            //        ,usera.user.PhoneNumber,usera.Skills,usera.YearsOfExperience,usera.ExpectedSalary,
            //        usera.Nationality, usera.DateOfBirth);
            //    return Result.Success(response);
            //}

            //var error = result.Errors.First();

            //return Result.Failure<RegisterResponse>(new Error(error.Code, error.Description, StatusCodes.Status400BadRequest));
            if (request == null || string.IsNullOrEmpty(request.Email))
            {
                return Result.Failure<RegisterResponse>(new Error("InvalidInput", "Email is required.", StatusCodes.Status400BadRequest));
            }
            if (string.IsNullOrEmpty(request.UserName))
            {
                return Result.Failure<RegisterResponse>(new Error("InvalidInput", "UserName is required.", StatusCodes.Status400BadRequest));
            }
            if (string.IsNullOrEmpty(request.Password))
            {
                return Result.Failure<RegisterResponse>(new Error("InvalidInput", "Password is required.", StatusCodes.Status400BadRequest));
            }

            // Check for duplicate email
            _logger.LogInformation("Checking email: {Email}", request.Email);
            var emailIsExists = await _userManager.Users.AnyAsync(x => x.Email == request.Email, cancellationToken);
            if (emailIsExists)
            {
                return Result.Failure<RegisterResponse>(UserErrors.DuplicatedEmail);
            }

            // Check for duplicate username
            var userNameExists = await _userManager.Users.AnyAsync(x => x.UserName == request.UserName, cancellationToken);
            if (userNameExists)
            {
                return Result.Failure<RegisterResponse>(UserErrors.DuplicatedUserName);
            }

            // Create user
            var user = request.Adapt<User>();
            user.EmailConfirmed = true;
            var result = await _userManager.CreateAsync(user, request.Password);
            if (!result.Succeeded)
            {
                var error = result.Errors.First();
                return Result.Failure<RegisterResponse>(new Error(error.Code, error.Description, StatusCodes.Status400BadRequest));
            }

            // Create and save UserProfile
            var profile = new UserProfile
            {
                userId = user.Id,
                CountryOfResidence = request.CountryOfResidence,
                Skills = request.Skills,
                Nationality = request.Nationality,
                DateOfBirth = request.DateOfBirth,
                ExpectedSalary = request.ExpectedSalary,
                YearsOfExperience = request.YearsOfExperience,
                EmailType= request.EmailType
            };
            _context.UserProfile.Add(profile);
            await _context.SaveChangesAsync(cancellationToken);

            // Retrieve UserProfile with User
            var userProfile = await _context.UserProfile
                .Include(x => x.user)
                .FirstOrDefaultAsync(x => x.userId == user.Id, cancellationToken);

            if (userProfile == null)
            {
                return Result.Failure<RegisterResponse>(new Error("ProfileNotFound", "Failed to create user profile.", StatusCodes.Status500InternalServerError));
            }

            // Create response
            var response = new RegisterResponse(
                userProfile.userId,
                userProfile.user.UserName,
                userProfile.CountryOfResidence,
                userProfile.user.PhoneNumber,
                userProfile.Skills,
                userProfile.YearsOfExperience,
                userProfile.ExpectedSalary,
                userProfile.Nationality,
                userProfile.DateOfBirth);

            return Result.Success(response);
        }

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

        public async Task<Result> ResendConfirmationEmailWepAsync(ReSendConfirmationEmail request)
        {
            if (await _userManager.FindByEmailAsync(request.Email) is not { } user)
                return Result.Success();

            if (user.EmailConfirmed)
                return Result.Failure(UserErrors.DuplicatedConfirmation);

            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

            _logger.LogInformation("Confirmation code: {code}", code);

            await SendConfirmationEmailWep(user, code);

            return Result.Success();
        }
        public async Task<Result> ResendConfirmationEmailFlutterAsync(ReSendConfirmationEmail request)
        {
            if (await _userManager.FindByEmailAsync(request.Email) is not { } user)
                return Result.Success();

            if (user.EmailConfirmed)
                return Result.Failure(UserErrors.DuplicatedConfirmation);

            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

            _logger.LogInformation("Confirmation code: {code}", code);

            await SendConfirmationEmailFlutter(user, code);

            return Result.Success();
        }

        private static string GenerateRefreshToken()
        {
            return Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
        }
       
        private async Task SendConfirmationEmailWep(User user, string code)
        {
            // request URL
            var origin = _httpContextAccessor.HttpContext?.Request.Headers.Origin;

            var emailBody = EmailBodyBuilder.GenerateEmailBody("EmailConfirmation",
                templateModel: new Dictionary<string, string>
                {
                { "{{name}}", user.UserName },
                //change it to your front end url

                    { "{{action_url}}", $"{origin}/auth/emailConfirmation?userId={user.Id}&code={code}" }
                }
            );

            await _emailSender.SendEmailAsync(user.Email!, "✅ EvalBot: Email Confirmation", emailBody);
        }
        private async Task SendConfirmationEmailFlutter(User user, string code)
        {
            // request URL
            var origin = _httpContextAccessor.HttpContext?.Request.Headers.Origin;

            var emailBody = EmailBodyBuilder.GenerateEmailBody("EmailConfirmation",
                templateModel: new Dictionary<string, string>
                {
                { "{{name}}", user.UserName },
                //change it to your front end url

                    { "{{action_url}}", $"{origin}/auth/emailConfirmation?userId={user.Id}&code={code}" }
                }
            );

            await _emailSender.SendEmailAsync(user.Email!, "✅ EvalBot: Email Confirmation", emailBody);
        }
        
        public async Task<Result> SendResetPasswordCodeFlutterAsync(string email)
        {
            if (await _userManager.FindByEmailAsync(email) is not { } user)
                return Result.Success();

            if (!user.EmailConfirmed)
                return Result.Failure(UserErrors.EmailNotConfirmed);

            var code = await _userManager.GeneratePasswordResetTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

            _logger.LogInformation("Reset code: {code}", code);

            await SendResetPasswordEmailFlutter(user, code);

            return Result.Success();
        }
        public async Task<Result> SendResetPasswordCodeWepAsync(string email)
        {
            if (await _userManager.FindByEmailAsync(email) is not { } user)
                return Result.Success();

            if (!user.EmailConfirmed)
                return Result.Failure(UserErrors.EmailNotConfirmed);

            var code = await _userManager.GeneratePasswordResetTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

            _logger.LogInformation("Reset code: {code}", code);

            await SendResetPasswordEmailWep(user, code);

            return Result.Success();
        }

        public async Task<Result> ResetPasswordAsync(ResetPasswordRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user is null || !user.EmailConfirmed)
                return Result.Failure(UserErrors.InvalidCode);

            IdentityResult result;

            try
            {
                var code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(request.Code));
                result = await _userManager.ResetPasswordAsync(user, code, request.NewPassword);
            }
            catch (FormatException)
            {
                result = IdentityResult.Failed(_userManager.ErrorDescriber.InvalidToken());
            }

            if (result.Succeeded)
                return Result.Success();

            var error = result.Errors.First();

            return Result.Failure(new Error(error.Code, error.Description, StatusCodes.Status401Unauthorized));
        }

        private async Task SendResetPasswordEmailFlutter(User user, string code)
        {
            var origin = _httpContextAccessor.HttpContext?.Request.Headers.Origin;

            var emailBody = EmailBodyBuilder.GenerateEmailBody("ForgetPassword",
                templateModel: new Dictionary<string, string>
                {
                { "{{name}}", user.UserName },
                
                //change it to your front end url
                { "{{action_url}}", $"{origin}/auth/forgetPassword?email={user.Email}&code={code}" }
                }
            );

            BackgroundJob.Enqueue(() => _emailSender.SendEmailAsync(user.Email!, "✅ EvalBot: Change Password", emailBody));

            await Task.CompletedTask;
        }
        private async Task SendResetPasswordEmailWep(User user, string code)
        {
            var origin = _httpContextAccessor.HttpContext?.Request.Headers.Origin;

            var emailBody = EmailBodyBuilder.GenerateEmailBody("ForgetPassword",
                templateModel: new Dictionary<string, string>
                {
                { "{{name}}", user.UserName },
                
                //change it to your front end url
                { "{{action_url}}", $"{origin}/auth/forgetPassword?email={user.Email}&code={code}" }
                }
            );

            BackgroundJob.Enqueue(() => _emailSender.SendEmailAsync(user.Email!, "✅ EvalBot: Change Password", emailBody));

            await Task.CompletedTask;
        }

        public async Task<Result<RegisterCompanyResponse>> RegisterCompanyAsync(RegisterCompanyRequest request, CancellationToken cancellationToken = default)
        {
            var emailexists= await  _userManager.Users.AnyAsync(x=>x.Email==request.Email,cancellationToken);
            if (emailexists)
                return Result.Failure<RegisterCompanyResponse>(UserErrors.DuplicatedEmail);
            var usernameexists = await _userManager.Users.AnyAsync(x => x.UserName == request.UserName, cancellationToken);
            if (usernameexists)
                return Result.Failure<RegisterCompanyResponse>(UserErrors.DuplicatedUserName);

            var user = request.Adapt<User>();
            user.EmailConfirmed = true;
            //var user = new User
            //{
            //    Email=request.Email,
            //    EmailConfirmed=true,
            //    UserName=request.UserName,

            //};

            var result =await _userManager.CreateAsync(user, request.Password);
            if (!result.Succeeded)
            {
                var error = result.Errors.First();
                return Result.Failure<RegisterCompanyResponse>(new Error(error.Code, error.Description, StatusCodes.Status400BadRequest));
            }
            var company = new CompanyProfile
            {
                userId = user.Id,
                Location = request.Location
            };
            _context.Add(company);
            await _context.SaveChangesAsync(cancellationToken);
            var companyProfile=await _context.CompanyProfile
                .Include(x=>x.user)
                .FirstOrDefaultAsync(x=>x.userId==user.Id);
            var response = new RegisterCompanyResponse
            (

                companyProfile.user.Email,
                companyProfile.user.UserName,
                companyProfile.Location

            );

            return Result.Success(response);
        }
    }
}
