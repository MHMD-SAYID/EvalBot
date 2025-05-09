
using Microsoft.AspNetCore.Identity.Data;
using LoginRequest = GraduationProject.Contracts.Authentication.LoginRequest;
using RefreshTokenRequest = GraduationProject.Contracts.Authentication.RefreshTokenRequest;
using RegisterRequest = GraduationProject.Contracts.Authentication.RegisterRequest;

namespace GraduationProject.Controllers
{
    public class AuthController(IAuthService authService, ILogger<AuthController> logger,AppDbContext context) : ControllerBase
    {
        private readonly IAuthService _authService = authService;
        private readonly AppDbContext _context = context;
        private readonly ILogger<AuthController> _logger = logger;

        [HttpPost("LogIn")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request, CancellationToken cancellationToken)
        {
            bool IsSameType = await _context.UserProfile.AnyAsync(x => x.EmailType == request.EmailType);
            if (IsSameType)
            {
                _logger.LogInformation("Logging with email: {email} and password: {password}", request.Email, request.Password);

            var authResult = await _authService.GetTokenAsync(request.Email, request.Password, cancellationToken);

            return authResult.IsSuccess ? Ok(authResult.Value) : authResult.ToProblem();
            }
            return BadRequest();
            //return authResult.Match(
            //    Ok,
            //    error => Problem(statusCode: StatusCodes.Status400BadRequest, title: error.Code, detail: error.Description)
            //);
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh([FromBody] RefreshTokenRequest request, CancellationToken cancellationToken)
        {
            var authResult = await _authService.GetRefreshTokenAsync(request.Token, request.RefreshToken, cancellationToken);

            return authResult.IsSuccess ? Ok(authResult.Value) : authResult.ToProblem();
        }

        [HttpPost("revoke-refresh-token")]
        public async Task<IActionResult> RevokeRefreshToken([FromBody] RefreshTokenRequest request, CancellationToken cancellationToken)
        {
            var result = await _authService.RevokeRefreshTokenAsync(request.Token, request.RefreshToken, cancellationToken);

            return result.IsSuccess ? Ok() : result.ToProblem();
        }

        [HttpPost("register-wep")]
        public async Task<IActionResult> RegisterWep([FromBody] RegisterRequest request, CancellationToken cancellationToken)
        {
            var result = await _authService.RegisterWepAsync(request ,cancellationToken);

            return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
        }
        [HttpPost("register-flutter")]
        public async Task<IActionResult> RegisterFlutter([FromBody] RegisterRequest request, CancellationToken cancellationToken)
        {
            var result = await _authService.RegisterFlutterAsync(request, cancellationToken);

            return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
        }

        [HttpPost("confirm-email")]
        public async Task<IActionResult> ConfirmEmail([FromBody] ConfirmEmailRequest request, CancellationToken cancellationToken)
        {
            var result = await _authService.ConfirmEmailAsync(request);

            return result.IsSuccess ? Ok() : result.ToProblem();
        }

        [HttpPost("resend-confirmation-email-wep")]
        public async Task<IActionResult> ResendConfirmationEmailwep([FromBody] ReSendConfirmationEmail request, CancellationToken cancellationToken)
        {
            var result = await _authService.ResendConfirmationEmailWepAsync(request);

            return result.IsSuccess ? Ok() : result.ToProblem();
        }
        [HttpPost("resend-confirmation-email-flutter")]
        public async Task<IActionResult> ResendConfirmationflutterEmail([FromBody] ReSendConfirmationEmail request, CancellationToken cancellationToken)
        {
            var result = await _authService.ResendConfirmationEmailFlutterAsync(request);

            return result.IsSuccess ? Ok() : result.ToProblem();
        }
        [HttpPost("forget-password-flutter")]
        public async Task<IActionResult> ForgetPasswordFlutter([FromBody] ForgotPasswordRequest request)
        {
            var result = await _authService.SendResetPasswordCodeFlutterAsync(request.Email);

            return result.IsSuccess ? Ok() : result.ToProblem();
        } 
        [HttpPost("forget-password-wep")]
        public async Task<IActionResult> ForgetPasswordWep([FromBody] ForgotPasswordRequest request)
        {
            var result = await _authService.SendResetPasswordCodeWepAsync(request.Email);

            return result.IsSuccess ? Ok() : result.ToProblem();
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] Contracts.Authentication.ResetPasswordRequest request)
        {
            var result = await _authService.ResetPasswordAsync(request);

            return result.IsSuccess ? Ok() : result.ToProblem();
        }
    }
}
