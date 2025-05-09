namespace GraduationProject.Entities
{
    public class UserImage
    {
        public int Id { get; set; }
        public string userProfileId { get; set; }
        public string Extension { get; set; }
        public string RealPath { get; set; }
        public string HostedPath { get; set; }
        public UserProfile userProfile { get; set; }
    }
}
