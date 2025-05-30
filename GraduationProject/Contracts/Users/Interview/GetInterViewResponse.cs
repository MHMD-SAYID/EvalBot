namespace GraduationProject.Contracts.Users.Interview;

public record GetInterViewResponse
(
    InterViewProfiles InterviewProfile,
    List<InterviewQuestions> Questions,
    List<interviewVisionResult> VisionResults
);
public record InterViewProfiles
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
public record InterviewQuestions
(
    int questionNumber,
    string Question,
    string userAnswer,
    string answer,
    List<string> Links,
    int score,
    string scoreExplanation,
    string Topic
);
public record interviewVisionResult
(
    int questionNumber,
    double tensionScore,
    double confidenceScore
);