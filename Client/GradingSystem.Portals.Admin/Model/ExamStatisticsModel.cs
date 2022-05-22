namespace GradingSystem.Portals.Admin.Models;
public class ExamStatisticsModel
{
    public string ExamName { get; set; }
    public string SubjectName { get; set; }
    public int TotalNrOfEvaluators { get; set; }
    public int NumberOfEvaluatorsPerThesis { get; set; }
    public bool WasGenerated { get; set; }
}
