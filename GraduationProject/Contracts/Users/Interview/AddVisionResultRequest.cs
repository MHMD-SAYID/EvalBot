namespace GraduationProject.Contracts.Users.Interview;

public record AddVisionResultRequest
(   
    int interviewId,
    string VideoPath,
    int Warnings,
    double interviewAverageConfidenceScore,
    double interviewAverageTensionScore,
    List<double> cheatTimes,
    bool isCompleted,
    List<visionPerQuestion> Data
);
public record visionPerQuestion
(
    int interviewId,
    int questionNumber,
    double averageConfidenceScore,
    double averageTensionScore
);
