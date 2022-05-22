namespace GradingSystem.Portals.Admin.Models;
public class EvaluationSchemeModel
{
    public string Id { get; set; }
    public string ExamId { get; set; }
    public string ExamName { get; set; }
    public string Name { get; set; }
    public int NumberOfItems { get; set; }
    public List<EvaluationSchemeComponentModel> EvaluationSchemeComponents { get; set; }
    // public int ItemNr { get; set; }
    // public int PageNr { get; set; }
    // public int ScoreValue { get; set; }
    // public string ScoreComment { get; set; }
}
