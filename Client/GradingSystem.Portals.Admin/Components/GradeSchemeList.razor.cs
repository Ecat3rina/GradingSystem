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

    public partial class GradeSchemeList
    {
        [Inject]
        private IHttpClientFactory _httpClientFactory { get; set; }

        private HttpClient _httpClient { get; set; }
        [Parameter]
        public string InitialGradeScheme { get; set; }
        [Parameter]
        public string initialScheme { get; set; }
        [Parameter]
        public EventCallback<string> OnSelectedGradeScheme { get; set; }
        [Parameter]
        public string selectedScheme { get; set; }

        [Parameter]
        public List<GradeSchemeModel> schemeList { get; set; } = new List<GradeSchemeModel>();
        [Inject]
        private IAuthenticationService _authService{get;set;} 
        
        protected override async Task OnInitializedAsync()
        {
            _httpClient = _httpClientFactory.CreateClient("AdminApi");
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await _authService.GetAuthTokenAsync());

            schemeList = await _httpClient.GetFromJsonAsync<List<GradeSchemeModel>>("gradescheme");
        }
        protected async Task ReceiveNewScheme(ChangeEventArgs e)
        {
            await OnSelectedGradeScheme.InvokeAsync(e.Value.ToString());
        }
    }
}