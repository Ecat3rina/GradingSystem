using System.ComponentModel.DataAnnotations;

namespace GradingSystem.Portals.Admin.Models;
public class GroupExamModel
{
    public string GroupName { get; set; }
    public List<string> Exams{ get; set; }
}
