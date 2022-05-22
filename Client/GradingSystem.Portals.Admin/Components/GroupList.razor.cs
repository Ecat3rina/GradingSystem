using System.Text.Json;
using GradingSystem.Portals.Admin.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System.Net.Http;
using System.Net.Http.Json;
using System.Net.Http.Headers;
using GradingSystem.Portals.Admin.Services;

namespace GradingSystem.Portals.Admin.Components
{

    public partial class GroupList
    {
        [Inject]
        private IHttpClientFactory _httpClientFactory { get; set; }

         [Inject]
        private IAuthenticationService _authService{get;set;} 

        private HttpClient _httpClient { get; set; }
        [Parameter]
        public string InitialGroup { get; set; }
        [Parameter]
        public EventCallback<string> OnSelectedGroup { get; set; }
        [Parameter]
        public string selectedGroup { get; set; }

        [Parameter]
        public List<GroupModel> groupList { get; set; } = new List<GroupModel>();
        protected override async Task OnInitializedAsync()
        {
            _httpClient = _httpClientFactory.CreateClient("AdminApi");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await _authService.GetAuthTokenAsync());

            groupList = await _httpClient.GetFromJsonAsync<List<GroupModel>>("group/getallgroups");
        }
        protected async Task ReceiveNewGroup(ChangeEventArgs e)
        {
            await OnSelectedGroup.InvokeAsync(e.Value.ToString());
        }
    }
}