using GraduationProject.External.Contract;

namespace GraduationProject.Service
{
    public class FacebookAuthServicce : IFaceBookAuthService
    {
        private const string TokenValidationUrl = "";//https://graph.facebook.com/debug_token
        private const string UserInfoUrl = "";//https://graph.facebook.com/me
        public Task<FaceBookUserInfoResult> GetUserInfoAsync(string accessToken)
        {
            throw new NotImplementedException();
        }

        public Task<FacebookTokenValidationResult> ValidationAccessTokenAsync(string accessToken)
        {
            throw new NotImplementedException();
        }
    }
}
