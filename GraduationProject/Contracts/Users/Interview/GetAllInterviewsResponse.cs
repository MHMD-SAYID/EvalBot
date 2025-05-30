namespace GraduationProject.Contracts.Users.Interview;

public record GetAllInterviewsResponse
(
    List<InterViewProfile> Interviews

);
public record InterViewProfile
(
    int Id,
    string? VideoPath,
    string Topic,
    int? Warnings,
    double? AverageConfidenceScore,
    double? AverageTensionScore,
    List<double>? CheatTimes,
    bool IsCompleted


);