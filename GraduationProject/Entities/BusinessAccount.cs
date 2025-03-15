// not finished yet

namespace GraduationProject.Entities
{
    public class BusinessAccount
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Type { get; set; }
        public string Link { get; set; }
        public User User { get; set; }
    }
}
