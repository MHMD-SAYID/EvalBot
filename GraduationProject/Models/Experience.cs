

namespace GraduationProject.Models
{
    public class Experience
    {
        public int Id { get; set; }
        //PlaceName-->JobTitle
        public string JobTitle { get; set; }
        // add( CompanyName , StartDate , EndDate)
        public string CompanyName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Description { get; set; }
        public int CVId { get; set; } // new 
        public CV cv { get; set; } // new
    }
}
