﻿using GraduationProject.Contracts.Users;
using GraduationProject.Contracts.Users.Interview;

namespace GraduationProject.IService
{
    public interface ICompanyService
    {
        Task<Result<CompanyProfileResponse>> GetCompanyProfileAsync(string companyId);
        Task<Result> AddJob(AddJopRequest request, CancellationToken cancellationToken);
        Task<Result> DeleteJob(DeleteRequest request, CancellationToken cancellationToken);
        Task<Result<GetJobDataResponse>> GetJobData(int Id, CancellationToken cancellationToken);
        Task<Result<List<GetAllJobsResponse>>> GetAllJobs(GetAllJObsRequest request, CancellationToken cancellationToken);
        Task<Result<List<GetUsersAppliedToJobResponse>>> GetUserAppliedToJob(GetUsersAppliedToJobRequest request, CancellationToken cancellationToken);
        
    }
}
