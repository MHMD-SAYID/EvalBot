namespace GraduationProject.Contracts.Users.Add
{
    public record AddLanguagesRequest
    (
        string userId,
        List<LanguageRegister>languages

    );
}
