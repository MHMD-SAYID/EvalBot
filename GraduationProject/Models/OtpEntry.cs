namespace GraduationProject.Models
{
    public class OtpEntry
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string OtpCode { get; set; }
        public DateTime ExpiryTime { get; set; }
    }

}
