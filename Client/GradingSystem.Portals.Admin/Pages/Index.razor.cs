using Microsoft.AspNetCore.Components;

namespace GradingSystem.Portals.Admin.Pages;
public partial class Index
{
    [Inject]
    protected NavigationManager NavigationManager { get; set; }

    protected override void OnInitialized()
    {
        NavigationManager?.NavigateTo("login");
    }
}
