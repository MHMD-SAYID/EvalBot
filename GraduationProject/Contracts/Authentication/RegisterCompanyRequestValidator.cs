
namespace GraduationProject.Contracts.Authentication;

public class RegisterCompanyRequestValdiator : AbstractValidator<RegisterCompanyRequest>
{
    public RegisterCompanyRequestValdiator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress();

        RuleFor(x => x.Password)
            .NotEmpty();
        
       
    }
}
