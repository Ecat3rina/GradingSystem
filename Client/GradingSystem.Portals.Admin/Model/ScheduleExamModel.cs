using System.ComponentModel.DataAnnotations;

namespace GradingSystem.Portals.Admin.Models;
public class ScheduleExamModel
{
    public string Id { get; set; }
    public string GroupId { get; set; }
    public string GroupName { get; set; }
    public string ExamId { get; set; }
    public string ExamName { get; set; }

}
