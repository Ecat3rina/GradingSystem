using Microsoft.AspNetCore.Components;

namespace GradingSystem.Portals.Admin.Pages;

public partial class MainMenu
{
    [Inject]
    private NavigationManager _navigationManager { get; set; }
    public void OnStudentsManagementClick()
    {
        _navigationManager.NavigateTo("/home/students");
    }
    public void OnEvaluatorsManagementClick()
    {
        _navigationManager.NavigateTo("/home/evaluators");
    }
    public void OnSubjectsManagementClick()
    {
        _navigationManager.NavigateTo("/home/subjects");
    }
     public void OnExamsManagementClick()
    {
        _navigationManager.NavigateTo("/home/exams");
    }
    public void OnGradeSchemesManagementClick()
    {
        _navigationManager.NavigateTo("/home/gradeschemes");
    }
    public void OnEvaluationSchemesManagementClick()
    {
        _navigationManager.NavigateTo("/home/evaluationschemes");
    }
    public void OnGroupsManagementClick()
    {
        _navigationManager.NavigateTo("/home/groups");
    }
    public void OnScheduleExamClick()
    {
        _navigationManager.NavigateTo("/home/scheduleexam");
    }
    public void OnOnEvaluatorDistributionClick()
    {
        _navigationManager.NavigateTo("/home/evaluatordistribution");
    }
}
