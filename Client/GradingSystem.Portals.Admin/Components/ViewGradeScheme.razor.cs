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

    public partial class ViewGradeScheme
    {
        [Inject]
        private IHttpClientFactory _httpClientFactory { get; set; }

        private HttpClient _httpClient { get; set; }
        [Parameter]
        public GradeSchemeModel GradeSchemeModel { get; set; } = new GradeSchemeModel();
        [Parameter]
        public string gradeSchemeId { get; set; }
        [Parameter]

        public EventCallback<GradeSchemeModel> OnViewCanceled { get; set; }
        [Inject]
        private IAuthenticationService _authService{get;set;} 
        protected override async Task OnInitializedAsync()
        {
            _httpClient = _httpClientFactory.CreateClient("AdminApi");
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await _authService.GetAuthTokenAsync());

            GradeSchemeModel = await _httpClient.GetFromJsonAsync<GradeSchemeModel>($"/gradescheme/{gradeSchemeId}");
        }

        protected async Task OnCancelViewClick()
        {
            await OnViewCanceled.InvokeAsync(null);
        }
    }
}