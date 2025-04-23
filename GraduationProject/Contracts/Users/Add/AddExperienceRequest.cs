namespace GraduationProject.Contracts.Users.Add
{
    public record AddExperienceRequest
    (
        string Id,
        string JobTitle,
        string CompanyName,
        bool StillWorkingThere,
        long EndDate,
        long StartDate
    );
}
