
namespace GradingSystem.Portals.Admin.Models;
public class EvaluationSchemeComponentModel
{
    public Guid Id { get; set; }
    public Guid EvaluationSchemeId { get; set; }
    public int? ItemNr { get; set; }
    public int? PageNr { get; set; }
    public int? MinimumScore { get; set; }
    public int? MaximumScore { get; set; }
    public string CorrectAnswer { get; set; }
    public string Specifications { get; set; }
}

