namespace GraduationProject.Contracts.Authentication;

public class ReSendConfirmationEmailValidator : AbstractValidator<ReSendConfirmationEmail>
{
    public ReSendConfirmationEmailValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress();
            

    }
}