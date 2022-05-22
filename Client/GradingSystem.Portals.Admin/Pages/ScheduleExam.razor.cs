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

public partial class ScheduleExam
{
    [Inject]
    private NavigationManager _navigationManager { get; set; }
    [Inject]
    private IHttpClientFactory _httpClientFactory { get; set; }

    private HttpClient _httpClient { get; set; }
    private bool _showEditForm { get; set; } = false;
    protected ScheduleExamModel scheduleExam { get; set; }
    protected List<GroupExamModel> groupExams { get; set; }
    [Inject]
    private IAuthenticationService _authService { get; set; }

    protected override async Task OnInitializedAsync()
    {
        _httpClient = _httpClientFactory.CreateClient("AdminApi");
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await _authService.GetAuthTokenAsync());

        groupExams = await _httpClient.GetFromJsonAsync<List<GroupExamModel>>("scheduleexam");

    }

    protected void OnAddNewScheduleExamClick()
    {
        scheduleExam = new ScheduleExamModel();
        _showEditForm = true;
    }

    protected void OnGoBackClick()
    {
        _navigationManager.NavigateTo("home");
    }
    protected async Task OnFinishingEditingScheduleExam(ScheduleExamModel model)
    {
        _showEditForm = false;
        scheduleExam = null;
        if (string.IsNullOrWhiteSpace(model.Id))
        {
            await _httpClient.PostAsJsonAsync("scheduleexam", model);
        }
        else
        {
            await _httpClient.PutAsJsonAsync("student", model);
        }
        groupExams = await _httpClient.GetFromJsonAsync<List<GroupExamModel>>("scheduleexam");
    }

    protected void OnEditScheduleExamCanceled()
    {
        _showEditForm = false;
    }

}
