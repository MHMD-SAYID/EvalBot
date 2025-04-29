namespace GraduationProject.Contracts.Users.Update
{
    public record UpdateSkillsRequest
    (
        
        string userId,
        List<string>Skills
    );
    
}
