using GraduationProject.External.Contract;

namespace GraduationProject.Service
{
    public interface IFaceBookAuthService
    {
        Task<FacebookTokenValidationResult> ValidationAccessTokenAsync(string accessToken);
        Task<FaceBookUserInfoResult> GetUserInfoAsync(string accessToken);
    }
}
