namespace GraduationProject.Entities
{
    public class Job
    {
        public int Id { get; set; }
        
        public string Title { get; set; }
        public string Level { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Description { get; set; }
        public string Benefits { get; set; }
        ICollection<string> Skills { get; set; } = new List<string>();
        public ICollection<User> Users { get; set; }

    }
}
