namespace GraduationProject.Contracts.Users.Interview;

public record GetInterViewResponse
(
    int Id,
    string? VideoPath,
    string Topic,
    int? Warnings,
    double? AverageConfidenceScore,
    double? AverageTensionScore,
    List<double>? CheatTimes,
    bool IsCompleted,
    List<InterviewQuestions> Questions,
    List<SoftSkillsDataResponse> SoftSkillsData
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
    ICollection<string> Links,
    int score,
    string scoreExplanation,
    string Topic
    
);
public record SoftSkillsDataResponse
    (
        string softSkillsQuestion,
        string targetSkill,
        string softSkillAnswer,
        int Level,
        int Clarity,
        int exampleQuality,
        int Structure,
        int Outcome,
        int Score,
        string Strength,
        string Weakness,
        string Feedback
    );
