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

    public partial class ExamList
    {
        [Inject]
        private IHttpClientFactory _httpClientFactory { get; set; }

        private HttpClient _httpClient { get; set; }
        [Parameter]
        public string InitialExam { get; set; }
        [Parameter]
        public EventCallback<string> OnSelectedExam { get; set; }
        [Parameter]
        public string selectedExam { get; set; }

        [Parameter]
        public List<ExamModel> examList { get; set; } = new List<ExamModel>();
        [Inject]
        private IAuthenticationService _authService { get; set; }
        protected override async Task OnInitializedAsync()
        {

            _httpClient = _httpClientFactory.CreateClient("AdminApi");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await _authService.GetAuthTokenAsync());

            examList = await _httpClient.GetFromJsonAsync<List<ExamModel>>("exam");
        }
        protected async Task ReceiveNewExam(ChangeEventArgs e)
        {
            await OnSelectedExam.InvokeAsync(e.Value.ToString());
        }
    }
}