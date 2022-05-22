using GradingSystem.Portals.Admin.Models;
using GradingSystem.Portals.Admin.Components;
using Microsoft.AspNetCore.Components;
using System.Text.Json;
using System.Net.Http;
using System.Net.Http.Json;
using System;
using System.Threading.Tasks;
using System.Linq;
using GradingSystem.Portals.Admin.Services;
using System.Net.Http.Headers;

namespace GradingSystem.Portals.Admin.Pages;

public partial class Evaluator
{
    [Inject]
    private NavigationManager _navigationManager { get; set; }
    [Inject]
    private IHttpClientFactory _httpClientFactory { get; set; }

    private HttpClient _httpClient { get; set; }
    protected List<EvaluatorModel> _evaluators { get; set; }
    protected List<SubjectModel> _subjectModels { get; set; }
    private bool _showEditForm { get; set; } = false;
    private bool _showDeleteModal { get; set; } = false;
    public string evaluatorId { get; set; }
    protected EvaluatorModel _editEvaluator { get; set; }
    [Inject]
    private IAuthenticationService _authService { get; set; }

    protected override async Task OnInitializedAsync()
    {
        _httpClient = _httpClientFactory.CreateClient("AdminApi");
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await _authService.GetAuthTokenAsync());

        await Task.WhenAll(GetEvaluators(), GetSubjects());
        foreach (var evaluator in _evaluators)
        {
            evaluator.SubjectName = _subjectModels.FirstOrDefault(x => x.Id == evaluator.SubjectId)?.Name;
        }
    }

    private async Task GetEvaluators()
    {
        _evaluators = await _httpClient.GetFromJsonAsync<List<EvaluatorModel>>("evaluator");
    }

    private async Task GetSubjects()
    {
        _subjectModels = await _httpClient.GetFromJsonAsync<List<SubjectModel>>("subject");
    }

    protected void OnAddNewEvaluatorClick()
    {
        _editEvaluator = new EvaluatorModel();
        _showEditForm = true;
    }
    protected async Task OnEditEvaluatorClick(string id)
    {
        var evaluator = await _httpClient.GetFromJsonAsync<EvaluatorModel>($"/evaluator/{id}");
        _editEvaluator = evaluator;
        var subject = await _httpClient.GetFromJsonAsync<SubjectModel>($"/subject/{evaluator.SubjectId}");
        _editEvaluator.SubjectName = subject.Name;
        _showEditForm = true;
    }


    protected void OnDeleteEvaluatorClick(string id)
    {
        _showDeleteModal = true;
        evaluatorId = id;
    }
    protected async Task OnCloseDeleteModalClick(bool accepted)
    {
        if (accepted)
        {
            await _httpClient.DeleteAsync($"evaluator/{evaluatorId}");
        }
        _showDeleteModal = false;

        await Task.WhenAll(GetEvaluators(), GetSubjects());
        foreach (var evaluator in _evaluators)
        {
            evaluator.SubjectName = _subjectModels.FirstOrDefault(x => x.Id == evaluator.SubjectId)?.Name;
        }
    }
    protected void OnGoBackClick()
    {
        _navigationManager.NavigateTo("home");
    }

    protected async Task OnFinishingEditingEvaluator(EvaluatorModel evaluatorModel)
    {
        _showEditForm = false;
        _editEvaluator = null;
        if (string.IsNullOrWhiteSpace(evaluatorModel.Id))
        {
            await _httpClient.PostAsJsonAsync("evaluator", evaluatorModel);
        }
        else
        {
            await _httpClient.PutAsJsonAsync("evaluator", evaluatorModel);
        }

        await Task.WhenAll(GetEvaluators(), GetSubjects());
        foreach (var evaluator in _evaluators)
        {
            evaluator.SubjectName = _subjectModels.FirstOrDefault(x => x.Id == evaluator.SubjectId)?.Name;
        }
    }

    protected void OnEditEvaluatorCanceled()
    {
        _showEditForm = false;
    }
}
