using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace GraduationProject.DTO
{
    public class RegisterDTO
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        //[Phone]
        //public string PhoneNumber { get; set; }
        //public int Gender { get; set; }
        //public DateTime DateOfBirth { get; set; }
    }
}
