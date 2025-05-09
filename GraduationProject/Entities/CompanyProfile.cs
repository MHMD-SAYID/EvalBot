using System.ComponentModel.DataAnnotations;

namespace GraduationProject.Entities
{
    public class CompanyProfile
    {
        public string  Name { get; set; }
        public string Address { get; set; }
        public ICollection<Job> Jobs { get; set; } = new List<Job>();
        [Key]
        public string userId { get; set; }
        public User user { get; set; }
    }
}
