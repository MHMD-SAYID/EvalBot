namespace GraduationProject.Contracts.Users.Interview;

public record AddInterviewDataRequest
(
    int questionNumber,
    int interviewId,
    string Answer,
    string userAnswer,
    string Question,
    List<string> Links,
    string Topic,
    string ScoreExplanation,
    int Score,
    double confidenceScore,
    double tensionScore
    
);
