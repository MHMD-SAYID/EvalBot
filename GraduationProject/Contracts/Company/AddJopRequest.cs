namespace GraduationProject.Contracts.Company
{
    public record AddJopRequest
    (
        string companyId,
        List<jobData> Jobs
    );
}
