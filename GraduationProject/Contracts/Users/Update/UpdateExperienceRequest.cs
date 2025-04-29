namespace GraduationProject.Contracts.Users.Update
{
    public record UpdateExperienceRequest
    (
        int id,
        string JobTitle,
        string CompanyName,
        bool StillWorkingThere,
        long StartDate,
        long EndDate
    );
}
