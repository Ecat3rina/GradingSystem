using System.Net.Http.Json;
using Blazored.LocalStorage;
using GradingSystem.Portals.Admin.Model;
using GradingSystem.Portals.Admin.Models;
using GradingSystem.Portals.Admin.Services;
using Microsoft.AspNetCore.Components;
using System.Text.Json;
using System.Net.Http;

namespace GradingSystem.Portals.Admin.Services
{

    public class AuthenticationService : IAuthenticationService
    {
        [Inject]
        private HttpClient _httpClient { get; set; }
        private const string AuthToken = "__AUTH_TOKEN__";

        private ILocalStorageService _localStorage { get; set; }

        public LoginModel loginModel { get; set; }
        private UserModel UserModel {get; set;}=null;

        public AuthenticationService(ILocalStorageService localStorage, IHttpClientFactory httpClientFactory)
        {
            _localStorage = localStorage;
            _httpClient = httpClientFactory.CreateClient("AuthApi");
        }
        public async Task<string> AuthenticateUserAync(LoginModel model)
        {
            loginModel = new LoginModel
            {
                Username = model.Username,
                Password = model.Password
            };
            var result = await _httpClient.PostAsJsonAsync("auth", loginModel);

            if (result.IsSuccessStatusCode)
            {
                this.UserModel=new UserModel{Username=model.Username};
                var token = await result.Content.ReadFromJsonAsync<TokenModel>();
                await StoreAuthTokenAsync(token);
                return "OK";
            }
            else
            {
                return "KO";
            }

        }

        private async Task StoreAuthTokenAsync(TokenModel tokenModel)
        {
            var userModel = new UserModel
            {
                Token = tokenModel.Token
            };
            await _localStorage.SetItemAsync(AuthToken, userModel);
        }

        public async Task<string> GetAuthTokenAsync()
        {
            return (await _localStorage.GetItemAsync<UserModel>(AuthToken))?.Token ?? "";
        }

        public async Task<UserModel> GetCurrentUserAsync()
        {
            
            return this.UserModel;
        }

        public async Task LogoutUserAsync()
        {
            await _localStorage.ClearAsync();
        }
    }
}