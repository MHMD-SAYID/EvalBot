using GraduationProject.Contracts.Users;
using GraduationProject.Contracts.Users.Add;
using GraduationProject.Contracts.Users.Delete;
using GraduationProject.Contracts.Users.Update;

namespace GraduationProject.Service
{
    public interface IUserService
    {
        Task<Result<UserProfileResponse>> GetProfileAsync(string userId);
        Task<Result> UpdateBio(UpdateBioRequest updateProfileRequest,CancellationToken cancellationToken);
        Task<Result> AddExperience(AddExperienceRequest request,CancellationToken cancellationToken);
        Task<Result> AddEducation(AddEducationRequest request,CancellationToken cancellationToken);
        Task<Result> AddProject(AddProjectRequest request,CancellationToken cancellationToken);
        Task<Result> AddBusinessAcount(AddBusinessAccountRequest request,CancellationToken cancellationToken);
        Task<Result> AddLanguages(AddLanguagesRequest request, CancellationToken cancellationToken);
        Task<Result> DeleteEducation(DeleteRequest request,CancellationToken cancellationToken);
        Task<Result> DeleteExperience(DeleteRequest request,CancellationToken cancellationToken);
        Task<Result> DeleteProject(DeleteRequest request,CancellationToken cancellationToken);
        Task<Result> DeleteBusinessAccountLink(DeleteRequest request,CancellationToken cancellationToken);
        Task<Result> DeleteLanguages(DeleteRequest request, CancellationToken cancellationToken);
        Task<Result> DeleteAccount(string userId,CancellationToken cancellationToken);
        Task<Result> UpdateSkills(UpdateSkillsRequest request,CancellationToken cancellationToken);
        Task<Result> UpdateExperience(UpdateExperienceRequest request,CancellationToken cancellationToken);
        Task<Result> UpdateEducation(UpdateEducationRequest request,CancellationToken cancellationToken);
        Task<Result> UpdateProject(UpdateProjectRequest request,CancellationToken cancellationToken);
        Task<Result> UpdateBusinessAccount(UpdateBusinessAccountRequest request,CancellationToken cancellationToken);
        Task<Result> UpdateLanguage(UpdateLanguageRequest request,CancellationToken cancellationToken);

    }
}
