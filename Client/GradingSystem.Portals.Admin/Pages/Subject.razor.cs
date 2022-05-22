using GradingSystem.Portals.Admin.Models;
using GradingSystem.Portals.Admin.Components;
using Microsoft.AspNetCore.Components;
using System.Text.Json;
using System.Net.Http;
using System.Net.Http.Json;
using GradingSystem.Portals.Admin.Services;
using System.Net.Http.Headers;

namespace GradingSystem.Portals.Admin.Pages;

public partial class Subject
{
    [Inject]
    private NavigationManager _navigationManager { get; set; }
    [Inject]
    private IHttpClientFactory _httpClientFactory { get; set; }

    private HttpClient _httpClient { get; set; }
    protected List<SubjectModel> _subjects { get; set; }
    private bool _showEditForm { get; set; } = false;
    private bool _showDeleteModal { get; set; } = false;
    public string subjectId { get; set; }
    protected SubjectModel _editSubject { get; set; }
    [Inject]
    private IAuthenticationService _authService { get; set; }

    protected override async Task OnInitializedAsync()
    {
        _httpClient = _httpClientFactory.CreateClient("AdminApi");
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await _authService.GetAuthTokenAsync());

        _subjects = await _httpClient.GetFromJsonAsync<List<SubjectModel>>("subject");
    }
    protected void OnAddNewSubjectClick()
    {
        _editSubject = new SubjectModel();
        _showEditForm = true;
    }
    protected async Task OnEditSubjectClick(string id)
    {
        var subject = await _httpClient.GetFromJsonAsync<SubjectModel>($"/subject/{id}");
        _editSubject = subject;
        _showEditForm = true;
    }


    protected void OnDeleteSubjectClick(string id)
    {
        _showDeleteModal = true;
        subjectId = id;
    }
    protected async Task OnCloseDeleteModalClick(bool accepted)
    {
        if (accepted)
        {
            await _httpClient.DeleteAsync($"subject/{subjectId}");
        }
        _showDeleteModal = false;
        _subjects = await _httpClient.GetFromJsonAsync<List<SubjectModel>>("subject");
    }
    protected void OnGoBackClick()
    {
        _navigationManager.NavigateTo("home");
    }

    protected async Task OnFinishingEditingSubject(SubjectModel subjectModel)
    {
        _showEditForm = false;
        _editSubject = null;
        if (string.IsNullOrWhiteSpace(subjectModel.Id))
        {
            await _httpClient.PostAsJsonAsync("subject", subjectModel);
        }
        else
        {
            await _httpClient.PutAsJsonAsync("subject", subjectModel);
        }
        _subjects = await _httpClient.GetFromJsonAsync<List<SubjectModel>>("subject");
    }

    protected void OnEditSubjectCanceled()
    {
        _showEditForm = false;
    }
}
