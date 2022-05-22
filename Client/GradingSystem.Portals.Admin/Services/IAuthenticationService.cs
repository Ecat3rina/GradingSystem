using GradingSystem.Portals.Admin.Model;
using GradingSystem.Portals.Admin.Models;

namespace GradingSystem.Portals.Admin.Services
{


    public interface IAuthenticationService
    {
        Task<string> AuthenticateUserAync(LoginModel loginModel);
        Task<UserModel> GetCurrentUserAsync();
       Task<string> GetAuthTokenAsync();
        Task LogoutUserAsync();
    }
}