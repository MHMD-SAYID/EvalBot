
namespace GraduationProject.Service
{
    public interface ICompanyService
    {
        Task<Result<CompanyProfileResponse>> GetCompanyProfileAsync(string companyId);
        Task<Result> AddJob(AddJopRequest request,CancellationToken cancellationToken);
        Task<Result> DeleteJob(DeleteRequest request,CancellationToken cancellationToken);
        Task<Result<GetJobDataResponse>> GetJobData(int Id,CancellationToken cancellationToken);
    }
}
