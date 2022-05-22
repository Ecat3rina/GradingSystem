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

    public partial class SubjectList
    {
        [Inject]
        private IHttpClientFactory _httpClientFactory { get; set; }

        private HttpClient _httpClient { get; set; }
        [Parameter]
        public string InitialSubject { get; set; }
        [Parameter]
        public EventCallback<string> OnSelectedSubject { get; set; }
        [Parameter]
        public string selectedSubject { get; set; }

        [Parameter]
        public List<SubjectModel> _subjectList { get; set; } = new List<SubjectModel>();
        [Inject]
        private IAuthenticationService _authService{get;set;} 
        protected override async Task OnInitializedAsync()
        {
            _httpClient = _httpClientFactory.CreateClient("AdminApi");
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await _authService.GetAuthTokenAsync());

            _subjectList = await _httpClient.GetFromJsonAsync<List<SubjectModel>>("subject");
        }
        protected async Task ReceiveNewSubject(ChangeEventArgs e)
        {
            await OnSelectedSubject.InvokeAsync(e.Value.ToString());
        }
    }
}