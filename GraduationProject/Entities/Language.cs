namespace GraduationProject.Entities
{
    public class Language
    {
        public int Id { get; set; }
        public string userProfileId { get; set; }
        public string Name { get; set; }
        public string Level { get; set; }
        public UserProfile userProfile { get; set; }
    }
}
