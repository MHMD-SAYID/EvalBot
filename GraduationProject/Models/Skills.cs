namespace GraduationProject.Entites
{
    public class Skills
    {
        public int Id { get; set; }
        public int CVId { get; set; }
        public CV cv { get; set; }
        public ICollection<string> TechnicalSkills { get; set; } = new List<string>();
        public ICollection<string> SoftSkills { get; set; } = new List<string>();
    }
}
