

using Microsoft.IdentityModel.Tokens;

namespace GraduationProject.Models
{
    public class Experience
    {
        public int Id { get; set; }
        
        public string UserId { get; set; }
        public string JobTitle { get; set; }
        public string CompanyName { get; set; }
        public bool StillWorkingThere { get; set; }
        public long StartDate { get; set; }
        public long? EndDate { get; set; }
        public User User { get; set; } 
    }
}
