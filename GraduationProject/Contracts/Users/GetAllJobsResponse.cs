namespace GraduationProject.Contracts.Users
{
    public record GetAllJobsResponse
    (
        int Id,
        string imageUrl,
        string jobTitle,
        string companyName,
        string Location,
        DateOnly releaseDate
    );
    public record jobShowedToUser
    (
        string jobTitle,
        string companyName,
        string Location,
        DateOnly releaseDate
    );
}
