using GraduationProject.Entites;

namespace GraduationProject.Entites
{
    public class Experience
    {
        public int Id { get; set; }
        //PlaceName-->JobTitle
        public string JobTitle { get; set; }
        // add( CompanyName , StartDate , EndDate)
        public string CompanyName { get; set; }
        public string StartDate { get; set; }
        public string EndData { get; set; }
        public string Description { get; set; }
        public int CVId { get; set; } // new 
        public CV cv { get; set; } // new
    }
}
