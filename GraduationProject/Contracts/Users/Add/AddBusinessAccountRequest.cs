namespace GraduationProject.Contracts.Users.Add
{
    public record AddBusinessAccountRequest
    (
        string Id,
        List<businessAccountRegister> businessAccounts
    );
}
