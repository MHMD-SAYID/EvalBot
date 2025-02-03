using FluentValidation;
using System.ComponentModel.DataAnnotations;

namespace GraduationProject.Contracts.Authentication
{
    public class LoginValidator: AbstractValidator<LoginRequest>
    {
        public LoginValidator()
        {
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.Password).NotEmpty();
        }
    }
}
