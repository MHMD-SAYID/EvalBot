namespace GraduationProject.Contracts.Users
{
    public record GetAllJobsResponse
    (
        int Id,
        string jobTitle,
        string companyName,
        string Location,
        DateOnly releaseDate
    //List<jobShowedToUser> jobs
    );
    public record jobShowedToUser
    (
        string jobTitle,
        string companyName,
        string Location,
        DateOnly releaseDate
    );
}
