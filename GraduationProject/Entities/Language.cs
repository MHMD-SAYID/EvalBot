namespace GraduationProject.Entities
{
    public class Language
    {
        public int Id { get; set; }
        public string userId { get; set; }
        public string Name { get; set; }
        public string Level { get; set; }
        public User user { get; set; }
    }
}
