


namespace GraduationProject.Models
{

    public class CV
    {
        public int Id { get; set; }
        //
        public int UserId { get; set; }
        public string Title { get; set; }
        public ICollection<Skills> skills { get; set; } = new List<Skills>();
        public ICollection<project> projects { get; set; } = new List<project>();
        public ICollection<Experience> experience { get; set; } = new List<Experience>();
        public ICollection<Certification> certifications { get; set; } = new List<Certification>();
        public ApplicationUser User { get; set; }
        
    }
}
