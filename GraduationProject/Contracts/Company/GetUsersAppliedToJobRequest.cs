namespace GraduationProject.Contracts.Company
{
    public record GetUsersAppliedToJobRequest
    (
        string companyId,
        int jobId
    );
   
}
