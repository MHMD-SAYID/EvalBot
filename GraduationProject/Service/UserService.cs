using GraduationProject.Contracts.Users;
using GraduationProject.Contracts.Users.Add;
using GraduationProject.Contracts.Users.Delete;
using GraduationProject.Contracts.Users.Update;


namespace GraduationProject.Service
{
    public class UserService(IWebHostEnvironment webHostEnvironment, UserManager<User> userManager,AppDbContext context) : IUserService
    {
        private readonly AppDbContext _context=context;
        private readonly UserManager<User> _userManager=userManager;
        private readonly string _imagesPath = $"{webHostEnvironment.WebRootPath}/Images";
        private readonly string _FilePath = $"{webHostEnvironment.WebRootPath}/CV";

        public async Task<Result> AddBusinessAcount(AddBusinessAccountRequest request, CancellationToken cancellationToken)
        {
            var user = await _userManager.Users
                .Include(x => x.businessAccounts)
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
            var account = new Entities.BusinessAccount
            {
                Type = request.Type,
                Link = request.Link
            };
            user.businessAccounts.Add(account);
            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                return Result.Failure(UserErrors.InternalServerError);
            }
            return Result.Success();
        }

        public async Task<Result> AddEducation(AddEducationRequest request, CancellationToken cancellationToken)
        {
            var user = await _userManager.Users
               .Include(x => x.Education)
               .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
            var education = new Entities.Education
            {
               
                Degree=request.Degree,
                FieldOfStudy=request.FieldOfStudy,
                StartDate=request.StartDate,
                EndDate=request.EndDate,
                Institution = request.Institution,
                IsUnderGraduate = request.IsUnderGraduate
       
            };
            user.Education.Add(education);

            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                return Result.Failure(UserErrors.InternalServerError);
            }
            return Result.Success();
        }

        public async Task<Result> AddExperience(AddExperienceRequest request, CancellationToken cancellationToken)
        {
           var user= await _userManager.Users
                .Include(x=>x.Experience)
                .FirstOrDefaultAsync(x=>x.Id==request.Id,cancellationToken);
            var experience = new Entities.Experience
            {
                CompanyName = request.CompanyName,
                JobTitle = request.JobTitle,
                StillWorkingThere = request.StillWorkingThere,
                EndDate = request.EndDate,
                StartDate = request.StartDate
            };
            user.Experience.Add(experience);

            var result =await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                return Result.Failure(UserErrors.InternalServerError);
            }
            return Result.Success();
        }

        public async Task<Result> AddProject(AddProjectRequest request, CancellationToken cancellationToken)
        {
            var user = await _userManager.Users
               .Include(x => x.Projects)
               .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
            var project = new Entities.Project
            {
                Link=request.link,
                Name=request.name
            };
            user.Projects.Add(project);

            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                return Result.Failure(UserErrors.InternalServerError);
            }
            return Result.Success();
        }
        
        public async Task<Result> DeleteBusinessAccountLink(DeleteRequest request, CancellationToken cancellationToken)
        {
            var account = await _context.businessAccounts
                .FirstOrDefaultAsync(e => e.Id == request.Id && e.UserId == request.userId, cancellationToken);

            if (account is null) { return Result.Failure(UserErrors.AccountNotFound); }

            _context.businessAccounts.Remove(account);
            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
        

        public async Task<Result> DeleteEducation(DeleteRequest request, CancellationToken cancellationToken)
        {
            var education = await _context.Education
            .FirstOrDefaultAsync(e => e.Id == request.Id && e.UserId == request.userId, cancellationToken);

            if(education is null) {return Result.Failure(UserErrors.EducationNotFound); }

            _context.Education.Remove(education);
            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success();

        }

        public async Task<Result> DeleteExperience(DeleteRequest request, CancellationToken cancellationToken)
        {
            var experience = await _context.Experience
            .FirstOrDefaultAsync(e => e.Id == request.Id && e.UserId == request.userId, cancellationToken);

            if (experience is null) { return Result.Failure(UserErrors.ExperienceNotFound); }

            _context.Experience.Remove(experience);
            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }

        public async Task<Result> DeleteProject(DeleteRequest request, CancellationToken cancellationToken=default)
        {
           
                var project = await _context.Projects
                    .FirstOrDefaultAsync(e => e.Id == request.Id && e.UserId == request.userId, cancellationToken);

                if (project is null) { return Result.Failure(UserErrors.ProjectNotFound); }

                _context.Projects.Remove(project);
                await _context.SaveChangesAsync(cancellationToken);

                return Result.Failure(UserErrors.EmailNotFound);
            
                
            
        }

        public  async Task<Result<UserProfileResponse>> GetProfileAsync(string userId)
        {

            var cvName = await _userManager.Users
               .Include(u => u.uploadedFiles)
               .Where(u => u.Id == userId)
               .Select(u => u.uploadedFiles)
               .Select(f => f.StoredFileName)
               .FirstOrDefaultAsync();
            var path = Path.Combine(_FilePath, cvName);
            var user = await _userManager.Users
     .Where(x => x.Id == userId)
     .Select(x => new UserProfileResponse
     {
         Email = x.Email,
         UserName = x.UserName,
         Skills = x.Skills.ToList(),
         Projects = x.Projects.Select(p => new project { id = p.Id, name = p.Name, link = p.Link }).ToList(),
         FirstLanguage = x.FirstLanguage,
         FirstLanguageLevel = x.FirstLanguageLevel,
         SecondLanguage = x.SecondLanguage,
         SecondLanguageLevel = x.SecondLanguageLevel,
         Bio = x.Bio,
         ProfilePicUrl = "",
         Experience = x.Experience.Select(e => new experience { id = e.Id, CompanyName = e.CompanyName, JobTitle = e.JobTitle, StillWorkingThere = e.StillWorkingThere }).ToList(),
         Education = x.Education.Select(e => new education { id = e.Id, Degree = e.Degree, FieldOfStudy = e.FieldOfStudy, Institution = e.Institution, IsUnderGraduate = e.IsUnderGraduate }).ToList(),
         Accounts = x.businessAccounts.Select(b => new businessAccount { id = b.Id, AccountLink = b.Link, AccountType = b.Type }).ToList(),
         CVUrl =path
     })
     .SingleAsync();
           

            user.ProfilePicUrl = user.ProfilePicUrl = Path.Combine(_imagesPath, user.UserName);

            return Result.Success(user);
        }

        public async Task<Result> UpdateBio(UpdateBioRequest request,CancellationToken cancellationToken )
        {
            var user= await _userManager.Users
                .Where(x=>x.Id==request.Id)
                .SingleOrDefaultAsync();
            if (user == null)
            {
                return Result.Failure(UserErrors.EmailNotFound);
            }
            user.Bio = request.Bio;
            var result= await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                return Result.Failure(UserErrors.InternalServerError);
            }
            return Result.Success(result);
            
        }

    }
}
