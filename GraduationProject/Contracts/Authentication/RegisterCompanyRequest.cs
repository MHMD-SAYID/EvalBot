namespace GraduationProject.Contracts.Authentication
{
    public record RegisterCompanyRequest
    (
        string Email,
        string Password,
        string UserName,
        string Location
        
    );
}
