namespace GraduationProject.Service
{
    public interface IAuthService
    {
        Task<Result<AuthResponse>> GetTokenAsync(string email, string password, CancellationToken cancellationToken = default);
        //Task<OneOf<AuthResponse, Error>> GetTokenAsync(string email, string password, CancellationToken cancellationToken = default);
        Task<Result<AuthResponse>> GetRefreshTokenAsync(string token, string refreshToken, CancellationToken cancellationToken = default);
        Task<Result> RevokeRefreshTokenAsync(string token, string refreshToken, CancellationToken cancellationToken = default);
        Task<Result<RegisterResponse>> RegisterWepAsync(RegisterRequest request,/*experienceList experience,businessAccountList accounts,projectList projects,educationList education,*/ CancellationToken cancellationToken = default);
        Task<Result<RegisterResponse>> RegisterFlutterAsync(RegisterRequest request/*, experienceList experience, businessAccountList accounts, projectList projects, educationList education*/, CancellationToken cancellationToken = default);
        Task<Result<RegisterCompanyResponse>> RegisterCompanyAsync(RegisterCompanyRequest request/*, experienceList experience, businessAccountList accounts, projectList projects, educationList education*/, CancellationToken cancellationToken = default);
        //Task<Result> CreateUserRoleAsync();
        Task<Result> ConfirmEmailAsync(ConfirmEmailRequest request);
        Task<Result> ResendConfirmationEmailFlutterAsync(ReSendConfirmationEmail request);
        Task<Result> ResendConfirmationEmailWepAsync(ReSendConfirmationEmail request);

        Task<Result> SendResetPasswordCodeFlutterAsync(string email);
        Task<Result> SendResetPasswordCodeWepAsync(string email);

        Task<Result> ResetPasswordAsync(ResetPasswordRequest request);

      
       
    }
}
