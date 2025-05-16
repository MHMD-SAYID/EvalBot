namespace GraduationProject.Contracts.Company
{
    public record GetJobDataResponse
    (
        int Id,
        string Title,
        string Location,
        string applicaitonLink,
        string Description,
        string Requirements,
        string Benefits
    );
}
