using System.ComponentModel.DataAnnotations;

namespace GradingSystem.Portals.Admin.Models;
public class ExamModel
{
    public string Id { get; set; }
    public string Name { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int NumberOfPages { get; set; }
    public int NumberOfEvaluators { get; set; }
    public string SubjectId { get; set; }
    public string SubjectName { get; set; }
    public string GradeSchemeId { get; set; }
    public string GradeSchemeName { get; set; }
    public bool WasGenerated { get; set; }

}
