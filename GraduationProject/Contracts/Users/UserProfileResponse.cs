using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using GraduationProject.Authentication;
namespace GraduationProject.Contracts.Users
{
    public record UserProfileResponse
    (
        string Email,
        string UserName,
        List<string>Skills,
        List<Authentication.Project> Projects,
        List<Authentication.Experience> Experiences,
        List<Authentication.Education> Educations,
        List<Authentication.BusinessAccount> Accounts,
        string FirstLanguage,
        string FirstLanguageLevel,
        string SecondLanguage,
        string SecondLanguageLevel

    );
}
