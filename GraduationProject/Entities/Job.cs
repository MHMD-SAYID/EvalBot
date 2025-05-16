namespace GraduationProject.Entities
{
    public class Job
    {
        public int Id { get; set; }
        public string applicaitonLink { get; set; }
        public string Location { get; set; }
        public string Requirements { get; set; }
        public string Title { get; set; }
        //public string Level { get; set; }
        public DateOnly ReleaseDate { get; set; } = DateOnly.FromDateTime(DateTime.Now);
        public string Description { get; set; }
        public string Benefits { get; set; }
        //ICollection<string> Skills { get; set; } = new List<string>();
        public string companyProfileId { get; set; }
        public CompanyProfile Company { get; set; }
        public ICollection<JobUserProfile> jobUserProfiles { get; set; }=new List<JobUserProfile>();

    }
}
