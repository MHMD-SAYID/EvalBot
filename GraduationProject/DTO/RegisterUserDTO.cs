using GraduationProject.Models;

namespace GraduationProject.DTO
{
    public class RegisterUserDTO
    {
        public string Email { get; set; }
        public string Otp { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public Gender Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
    }

}
