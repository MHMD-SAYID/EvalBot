namespace GraduationProject.Contracts.Users.Add
{
    public record AddProjectRequest
    (
        string Id,
        List<projectRegister> project
    );
}
