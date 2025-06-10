namespace GraduationProject.Contracts.Company
{
    public record GetJobDataResponse
    (
        int Id,
        string imageUrl,
        string Title,
        string Location,
        string Description,
        string Requirements,
        string Benefits,
        string track,
        int questionNumber,
        string difficulty

    );
}
