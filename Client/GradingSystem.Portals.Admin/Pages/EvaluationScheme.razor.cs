using GradingSystem.Portals.Admin.Models;
using GradingSystem.Portals.Admin.Components;
using Microsoft.AspNetCore.Components;
using System.Text.Json;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Linq;
using GradingSystem.Portals.Admin.Services;
using System.Net.Http.Headers;

namespace GradingSystem.Portals.Admin.Pages;

public partial class EvaluationScheme
{
    [Inject]
    private NavigationManager _navigationManager { get; set; }
    [Inject]
    private IHttpClientFactory _httpClientFactory { get; set; }

    private HttpClient _httpClient { get; set; }
    protected List<EvaluationSchemeModel> _evaluationSchemes { get; set; }
    protected List<ExamModel> _examModels { get; set; }
    private bool _showEditForm { get; set; } = false;
    private bool _showDeleteModal { get; set; } = false;
    public string _evaluationSchemeId { get; set; }
    private bool _showViewEvaluationSchemeComponentsModal { get; set; } = false;

    protected EvaluationSchemeModel _editEvaluationScheme { get; set; }
    [Inject]
    private IAuthenticationService _authService { get; set; }

    protected override async Task OnInitializedAsync()
    {
        _httpClient = _httpClientFactory.CreateClient("AdminApi");
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await _authService.GetAuthTokenAsync());

        await Task.WhenAll(GetEvaluationSchemes(), GetExams());
        foreach (var evaluationScheme in _evaluationSchemes)
        {
            evaluationScheme.ExamName = _examModels.FirstOrDefault(x => x.Id == evaluationScheme.ExamId)?.Name;
        }
    }

    private async Task GetEvaluationSchemes()
    {
        _evaluationSchemes = await _httpClient.GetFromJsonAsync<List<EvaluationSchemeModel>>("evaluationscheme");
    }

    private async Task GetExams()
    {
        _examModels = await _httpClient.GetFromJsonAsync<List<ExamModel>>("exam");
    }

    protected void OnAddNewEvaluationSchemeClick()
    {
        _editEvaluationScheme = new EvaluationSchemeModel();
        _editEvaluationScheme.EvaluationSchemeComponents = new List<EvaluationSchemeComponentModel>();
        _showEditForm = true;
        _showViewEvaluationSchemeComponentsModal = false;
    }
    protected async Task OnEditEvaluationSchemeClick(string id)
    {
        var evaluationscheme = await _httpClient.GetFromJsonAsync<EvaluationSchemeModel>($"/evaluationscheme/GetEvaluationSchemesById/{id}");
        _editEvaluationScheme = evaluationscheme;
        var exam = await _httpClient.GetFromJsonAsync<ExamModel>($"/exam/GetExamById/{evaluationscheme.ExamId}");
        _editEvaluationScheme.ExamName = exam.Name;
        _showEditForm = true;
    }


    protected void OnDeleteEvaluationSchemeClick(string id)
    {

        _showDeleteModal = true;
        _evaluationSchemeId = id;
    }
    protected async Task OnCloseDeleteModalClick(bool accepted)
    {
        if (accepted)
        {
            await _httpClient.DeleteAsync($"evaluationscheme/{_evaluationSchemeId}");
        }
        _showDeleteModal = false;

        await Task.WhenAll(GetEvaluationSchemes(), GetExams());
        foreach (var evaluationScheme in _evaluationSchemes)
        {
            evaluationScheme.ExamName = _examModels.FirstOrDefault(x => x.Id == evaluationScheme.ExamId)?.Name;
        }
    }
    protected void OnGoBackClick()
    {
        _navigationManager.NavigateTo("home");
    }

    protected async Task OnFinishingEditingEvaluationScheme(EvaluationSchemeModel evaluationSchemeModel)
    {
        _showEditForm = false;
        _editEvaluationScheme = null;
        if (string.IsNullOrWhiteSpace(evaluationSchemeModel.Id))
        {
            await _httpClient.PostAsJsonAsync("evaluationscheme", evaluationSchemeModel);
        }
        else
        {
            await _httpClient.PutAsJsonAsync("evaluationscheme", evaluationSchemeModel);
        }

        await Task.WhenAll(GetEvaluationSchemes(), GetExams());
        foreach (var evaluationScheme in _evaluationSchemes)
        {
            evaluationScheme.ExamName = _examModels.FirstOrDefault(x => x.Id == evaluationScheme.ExamId)?.Name;
        }
    }

    protected void OnEditEvaluationSchemeCanceled()
    {
        _showEditForm = false;
    }
    protected void OnViewEvaluationSchemeCanceled()
    {
        _showViewEvaluationSchemeComponentsModal = false;
    }
    protected void OnViewEvaluationSchemeClick(string id)
    {
        _showEditForm = false;
        _showViewEvaluationSchemeComponentsModal = true;
        _evaluationSchemeId = id;
    }
}
