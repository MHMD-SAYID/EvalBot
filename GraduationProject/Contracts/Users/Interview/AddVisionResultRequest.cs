namespace GraduationProject.Contracts.Users.Interview;

public record AddVisionResultRequest
(   
    int interviewId,
    string VideoPath,
    int Warnings,
    double interviewAverageConfidenceScore,
    double interviewAverageTensionScore,
    List<double> cheatTimes,
    bool isCompleted
   
);

