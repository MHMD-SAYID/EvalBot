namespace GraduationProject.Contracts.Users.Interview;

public record AddVisionResultRequest
(   int questionNumber,
    int interviewId,
    double averageConfidenceScore,
    double averageTensionScore,
    string VideoPath,
    int Warnings,
    double interviewAverageConfidenceScore,
    double interviewAverageTensionScore,
    List<double> cheatTimes,
    bool isCompleted
);
