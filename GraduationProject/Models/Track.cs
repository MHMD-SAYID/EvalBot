namespace GraduationProject.Entites
{
    public class Track
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Interview interview { get; set; }
        public ICollection<Q_A> q_a { get; set; }=new List<Q_A>();
    }
}
