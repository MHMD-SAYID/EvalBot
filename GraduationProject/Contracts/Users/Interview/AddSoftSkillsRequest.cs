namespace GraduationProject.Contracts.Users.Interview
{
    public record AddSoftSkillsRequest
    (
        string userProfileId,
        int interviewId,
        List<SoftSkillsData> softSkillsList
    );
    public record SoftSkillsData(int questionNumber,
        string Question,
        string targetSkill,
        string Answer,
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
}
