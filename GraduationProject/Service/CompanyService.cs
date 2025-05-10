
using GraduationProject.Contracts.Company;
using GraduationProject.Contracts.Users;

namespace GraduationProject.Service
{
    public class CompanyService(AppDbContext context) : ICompanyService
    {
        private readonly AppDbContext _context = context;

       

        public async Task<Result<CompanyProfileResponse>> GetCompanyProfileAsync(string companyId)
        {
            var Imagepath = await _context.UserImage
                .Where(x => x.companyProfileId == companyId)
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
                        ? x.Jobs.Select(p => new jobsProfile(p.Title, p.ReleaseDate)).ToList()
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
    }
}
