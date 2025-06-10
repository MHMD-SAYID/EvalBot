namespace GraduationProject.Contracts.Company
{
    public record AddJopRequest
    (
        string companyId,
        string Title,
        string track,
        int questionNumber,
        string difficulty,
        string Location,
        string Description,
        string Requirements,
        string Benefits
    //List<jobData> Jobs
    );
}
