using System.ComponentModel.DataAnnotations;

namespace GraduationProject.Entities
{
    public class SoftSkills
    {
        [Key]
        public int Id { get; set; }
        public int  questionNumber { get; set; }
        public string Question { get; set; }
        public string targetSkill { get; set; }
        public string answer { get; set; }
        public int Level { get; set; }
        public int Clarity { get; set; }
        public int exampleQuality { get; set; }
        public int Structure { get; set; }
        public int Outcome { get; set; }
        public int Score { get; set; }
        public string Strength { get; set; }
        public string Weakness { get; set; }
        public string Feedback { get; set; }
        public int interviewId { get; set; }
        public Interview Interview { get; set; }
    }
}