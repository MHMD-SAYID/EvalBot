namespace GraduationProject.Contracts.Users.Add
{
    public record AddEducationRequest
    (
        string Id,
        List<educationRegister>education
    );
}
