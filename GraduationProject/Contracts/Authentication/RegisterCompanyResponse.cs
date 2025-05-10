namespace GraduationProject.Contracts.Authentication
{
    public record RegisterCompanyResponse
    (
        string Email,
        string UserName,
        string Location
    );
}
