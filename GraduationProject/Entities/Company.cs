namespace GraduationProject.Entities
{
    public class Company
    {
        public int Id { get; set; }
        public string  Name { get; set; }
        public string Address { get; set; }
        public ICollection<Job> Jobs { get; set; } = new List<Job>();
    }
}
