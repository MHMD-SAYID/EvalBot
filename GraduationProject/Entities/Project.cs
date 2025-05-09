using System.ComponentModel.DataAnnotations.Schema;

namespace GraduationProject.Entities
{
    public class Project
    {
        public int Id { get; set; }
        public string userProfileId { get; set; }
        public UserProfile userProfile { get; set; }
        public string Name { get; set; }
        public string Link { get; set; }

    }
}

