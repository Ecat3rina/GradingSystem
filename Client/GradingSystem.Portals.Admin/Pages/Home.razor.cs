using GradingSystem.Portals.Admin.Services;
using Microsoft.AspNetCore.Components;

namespace GradingSystem.Portals.Admin.Pages;
public partial class Home
{
    [Inject]
    protected IAuthenticationService _authenticationService { get; set; }

    [Inject]
    private NavigationManager _navigationManager { get; set; }
    protected string Username { get; set; }
    protected override async Task OnInitializedAsync()
    {
        Username = (await _authenticationService?.GetCurrentUserAsync()).Username;

        _navigationManager.NavigateTo("/home/main");
    }

    public async Task OnLogoutClick()
    {
        await _authenticationService.LogoutUserAsync();
        _navigationManager?.NavigateTo("login");
    }

}
