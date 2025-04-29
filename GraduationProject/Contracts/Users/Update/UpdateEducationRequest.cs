namespace GraduationProject.Contracts.Users.Update
{
    public record UpdateEducationRequest
    (

        int id,
        string Institution,
        string Degree,
        string FieldOfStudy,
        bool IsUnderGraduate,
        long StartDate,
        long EndDate

    );
}
