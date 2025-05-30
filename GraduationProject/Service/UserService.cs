using AutoMapper;
using GraduationProject.Contracts.Users;
using GraduationProject.Contracts.Users.Add;
using GraduationProject.Contracts.Users.Delete;
using GraduationProject.Contracts.Users.Interview;
using GraduationProject.Contracts.Users.Update;
using GraduationProject.Entities;
using System.IO;




namespace GraduationProject.Service
{
    public class UserService(IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor
        , UserManager<User> userManager,AppDbContext context) : IUserService
    {
        private readonly AppDbContext _context=context;
        private readonly UserManager<User> _userManager=userManager;
        private readonly IHttpContextAccessor _httpContextAccessor= httpContextAccessor;
        private readonly string _imagesPath = $"{webHostEnvironment.WebRootPath}/Images";
        private readonly string _FilePath = $"{webHostEnvironment.WebRootPath}/CV";

        public async Task<Result<UserProfileResponse>> GetProfileAsync(string userId)
        {

            //var cvName = await _userManager.Users
            //   .Include(u => u.uploadedFiles)
            //   .Where(u => u.Id == userId)
            //   .Select(u => u.uploadedFiles)
            //   .Select(f => f.StoredFileName)
            //   .FirstOrDefaultAsync();
            //var path = !string.IsNullOrEmpty(cvName) ? Path.Combine(_FilePath, cvName) : null;
            var cvpath = await _context.UserCV
                .Where(x => x.userProfileId == userId)
                .Select(x => x.HostedPath)
                .FirstOrDefaultAsync();
            var Imagepath = await _context.UserImage
                .Where(x => x.userId == userId)
                .Select(x => x.HostedPath)
                .FirstOrDefaultAsync();
            //vFirstOrDefaultAsync(x=>x.userId == userId);
            var user = await _context.UserProfile
     .Where(x => x.userId == userId)
     .Select(x => new UserProfileResponse
     {
         Email = x.user.Email!,
         UserName = x.user.UserName!,
         Skills = x.Skills.ToList(),
         Projects = x.Projects != null && x.Projects.Any()
            ? x.Projects.Select(p => new projectProfile { id = p.Id, name = p.Name, link = p.Link }).ToList()
            : null,
         Experience = x.Experience != null && x.Experience.Any()
            ? x.Experience.Select(e => new experienceProfile { id = e.Id, CompanyName = e.CompanyName, JobTitle = e.JobTitle, StillWorkingThere = e.StillWorkingThere }).ToList()
            : null,
         Education = x.Education != null && x.Education.Any()
            ? x.Education.Select(e => new educationProfile { id = e.Id, Degree = e.Degree, FieldOfStudy = e.FieldOfStudy, Institution = e.Institution, IsUnderGraduate = e.IsUnderGraduate }).ToList()
            : null,
         Accounts = x.businessAccounts != null && x.businessAccounts.Any()
            ? x.businessAccounts.Select(b => new businessAccountProfile { id = b.Id, AccountLink = b.Link, AccountType = b.Type }).ToList()
            : null,
         Languages = x.languages != null && x.languages.Any()
            ? x.languages.Select(b => new languageProfile { id = b.Id, name = b.Name, lavel = b.Level }).ToList()
            : null,

         Bio = x.Bio,
         ProfilePicUrl = Imagepath,
         CVUrl = cvpath
     })
     .SingleAsync();

            //var imageUrl = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}/Images/{user.UserName}";
            //user.ProfilePicUrl = File.Exists(imageUrl) ? imageUrl : null;

            //var imagePath =user.ProfilePicUrl;
            //user.ProfilePicUrl = File.Exists(imagePath) ? imagePath : null;

            return Result.Success(user);
        }
        public async Task<Result> AddBusinessAcount(AddBusinessAccountRequest request, CancellationToken cancellationToken)
        {

            if (request.businessAccounts?.Any() != true)
                return Result.Success();
            var account = request.businessAccounts.Select(p => new BusinessAccount
            {
                Link=p.AccountLink,
                Type=p.AccountType,
                userProfileId = request.Id
                

            }).ToList();
            await _context.businessAccounts.AddRangeAsync(account, cancellationToken);
            var result = await _context.SaveChangesAsync(cancellationToken);

            return result > 0 ? Result.Success() : Result.Failure(PorfileErrors.AccountNotFound);
        }
        public async Task<Result> AddEducation(AddEducationRequest request, CancellationToken cancellationToken)
        {
            if (request.education?.Any() != true)
                return Result.Success();
            var education = request.education.Select(p => new Education
            {
                FieldOfStudy = p.FieldOfStudy,
                Institution = p.Institution,
                StartDate = p.StartDate,
                Degree = p.Degree,
                EndDate = p.EndDate,
                userProfileId = request.Id


            }).ToList();
            await _context.Education.AddRangeAsync(education, cancellationToken);
            var result = await _context.SaveChangesAsync(cancellationToken);

            return result > 0 ? Result.Success() : Result.Failure(PorfileErrors.EducationNotFound);

        }

        public async Task<Result> AddExperience(AddExperienceRequest request, CancellationToken cancellationToken)
        {
            {// var user= await _userManager.Users
             //      .Include(x=>x.Experience)
             //      .FirstOrDefaultAsync(x=>x.Id==request.Id,cancellationToken);
             //  var experience = new Entities.Experience
             //  {
             //      CompanyName = request.CompanyName,
             //      JobTitle = request.JobTitle,
             //      StillWorkingThere = request.StillWorkingThere,
             //      EndDate = request.EndDate,
             //      StartDate = request.StartDate
             //  };
             //  user.Experience.Add(experience);

                //  var result =await _userManager.UpdateAsync(user);

                //  if (!result.Succeeded)
                //  {
                //      return Result.Failure(UserErrors.InternalServerError);
                //  }
                //      return Result.Success();
            }

            if (request.experience?.Any() != true)
                return Result.Success();

            var experience = request.experience.Select(p => new Experience
            {
                CompanyName = p.CompanyName,
                JobTitle = p.JobTitle,
                StartDate = p.StartDate,
                EndDate = p.EndDate,
                StillWorkingThere = p.StillWorkingThere,
                userProfileId = request.Id
            }).ToList();

            await _context.Experience.AddRangeAsync(experience, cancellationToken);
            var result = await _context.SaveChangesAsync(cancellationToken);

            return result>0 ? Result.Success() : Result.Failure(PorfileErrors.ExperienceNotFound);   

        
        }


        public async Task<Result> AddProject(AddProjectRequest request, CancellationToken cancellationToken)
        {
            if (request.project?.Any() != true)
                return Result.Success();
            var projects = request.project.Select(p => new Project
            {
                Link = p.Link,
                Name = p.Name,
                userProfileId = request.Id
            }).ToList();
            await _context.Projects.AddRangeAsync(projects, cancellationToken);
            var result = await _context.SaveChangesAsync(cancellationToken);

            return result > 0 ? Result.Success() : Result.Failure(PorfileErrors.ProjectNotFound);
        }
        public async Task<Result> DeleteBusinessAccountLink(DeleteRequest request, CancellationToken cancellationToken)
        {
            var account = await _context.businessAccounts
                .FirstOrDefaultAsync(e => e.Id == request.Id && e.userProfileId == request.userId, cancellationToken);

            if (account is null) { return Result.Failure(PorfileErrors.AccountNotFound); }

            _context.businessAccounts.Remove(account);
            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
        

        public async Task<Result> DeleteEducation(DeleteRequest request, CancellationToken cancellationToken)
        {
            var education = await _context.Education
            .FirstOrDefaultAsync(e => e.Id == request.Id && e.userProfileId == request.userId, cancellationToken);

            if(education is null) {return Result.Failure(PorfileErrors.EducationNotFound); }

            _context.Education.Remove(education);
            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success();

        }

        public async Task<Result> DeleteExperience(DeleteRequest request, CancellationToken cancellationToken)
        {
            var experience = await _context.Experience.Where(e => e.Id == request.Id && e.userProfileId == request.userId)
            .FirstOrDefaultAsync (cancellationToken);

            if (experience is null) { return Result.Failure(PorfileErrors.ExperienceNotFound); }

            _context.Experience.Remove(experience);
            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }

        public async Task<Result> DeleteProject(DeleteRequest request, CancellationToken cancellationToken=default)
        {
           
                var project = await _context.Projects
                    .FirstOrDefaultAsync(e => e.Id == request.Id && e.userProfileId == request.userId, cancellationToken);

                if (project is null) { return Result.Failure(PorfileErrors.ProjectNotFound); }

                _context.Projects.Remove(project);
                await _context.SaveChangesAsync(cancellationToken);

                return Result.Success();
            
                
            
        }

        

        public async Task<Result> UpdateBio(UpdateBioRequest request,CancellationToken cancellationToken )
        {
            var user =await  _context.UserProfile.Where(x => x.userId == request.Id).FirstOrDefaultAsync();
            //var user= await _userManager.Users
            //    .Where(x=>x.Id==request.Id)
            //    .SingleOrDefaultAsync();
            if (user == null)
            {
                return Result.Failure(UserErrors.EmailNotFound);
            }
            user.Bio = request.Bio;
           _context.Update(user);
            var result = await _context.SaveChangesAsync(cancellationToken);

            //var result= await _userManager.UpdateAsync(user);
            return result > 0 ?
                 Result.Success(result) :
                 Result.Failure(UserErrors.InternalServerError);

            //if (!result)
            //{
            //    return Result.Failure(UserErrors.InternalServerError);
            //}
            //return Result.Success(result);
            
        }

        public async Task<Result> DeleteAccount(string userId, CancellationToken cancellationToken)
        {
            var userman = await _userManager.Users
                    .FirstOrDefaultAsync(x => x.Id == userId, cancellationToken);
            var user = await _context.UserProfile.Where(x => x.userId == userId).SingleOrDefaultAsync();

            if (user is null)
            {
                return Result.Failure(PorfileErrors.AccountNotFound);
            }
            var cvpath = await _context.UserCV
                .Where(x => x.userProfileId == userman.Id)
                .Select(x => x.RealPath)
                .FirstOrDefaultAsync();
            var Imagepath = await _context.UserImage
                //.Where(x => x.userId == userman.Id)
                .Select(x => x.RealPath)
                .FirstOrDefaultAsync();
            if (System.IO.File.Exists(cvpath))
            {

                System.IO.File.Delete(cvpath);
            }
            if (System.IO.File.Exists(Imagepath))
            {

                System.IO.File.Delete(Imagepath);
            }
            var result = await _userManager.DeleteAsync(userman);

            if (!result.Succeeded)
            {
                return Result.Failure(PorfileErrors.AccountNotFound);
            }

            return Result.Success();
        }

        public async Task<Result> UpdateSkills(UpdateSkillsRequest request, CancellationToken cancellationToken)
        {
            var user = await _context.UserProfile.Where(x => x.userId == request.userId).SingleOrDefaultAsync();

            //var user = await _userManager.Users.FirstOrDefaultAsync(x=>x.Id==request.userId,cancellationToken);
            user.Skills = request.Skills;
            _context.Update(user);
            var result=await _context.SaveChangesAsync(cancellationToken);
            //var result =await _userManager.UpdateAsync(user);
            return result > 0 ? Result.Success() : Result.Failure(PorfileErrors.InvalidSkillsUpdate);
            //if (!result.Succeeded)
            //{ 
            //    return Result.Failure(PorfileErrors.InvalidSkillsUpdate);
            //}
            //return Result.Success();

        }

        public async Task<Result> UpdateExperience(UpdateExperienceRequest request, CancellationToken cancellationToken)
        {

            var experience = await _context.Experience.FindAsync(request.id) ;
            if (experience is null)
            {
                return Result.Failure(PorfileErrors.ExperienceNotFound);
            }
            experience.CompanyName=request.CompanyName;
            experience.StillWorkingThere = request.StillWorkingThere;
            experience.JobTitle=request.JobTitle;
            experience.StartDate=request.StartDate;
            experience.EndDate=request.EndDate;
                 
            var result = await _context.SaveChangesAsync();
            return Result.Success() ;
        }

        public async Task<Result> UpdateEducation(UpdateEducationRequest request, CancellationToken cancellationToken)
        {
           
            var education = await _context.Education.Where(x => x.Id == request.id).FirstOrDefaultAsync();
            if (education is null)
            {
                return Result.Failure(PorfileErrors.ExperienceNotFound);
            }
            education.Institution = request.Institution;
            education.FieldOfStudy = request.FieldOfStudy;
            education.Degree = request.Degree;
            education.StartDate = request.StartDate;
            education.EndDate = request.EndDate;
            education.IsUnderGraduate= request.IsUnderGraduate;
            _context.Update(education);
            var result = await _context.SaveChangesAsync();
            return Result.Success();

        }

        public async Task<Result> UpdateProject(UpdateProjectRequest request, CancellationToken cancellationToken)
        {
            var project = await _context.Projects.Where(x => x.Id == request.id).FirstOrDefaultAsync();
            if (project is null)
            {
                return Result.Failure(PorfileErrors.ExperienceNotFound);
            }
            project.Link = request.link;
            project.Name = request.name;
            _context.Update(project);
            var result = await _context.SaveChangesAsync();
            return Result.Success();
        }

        public async Task<Result> UpdateBusinessAccount(UpdateBusinessAccountRequest request, CancellationToken cancellationToken)
        {
            var account = await _context.businessAccounts.Where(x => x.Id == request.id).FirstOrDefaultAsync();
            if (account is null)
            {
                return Result.Failure(PorfileErrors.ExperienceNotFound);
            }
            account.Type = request.AccountType;
            account.Link = request.AccountLink;
            _context.Update(account);
            var result = await _context.SaveChangesAsync();
            return Result.Success();
        }

        public async Task<Result> UpdateLanguage(UpdateLanguageRequest request, CancellationToken cancellationToken)
        {
            var language = await _context.Language.Where(x => x.Id == request.id).FirstOrDefaultAsync();
            if (language is null)
            {
                return Result.Failure(PorfileErrors.ExperienceNotFound);
            }
            language.Name = request.Language;
            language.Level = request.Level;
            _context.Update(language);
            var result = await _context.SaveChangesAsync();
            return Result.Success();

        }


        public async Task<Result> AddLanguages(AddLanguagesRequest request, CancellationToken cancellationToken)
        {
            //var user = await _userManager.Users
            //    .FirstOrDefaultAsync(x => x.Id == request.userId);
            var user = await _context.UserProfile.Where(x => x.userId == request.userId).SingleOrDefaultAsync();
            if (request.languages?.Any() != true)
                return Result.Success();
            var languages = request.languages.Select(p => new Language
            {
                Name = p.name,
                Level = p.level,
                userProfileId= request.userId
            }).ToList();
            await _context.Language.AddRangeAsync(languages, cancellationToken);
            var result = await _context.SaveChangesAsync(cancellationToken);

            return result > 0 ? Result.Success() : Result.Failure(PorfileErrors.InvalidLangiageAdd);
        }

        public async Task<Result> DeleteLanguages(DeleteRequest request, CancellationToken cancellationToken)
        {
            var language = await _context.Language
           .FirstOrDefaultAsync(e => e.Id == request.Id && e.userProfileId == request.userId, cancellationToken);

            if (language is null) { return Result.Failure(PorfileErrors.LanguageNotFound); }

            _context.Language.Remove(language);
            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }

        

        public async Task<Result<List<GetAllJobsResponse>>> GetAllJobs(CancellationToken cancellationToken)
        {
            var jobs = await _context.Jobs
                .Select(j => new GetAllJobsResponse
                (
                    j.Id,
                    j.Title,
                    j.Company.user.UserName!,
                    j.Location,
                    j.ReleaseDate
                ))
                .ToListAsync();
            return Result.Success(jobs);
            
        }

        public async Task<Result> ApplyToJob(ApplyToJobRequest request, CancellationToken cancellationToken)
        {
            var jobExists = await _context.Jobs
                .AnyAsync(x => x.Id == request.jobId);
            if (!jobExists)
                return Result.Failure(CompanyErrors.JobNotFound);

            var isApplied = await _context.JobUserProfiles
                .AnyAsync(x => x.userProfileId == request.userId && x.jobId == request.jobId);

            if (isApplied)
                return Result.Failure(UserErrors.DuplicatedApply);
            var result = await _context.JobUserProfiles.AddAsync(new JobUserProfile
            {
                userProfileId = request.userId,
                jobId = request.jobId
            }, cancellationToken);
            var response= await _context.SaveChangesAsync();
            return Result.Success();
        }

        public async Task<Result<int>> ConductInterView(CoductInterviewRequest request, CancellationToken cancellationToken)
        {
            var interview = new Interview
            {
                userProfileId = request.userProfileId,
                Topic=request.Topic
                
            };
            _context.Interview.Add(interview);
            var result =await _context.SaveChangesAsync(cancellationToken); 
            return Result.Success(interview.Id);
        }

        public async Task<Result> AddInterViewData(AddInterviewDataRequest request, CancellationToken cancellationToken)
        {
            var interviewExists = await _context.Interview
                .AnyAsync(x => x.Id == request.interviewId, cancellationToken);
            if(!interviewExists)
            { return Result.Failure(UserErrors.InterviewNotFound); }
            var Data = new Q_A
            {
                InterviewId=request.interviewId,
                Topic=request.Topic,
                QuestionNumber = request.questionNumber,
                Question = request.Question,
                Answer=request.Answer,
                userAnswer=request.userAnswer,
                Score=request.Score,
                ScoreExplanation=request.ScoreExplanation,
                Links = request.Links,
            };
            _context.Q_A.Add(Data);
            var result =await _context.SaveChangesAsync(cancellationToken);
            return result > 0 ? Result.Success() : Result.Failure(UserErrors.InterviewDataNotAdded);
        }

        public async Task<Result> AddInterViewVisionData(AddVisionResultRequest request, CancellationToken cancellationToken)
        {
            var interview = await _context.Interview
                .FirstOrDefaultAsync(x => x.Id == request.interviewId, cancellationToken);
            if (interview==null)
            { return Result.Failure(UserErrors.InterviewNotFound); }
            
            {
                interview.AverageConfidenceScore = request.interviewAverageConfidenceScore;
                interview.AverageTensionScore = request.interviewAverageTensionScore;
                interview.videoPath = request.VideoPath;
                interview.Warnings = request.Warnings;
                interview.IsCompleted = request.isCompleted;
                interview.CheatTimes = request.cheatTimes;
            }
            var vision = new Q_AVisionResult
            {
                questionNumber = request.questionNumber,
                interviewId = request.interviewId,
                AverageConfidenceScore=request.averageConfidenceScore,
                AverageTensionScore=request.averageTensionScore,
                
            };
            _context.Interview.Update(interview);
            _context.Q_AVisionResults.Add(vision);
            var result = await _context.SaveChangesAsync(cancellationToken);    
            return result>0? Result.Success() : Result.Failure(UserErrors.InterviewVisionDataNotAdded);
        }
    }
}
