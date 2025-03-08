using Microsoft.AspNetCore.Identity;

namespace GraduationProject.Models
{
    public partial class User:IdentityUser
    {
        public string? IDCard { get; set; }
        public Gender gender { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public ICollection<Interview> interviews { get; set; } = new List<Interview>();//list int score gender list<string> skills project
        public Role role { get; set; }
        public ICollection<Job> Jobs { get; set; }
        public List<RefreshToken> RefreshTokens { get; set; } = [];
        public string CountryOfResidence { get; set; }
        public string EmailType { get; set; }
        public ICollection<string> Skills { get; set; } = new List<string>();
        public double ExpectedSalary { get; set; }
        public string Nationality { get; set; }
        public string FirstLanguage { get; set; }
        public string FirstLanguageLevel { get; set; }

        public string SecondLanguage { get; set; }
        public string SecondLanguageLevel { get; set; }
        //public List<UserLanguage> userLanguages { get; set; }
        public List<BusinessAccount> businessAccounts { get; set; }
    }
}
