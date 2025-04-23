namespace GraduationProject.Contracts.Users.Add
{
    public record AddEducationRequest
    (
        string Id,
        string Institution,
        string Degree,
        string FieldOfStudy,
        bool IsUnderGraduate,
        long StartDate,
        long EndDate
    );
}
