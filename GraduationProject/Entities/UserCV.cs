namespace GraduationProject.Entities
{
    public class UserCV
    {
        public int Id { get; set; }
        public string userId { get; set; }
        public string  Extension { get; set; }
        public string  RealPath { get; set; }
        public string  HostedPath { get; set; }
        public User user { get; set; }
    }
}
