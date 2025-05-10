namespace GraduationProject.Contracts.Company
{
    public record AddJopRequest
    (
        string companyId,
        List<AddJob> Jobs
    );
    public record AddJob
        (
        string Title,
        string Location,
        string applicaitonLink,
        string Description,
        string Requirements,
        string Benefits
        );
}
