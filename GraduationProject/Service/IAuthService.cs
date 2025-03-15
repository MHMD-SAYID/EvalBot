namespace GraduationProject.Service
{
    public interface IAuthService
    {
        Task<Result<AuthResponse>> GetTokenAsync(string email, string password, CancellationToken cancellationToken = default);
        //Task<OneOf<AuthResponse, Error>> GetTokenAsync(string email, string password, CancellationToken cancellationToken = default);
        Task<Result<AuthResponse>> GetRefreshTokenAsync(string token, string refreshToken, CancellationToken cancellationToken = default);
        Task<Result> RevokeRefreshTokenAsync(string token, string refreshToken, CancellationToken cancellationToken = default);
        Task<Result> RegisterAsync(Contracts.Authentication.RegisterRequest request, CancellationToken cancellationToken = default);
        //Task<Result> CreateUserRoleAsync();
        Task<Result> ConfirmEmailAsync(ConfirmEmailRequest request);
        Task<Result> ResendConfirmationEmailAsync(ReSendConfirmationEmail request);
        Task<Result> ResetPassword(ResetPasswordRequest request);
    }
}
