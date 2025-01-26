using GraduationProject.DTO;
using GraduationProject.Helper;

namespace GraduationProject.Service
{
    public interface IUserService
    {
        Task<APIResponse> UserRegisteration(RegisterDTO userRegister);
        Task<APIResponse> ConfirmRegister(int userid, string username, string otptext);
        Task<APIResponse> ResetPassword(string username, string oldpassword, string newpassword);
        Task<APIResponse> ForgetPassword(string username);
        Task<APIResponse> UpdatePassword(string username, string Password, string Otptext);
    }
}
