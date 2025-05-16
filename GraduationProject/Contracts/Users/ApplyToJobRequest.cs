namespace GraduationProject.Contracts.Users
{
    public record ApplyToJobRequest
    (
        string userId,
        int jobId
    );
}
