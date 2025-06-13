    namespace GraduationProject.Contracts.Users.Interview;

    public record AddInterviewDataRequest
    (
    
        int interviewId,
        string userProfileId,
        
        List<AddQuestions> questions

    );
    public record AddQuestions
    (
        int questionNumber,
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
