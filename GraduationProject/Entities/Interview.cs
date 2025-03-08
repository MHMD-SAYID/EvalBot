

namespace GraduationProject.Models
{
    public class Interview
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public string FeedBack { get; set; }
        public int TracksId { get; set; }
        public Track track { get; set; }
        public int Score { get; set; }
        public ICollection<Q_A> q_a { get; set; } = new List<Q_A>();
        public Level level { get; set; }
    }
    }
