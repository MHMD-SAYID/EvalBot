

using Microsoft.IdentityModel.Tokens;

namespace GraduationProject.Entities
{
    public class Experience
    {
        public int Id { get; set; }
        
        public string userProfileId { get; set; }
        public string JobTitle { get; set; }
        public string CompanyName { get; set; }
        public bool StillWorkingThere { get; set; }
        public long StartDate { get; set; }
        public long? EndDate { get; set; }
        public UserProfile userProfile { get; set; } 
    }
}
