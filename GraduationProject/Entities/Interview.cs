

namespace GraduationProject.Entities
{
    public class Interview
    {
        public int Id { get; set; }
        public string? videoPath { get; set; }
        public string Topic { get; set; }
        public int? Warnings { get; set; }
        public double? AverageConfidenceScore { get; set; }
        public double? AverageTensionScore { get; set; }
        public ICollection<double>? CheatTimes { get; set; } = new List<double>();
        public bool IsCompleted { get; set; }
        public string userProfileId { get; set; }
        public UserProfile userProfile { get; set; }
        public ICollection<Q_A> q_a { get; set; } = new List<Q_A>();


        //public DateTime StartDate { get; set; }
        //public DateTime EndDate { get; set; }
        //public string FeedBack { get; set; }
        //public int TracksId { get; set; }
        //public Track track { get; set; }
        //public int Score { get; set; }
        // public Level level { get; set; }
    }
    }
