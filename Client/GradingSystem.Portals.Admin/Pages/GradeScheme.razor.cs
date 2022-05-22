using GradingSystem.Portals.Admin.Models;
using GradingSystem.Portals.Admin.Components;
using Microsoft.AspNetCore.Components;
using System.Text.Json;
using System.Net.Http;
using System.Net.Http.Json;
using GradingSystem.Portals.Admin.Services;
using System.Net.Http.Headers;

namespace GradingSystem.Portals.Admin.Pages;

public partial class GradeScheme
{
    [Inject]
    private NavigationManager _navigationManager { get; set; }
    protected List<GradeSchemeModel> _gradeSchemes { get; set; }
    private bool _showEditForm { get; set; } = false;
    private bool _showDeleteModal { get; set; } = false;
    private bool _showViewGradeSchemeComponentsModal { get; set; } = false;

    public string gradeSchemeId { get; set; }
    protected GradeSchemeModel editGradeScheme { get; set; }

    [Inject]
    private IHttpClientFactory _httpClientFactory { get; set; }

    private HttpClient _httpClient { get; set; }
    [Inject]
    private IAuthenticationService _authService { get; set; }

    protected override async Task OnInitializedAsync()
    {
        _httpClient = _httpClientFactory.CreateClient("AdminApi");
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await _authService.GetAuthTokenAsync());

        _gradeSchemes = await _httpClient.GetFromJsonAsync<List<GradeSchemeModel>>("gradescheme");

    }



    protected void OnAddNewGradeSchemeClick()
    {
        editGradeScheme = new GradeSchemeModel();
        editGradeScheme.GradeSchemeComponents = new List<GradeSchemeComponentModel>();
        _showEditForm = true;
        _showViewGradeSchemeComponentsModal = false;
    }
    protected async Task OnEditGradeSchemeClick(string id)
    {
        var gradeScheme = await _httpClient.GetFromJsonAsync<GradeSchemeModel>($"/gradescheme/{id}");
        editGradeScheme = gradeScheme;
        _showEditForm = true;
    }


    protected void OnDeleteGradeSchemeClick(string id)
    {
        _showDeleteModal = true;
        gradeSchemeId = id;
        _showEditForm = false;

    }
    protected void OnViewGradeSchemeClick(string id)
    {
        _showEditForm = false;
        _showViewGradeSchemeComponentsModal = true;
        gradeSchemeId = id;
    }

    protected async Task OnCloseDeleteModalClick(bool accepted)
    {
        if (accepted)
        {
            await _httpClient.DeleteAsync($"gradescheme/{gradeSchemeId}");
        }
        _showDeleteModal = false;
        _gradeSchemes = await _httpClient.GetFromJsonAsync<List<GradeSchemeModel>>("gradescheme");

    }
    protected void OnGoBackClick()
    {
        _navigationManager.NavigateTo("/home");
    }

    protected async Task OnFinishingEditingGradeScheme(GradeSchemeModel gradeSchemeModel)
    {
        _showEditForm = false;
        editGradeScheme = null;
        if (string.IsNullOrWhiteSpace(gradeSchemeModel.Id))
        {
            await _httpClient.PostAsJsonAsync("gradescheme", gradeSchemeModel);
        }
        else
        {
            await _httpClient.PutAsJsonAsync("gradescheme", gradeSchemeModel);
        }
        _gradeSchemes = await _httpClient.GetFromJsonAsync<List<GradeSchemeModel>>("gradescheme");


    }
    protected void OnEditGradeSchemeCanceled()
    {
        _showEditForm = false;
    }

    protected void OnViewGradeSchemeCanceled()
    {
        _showViewGradeSchemeComponentsModal = false;
    }
}
