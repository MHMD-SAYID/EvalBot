
namespace GraduationProject.Entities
{
    public class Q_A
    {
        public int Id { get; set; }
        public string Topic { get; set; }
        public int QuestionNumber { get; set; }
        public ICollection<string> Links { get; set; } = new List<string>();
        public string ScoreExplanation { get; set; }
        public int Score { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
        public string userAnswer { get; set; }
        public int InterviewId { get; set; }
        public Interview Interview { get; set; }
        public string? audioLink { get; set; }
    }
}
