using System.ComponentModel.DataAnnotations;

namespace GraduationProject.Entities;

public class Q_AVisionResult
{
    
    public int Id { get; set; }
    public int questionNumber { get; set; }
    public int interviewId { get; set; }
    public Interview Interview { get; set; }
    public double AverageConfidenceScore { get; set; }
    public double AverageTensionScore { get; set; }
}
