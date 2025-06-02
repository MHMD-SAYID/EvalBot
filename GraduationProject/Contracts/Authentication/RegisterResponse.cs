namespace GraduationProject.Contracts.Authentication
{
    public record RegisterResponse
    (
        string userId,
        string userName,
        string country,
        string phoneNumber,
        ICollection<string>skills,
        int yearsOfExperience,
        double expectedSalary,
        string nationality,
        DateOnly dateOfBirth,
        string role
    );
}
