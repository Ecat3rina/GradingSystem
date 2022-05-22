namespace GradingSystem.Portals.Admin.Models;
public class GradeSchemeModel
{
    public string Id { get; set; }
    public string Name { get; set; }
    public List<GradeSchemeComponentModel> GradeSchemeComponents{ get; set; }
}
