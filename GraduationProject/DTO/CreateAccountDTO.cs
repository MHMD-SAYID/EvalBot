using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GraduationProject.DTO
{
    public class CreateAccountDTO
    {
        public string FullName { get; set; }
        [PasswordPropertyText]
        public string Password { get; set; }
        [Phone]
        public string PhoneNumber { get; set; }
        public int Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
