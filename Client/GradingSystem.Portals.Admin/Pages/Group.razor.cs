using GradingSystem.Portals.Admin.Models;
using GradingSystem.Portals.Admin.Components;
using Microsoft.AspNetCore.Components;
using System.Text.Json;
using System.Net.Http;
using System.Net.Http.Json;
using GradingSystem.Portals.Admin.Services;
using System.Net.Http.Headers;

namespace GradingSystem.Portals.Admin.Pages;

public partial class Group
{
    [Inject]
    private NavigationManager _navigationManager { get; set; }
    protected List<GroupModel> groups { get; set; }
    private bool _showEditForm { get; set; } = false;
    private bool _showDeleteModal { get; set; } = false;
    public string groupId { get; set; }
    [Inject]
    private IHttpClientFactory _httpClientFactory { get; set; }

    private HttpClient _httpClient { get; set; }

    protected GroupModel _editGroup { get; set; }
    [Inject]
    private IAuthenticationService _authService { get; set; }
    protected override async Task OnInitializedAsync()
    {
        _httpClient = _httpClientFactory.CreateClient("AdminApi");
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await _authService.GetAuthTokenAsync());

        groups = await _httpClient.GetFromJsonAsync<List<GroupModel>>("group/getallgroups");
    }

    protected void OnAddNewGroupClick()
    {
        _editGroup = new GroupModel();
        _showEditForm = true;
    }

    protected async Task OnEditGroupClick(string id)
    {
        var group = await _httpClient.GetFromJsonAsync<GroupModel>($"/group/getbyid/{id}");
        _editGroup = group;
        _showEditForm = true;
    }

    protected void OnDeleteGroupClick(string id)
    {
        _showDeleteModal = true;
        groupId = id;
    }

    protected async Task OnCloseDeleteModalClick(bool accepted)
    {
        if (accepted)
        {
            await _httpClient.DeleteAsync($"group/deletegroup/{groupId}");
        }
        _showDeleteModal = false;
        groups = await _httpClient.GetFromJsonAsync<List<GroupModel>>("group/getallgroups");
    }

    protected void OnGoBackClick()
    {
        _navigationManager.NavigateTo("home");
    }

    protected async Task OnFinishingEditingGroup(GroupModel groupModel)
    {
        _showEditForm = false;
        _editGroup = null;
        if (string.IsNullOrWhiteSpace(groupModel.Id))
        {
            await _httpClient.PostAsJsonAsync("group/", groupModel);
        }
        else
        {
            await _httpClient.PutAsJsonAsync("group/", groupModel);
        }
        groups = await _httpClient.GetFromJsonAsync<List<GroupModel>>("group/getallgroups");

    }

    protected void OnEditGroupCanceled()
    {
        _showEditForm = false;
    }
}
