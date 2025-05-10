using GraduationProject.Contracts.Company;
using GraduationProject.Contracts.Users;

namespace GraduationProject.Service
{
    public interface ICompanyService
    {
        Task<Result<CompanyProfileResponse>> GetCompanyProfileAsync(string companyId);
        Task<Result> AddJob(AddJopRequest request,CancellationToken cancellationToken);
    }
}
