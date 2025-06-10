
using GraduationProject.Contracts.Company;
using GraduationProject.Contracts.Users;
using GraduationProject.Contracts.Users.Delete;
using GraduationProject.Contracts.Users.Interview;
using GraduationProject.Entities;
using GraduationProject.IService;

namespace GraduationProject.Service
{
    public class CompanyService(AppDbContext context) : ICompanyService
    {
        private readonly AppDbContext _context = context;

       

        public async Task<Result<CompanyProfileResponse>> GetCompanyProfileAsync(string companyId)
        {
            var Imagepath = await _context.UserImage
                .Where(x => x.userId == companyId)
                .Select(x => x.HostedPath)
                .FirstOrDefaultAsync();
            var company = await _context.CompanyProfile
                .Where(x => x.userId == companyId)
                .Include(x => x.user)
                .Select(x => new CompanyProfileResponse
                (
                    x.user.UserName,
                    x.user.Email,
                    Imagepath,
                    x.Jobs != null && x.Jobs.Any()
                        ? x.Jobs.Select(p => new jobsProfile(p.Id,p.Title, p.ReleaseDate)).ToList()
                        : null



                )).SingleAsync();
               
                

         return Result.Success(company);
        }
        public async Task<Result> AddJob(AddJopRequest request, CancellationToken cancellationToken)
        {
            
            var companyExists = await _context.CompanyProfile
                .AnyAsync(x => x.userId == request.companyId, cancellationToken);
            var job = new Job
            {
                Title = request.Title,
                Location = request.Location,
                questionNumber=request.questionNumber,
                track = request.track,
                difficulty = request.difficulty,
                Description = request.Description,
                Requirements = request.Requirements,
                Benefits = request.Benefits,
                companyProfileId = request.companyId,
                ReleaseDate = DateOnly.FromDateTime(DateTime.UtcNow)
            };
            await _context.Jobs.AddAsync(job, cancellationToken);
            var result = await _context.SaveChangesAsync(cancellationToken);

            return result > 0 ? Result.Success() : Result.Failure(PorfileErrors.EducationNotFound);
        }

        public async Task<Result> DeleteJob(DeleteRequest request, CancellationToken cancellationToken)
        {
            var job =await _context.Jobs.Where(x=>x.Id==request.Id && x.companyProfileId==request.userId)
                .FirstOrDefaultAsync(cancellationToken);
            if (job is null) { return Result.Failure(CompanyErrors.JobNotFound); }

            _context.Remove(job);
            var result =await _context.SaveChangesAsync();
            return Result.Success();
        }

        public async Task<Result<GetJobDataResponse>> GetJobData(int Id, CancellationToken cancellationToken)
        {
            var response = await _context.Jobs
                .Where(x => x.Id == Id)
                .Select(j => new GetJobDataResponse
                (
                    j.Id,
                    j.Company.user.Image.HostedPath,
                    j.Title,
                    j.Location,
                    j.Description,
                    j.Requirements,
                    j.Benefits,
                    j.track,
                    j.questionNumber,
                    j.difficulty
                )).Distinct()
                .SingleAsync();
             
            if(response is null )
                return Result.Failure<GetJobDataResponse>(CompanyErrors.JobNotFound);
            return Result.Success(response);
            
        }

        public async Task<Result<List<GetAllJobsResponse>>> GetAllJobs(GetAllJObsRequest request, CancellationToken cancellationToken)
        {
            var jobs = await _context.Jobs.Where(x=>x.companyProfileId==request.companyId)
               .Select(j => new GetAllJobsResponse
               (
                   j.Id,
                   j.Company.user.Image.HostedPath,
                   j.Title,
                   j.Company.user.UserName!,
                   j.Location,
                   j.ReleaseDate

               )).Distinct()
               .ToListAsync();
            return Result.Success(jobs);
        }

        public async Task<Result<List<GetUsersAppliedToJobResponse>>> GetUserAppliedToJob(GetUsersAppliedToJobRequest request, CancellationToken cancellationToken)
        {
            var users =await  _context.JobUserProfiles
                .Where(j => j.jobId == request.jobId)
                .Select(u => new GetUsersAppliedToJobResponse
                (
                    u.userProfileId,
                    u.userProfile.user.Image.HostedPath
                )).ToListAsync(cancellationToken);
            return Result.Success(users);
        }

        
    }
}
