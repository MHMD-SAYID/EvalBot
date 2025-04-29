namespace GraduationProject.Contracts.Users.Update
{
    public record UpdateBusinessAccountRequest
    (
        int id,
        string AccountType,
        string AccountLink
    );
}
