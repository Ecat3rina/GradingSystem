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

public partial class EvaluatorDistribution
{
    [Inject]
    private NavigationManager _navigationManager { get; set; }
    [Inject]
    private IHttpClientFactory _httpClientFactory { get; set; }

    private HttpClient _httpClient { get; set; }
    public TriggerRepartitionRequest request { get; set; }
    protected List<ExamStatisticsModel> statistics { get; set; }
    [Inject]
    private IAuthenticationService _authService { get; set; }


    protected override async Task OnInitializedAsync()
    {
        _httpClient = _httpClientFactory.CreateClient("AdminApi");
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await _authService.GetAuthTokenAsync());

        statistics = await _httpClient.GetFromJsonAsync<List<ExamStatisticsModel>>("evaluatorRepartition");
    }
    protected void OnGoBackClick()
    {
        _navigationManager.NavigateTo("home");
    }
    protected async Task TriggerEvaluatorRepartitionClick(string name)
    {
        System.Console.WriteLine(name);
        request = new TriggerRepartitionRequest { ExamName = name };
        await _httpClient.PostAsJsonAsync("evaluatorRepartition", request);
    }

}
