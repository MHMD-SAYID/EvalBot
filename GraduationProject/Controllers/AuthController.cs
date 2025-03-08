﻿using Google.Apis.Auth.OAuth2.Requests;
using GraduationProject.Contracts.Authentication;
using GraduationProject.DTO;
using GraduationProject.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
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
            bool IsSameType = await _context.Users.AnyAsync(x => x.EmailType == request.EmailType);
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

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request, CancellationToken cancellationToken)
        {
            var result = await _authService.RegisterAsync(request ,cancellationToken);

            return result.IsSuccess ? Ok() : result.ToProblem();
        }

        [HttpPost("confirm-email")]
        public async Task<IActionResult> ConfirmEmail([FromBody] ConfirmEmailRequest request, CancellationToken cancellationToken)
        {
            var result = await _authService.ConfirmEmailAsync(request);

            return result.IsSuccess ? Ok() : result.ToProblem();
        }

        [HttpPost("resend-confirmation-email")]
        public async Task<IActionResult> ResendConfirmationEmail([FromBody] ReSendConfirmationEmail request, CancellationToken cancellationToken)
        {
            var result = await _authService.ResendConfirmationEmailAsync(request);

            return result.IsSuccess ? Ok() : result.ToProblem();
        }
    }
}
