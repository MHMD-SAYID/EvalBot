using System.ComponentModel.DataAnnotations;

namespace GraduationProject.Entities
{
    public class UserProfile
    {
    
        public User user { get; set; }
        [Key]
        public string userId { get; set; }
        public string? IDCard { get; set; }
        public Gender gender { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string CountryOfResidence { get; set; }
        public string EmailType { get; set; }
        public double ExpectedSalary { get; set; }
        public int YearsOfExperience { get; set; }
        public string Nationality { get; set; }
        public string? Bio { get; set; }
        public ICollection<string> Skills { get; set; } = new List<string>();
        public ICollection<Education> Education { get; set; } = new List<Education>();
        public ICollection<Experience> Experience { get; set; } = new List<Experience>();
        public ICollection<Project> Projects { get; set; } = new List<Project>();
        public ICollection<Language> languages { get; set; } = new List<Language>();
        public ICollection<BusinessAccount> businessAccounts { get; set; } = new List<BusinessAccount>();
        public ICollection<Interview>? interviews { get; set; } = new List<Interview>();//list int score gender list<string> skills project
        //public UserImage Image { get; set; }
        public UserCV? CV { get; set; }
        //public ICollection<Job>? Jobs { get; set; }
        public ICollection<JobUserProfile> jobUserProfiles { get; set; } = new List<JobUserProfile>();

    }
}
