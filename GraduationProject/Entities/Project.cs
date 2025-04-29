using System.ComponentModel.DataAnnotations.Schema;

namespace GraduationProject.Entities
{
    public class Project
    {
        public int Id { get; set; }
        public User User { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }
        public string Link { get; set; }

    }
}

