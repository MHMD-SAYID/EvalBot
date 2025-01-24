namespace GraduationProject.Entites
{
    public class Q_A
    {
        public int Id { get; set; }
        public string Answer { get; set; }
        public string Question { get; set; }
        public Level level { get; set; }
        public int trackId { get; set; }
        public Track track { get; set; }
        public int InterviewId { get; set; }
        public Interview Interview { get; set; }

    }
}
