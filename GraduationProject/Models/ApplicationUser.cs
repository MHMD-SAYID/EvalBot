using Microsoft.AspNetCore.Identity;

namespace GraduationProject.Models
{
    public class ApplicationUser:IdentityUser
    {
        public string? IDCard { get; set; }
        public Gender gender { get; set; }
        public string? LinkedIn { get; set; }
        // in db -->DATETIMEOFFSET ---> 2024-12-18 13:30:00.1234567 +03:00
        public DateTimeOffset CreatedAt { get; set; }
        //public int CVId { get; set; }
        //public CV cv { get; set; }
        //ICollection<T> is better than IList<T> -->has more flexibility
        ICollection<CV> cVs { get; set; }
        public ICollection<Interview> interviews { get; set; } = new List<Interview>();//list int score gender list<string> skills project
        public Role role { get; set; }
        public ICollection<Job> Jobs { get; set; }
    }
}
