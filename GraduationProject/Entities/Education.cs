// not finished yet

namespace GraduationProject.Entities
{
    public class Education
    {
        public int Id { get; set; }
        public string Institution { get; set; }
        public string  Degree { get; set; }
        public string FieldOfStudy { get; set; }
        public bool IsUnderGraduate { get; set; }
        public long StartDate { get; set; }
        public long EndDate { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }


    }
}
