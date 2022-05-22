using System.Text.Json;
using GradingSystem.Portals.Admin.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System.Net.Http;
using System.Net.Http.Json;
using GradingSystem.Portals.Admin.Services;
using System.Net.Http.Headers;

namespace GradingSystem.Portals.Admin.Components
{

    public partial class EvaluationSchemeList
    {
        [Inject]
        private IHttpClientFactory _httpClientFactory { get; set; }

        private HttpClient _httpClient { get; set; }
        [Parameter]
        public string InitialEvaluationScheme { get; set; }
        [Parameter]
        public EventCallback<string> OnSelectedEvaluationScheme { get; set; }
        [Parameter]
        public string selectedEvaluationScheme { get; set; }
         [Inject]
        private IAuthenticationService _authService{get;set;} 

        [Parameter]
        public List<EvaluationSchemeModel> _evaluationSchemes { get; set; } = new List<EvaluationSchemeModel>();
        protected override async Task OnInitializedAsync()
        {
            _httpClient = _httpClientFactory.CreateClient("AdminApi");
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await _authService.GetAuthTokenAsync());

            _evaluationSchemes = await _httpClient.GetFromJsonAsync<List<EvaluationSchemeModel>>("evaluationscheme");
        }
        protected async Task ReceiveNewEvaluationScheme(ChangeEventArgs e)
        {
            await OnSelectedEvaluationScheme.InvokeAsync(e.Value.ToString());
        }
    }
}