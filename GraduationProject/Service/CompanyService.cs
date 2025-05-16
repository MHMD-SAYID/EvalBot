
using GraduationProject.Contracts.Company;
using GraduationProject.Contracts.Users;
using GraduationProject.Contracts.Users.Delete;
using GraduationProject.Entities;

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
            if (request.Jobs?.Any() != true)
                return Result.Success();
            var job = request.Jobs.Select(p => new Job
            {
                applicaitonLink = p.applicaitonLink,
                Benefits = p.Benefits,
                Description= p.Description,
                Location = p.Location,
                Requirements = p.Requirements,
                Title = p.Title,
                companyProfileId=request.companyId


            }).ToList();
            await _context.Jobs.AddRangeAsync(job, cancellationToken);
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
                    j.Title,
                    j.Location,
                    j.applicaitonLink,
                    j.Description,
                    j.Requirements,
                    j.Benefits
                ))
                .SingleAsync();
             
            if(response is null )
                return Result.Failure<GetJobDataResponse>(CompanyErrors.JobNotFound);
            return Result.Success(response);
            
        }
    }
}
