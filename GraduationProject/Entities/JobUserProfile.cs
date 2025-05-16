namespace GraduationProject.Entities
{
    public class JobUserProfile
    {
        public int Id { get; set; }
        public string userProfileId { get; set; }
        public int jobId { get; set; }
        public UserProfile userProfile { get; set; }
        public Job job { get; set; }
    }
}
