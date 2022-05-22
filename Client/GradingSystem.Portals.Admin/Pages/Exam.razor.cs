using GradingSystem.Portals.Admin.Models;
using GradingSystem.Portals.Admin.Components;
using Microsoft.AspNetCore.Components;
using System.Text.Json;
using System.Net.Http;
using System.Net.Http.Json;
using GradingSystem.Portals.Admin.Services;
using System.Net.Http.Headers;

namespace GradingSystem.Portals.Admin.Pages;

public partial class Exam
{
    [Inject]
    private NavigationManager _navigationManager { get; set; }
    protected List<ExamModel> _exams { get; set; }
    protected List<SubjectModel> _subjectModels { get; set; }
    protected List<GradeSchemeModel> gradeSchemeModels { get; set; }

    private bool _showEditForm { get; set; } = false;
    private bool _showDeleteModal { get; set; } = false;
    public string examId { get; set; }
    protected ExamModel _editExam { get; set; }

    [Inject]
    private IHttpClientFactory _httpClientFactory { get; set; }
    [Inject]
    private IAuthenticationService _authService { get; set; }

    private HttpClient _httpClient { get; set; }
    protected override async Task OnInitializedAsync()
    {
        _httpClient = _httpClientFactory.CreateClient("AdminApi");
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await _authService.GetAuthTokenAsync());

        await Task.WhenAll(GetExams(), GetSubjects(), GetGradeSchemes());
        foreach (var exam in _exams)
        {
            exam.SubjectName = _subjectModels.FirstOrDefault(x => x.Id == exam.SubjectId)?.Name;
            exam.GradeSchemeName = gradeSchemeModels.FirstOrDefault(x => x.Id == exam.GradeSchemeId)?.Name;
        }
    }

    private async Task GetGradeSchemes()
    {
        gradeSchemeModels = await _httpClient.GetFromJsonAsync<List<GradeSchemeModel>>("gradescheme");
    }
    private async Task GetExams()
    {
        _exams = await _httpClient.GetFromJsonAsync<List<ExamModel>>("exam");
    }

    private async Task GetSubjects()
    {
        _subjectModels = await _httpClient.GetFromJsonAsync<List<SubjectModel>>("subject");
    }

    protected void OnAddNewExamClick()
    {
        _editExam = new ExamModel();
        _showEditForm = true;
    }
    protected async Task OnEditExamClick(string id)
    {
        var exam = await _httpClient.GetFromJsonAsync<ExamModel>($"/exam/GetExamById/{id}");
        _editExam = exam;
        var subject = await _httpClient.GetFromJsonAsync<SubjectModel>($"/subject/{exam.SubjectId}");
        _editExam.SubjectName = subject.Name;
        var gradeScheme = await _httpClient.GetFromJsonAsync<GradeSchemeModel>($"/gradescheme/{exam.GradeSchemeId}");
        _editExam.GradeSchemeName = gradeScheme.Name;
        _showEditForm = true;
    }


    protected void OnDeleteExamClick(string id)
    {
        _showDeleteModal = true;
        examId = id;
    }

    protected async Task OnCloseDeleteModalClick(bool accepted)
    {
        if (accepted)
        {
            await _httpClient.DeleteAsync($"exam/{examId}");
        }
        _showDeleteModal = false;

        await Task.WhenAll(GetExams(), GetSubjects(), GetGradeSchemes());
        foreach (var exam in _exams)
        {
            exam.SubjectName = _subjectModels.FirstOrDefault(x => x.Id == exam.SubjectId)?.Name;
            exam.GradeSchemeName = gradeSchemeModels.FirstOrDefault(x => x.Id == exam.GradeSchemeId)?.Name;
        }
    }
    protected void OnGoBackClick()
    {
        _navigationManager.NavigateTo("/home");
    }

    protected async Task OnFinishingEditingExam(ExamModel examModel)
    {
        _showEditForm = false;
        _editExam = null;
        if (string.IsNullOrWhiteSpace(examModel.Id))
        {
            await _httpClient.PostAsJsonAsync("exam", examModel);
        }
        else
        {
            await _httpClient.PutAsJsonAsync("exam", examModel);
        }

        await Task.WhenAll(GetExams(), GetSubjects(), GetGradeSchemes());
        foreach (var exam in _exams)
        {
            exam.SubjectName = _subjectModels.FirstOrDefault(x => x.Id == exam.SubjectId)?.Name;
            exam.GradeSchemeName = gradeSchemeModels.FirstOrDefault(x => x.Id == exam.GradeSchemeId)?.Name;
        }
    }

    protected void OnEditExamCanceled()
    {
        _showEditForm = false;
    }
}
