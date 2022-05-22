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

    public partial class ViewEvaluationScheme
    {
        [Inject]
        private IHttpClientFactory _httpClientFactory { get; set; }

        private HttpClient _httpClient { get; set; }
        [Parameter]
        public EvaluationSchemeModel EvaluationSchemeModel { get; set; } = new EvaluationSchemeModel();
        [Parameter]
        public string evaluationSchemeId { get; set; }
        [Parameter]
        public string examName { get; set; }

        [Parameter]
        public EventCallback<EvaluationSchemeModel> OnViewCanceled { get; set; }
        [Inject]
        private IAuthenticationService _authService{get;set;} 
        protected override async Task OnInitializedAsync()
        {
            _httpClient = _httpClientFactory.CreateClient("AdminApi");
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await _authService.GetAuthTokenAsync());

            EvaluationSchemeModel = await _httpClient.GetFromJsonAsync<EvaluationSchemeModel>($"/evaluationscheme/GetEvaluationSchemesById/{evaluationSchemeId}");
            var exam = await _httpClient.GetFromJsonAsync<ExamModel>($"exam/GetExamById/{EvaluationSchemeModel.ExamId}");
            examName = exam.Name;
        }

        protected async Task OnCancelViewClick()
        {
            await OnViewCanceled.InvokeAsync(null);
        }
    }
}