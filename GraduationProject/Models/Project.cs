namespace GraduationProject.Models
{
    public class project
    {
        public int Id { get; set; }
        public int CVId { get; set; }
        public CV cv { get; set; }
        public string Description { get; set; }
        public string GitHupLink { get; set; }
        // Datetime -> string
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        // added title
        public string Title { get; set; }

    }
}

