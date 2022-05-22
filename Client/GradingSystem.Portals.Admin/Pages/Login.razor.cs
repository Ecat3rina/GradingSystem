using Microsoft.AspNetCore.Components;
using GradingSystem.Portals.Admin.Models;
using GradingSystem.Portals.Admin.Services;

namespace GradingSystem.Portals.Admin.Pages;
public partial class Login
{

    [Inject]
    protected NavigationManager _navigationManager { get; set; }

    [Inject]
    protected IAuthenticationService _authenticationService { get; set; }
    protected LoginModel LoginModel { get; set; } = new();
    public bool userExists { get; set; } = true;


    public async Task OnLoginClick()
    {
        var result = await _authenticationService.AuthenticateUserAync(LoginModel);
        Console.WriteLine(result);

        if (result == "KO")
        {
            userExists = false;
        }
        else
        {
            _navigationManager?.NavigateTo("home");
        }
    }
}

