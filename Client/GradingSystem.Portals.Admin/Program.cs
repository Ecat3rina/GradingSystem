using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using GradingSystem.Portals.Admin;
using Blazored.LocalStorage;
using GradingSystem.Portals.Admin.Services;
using Microsoft.Extensions.Http;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");


builder.Services.AddHttpClient("AuthApi", client => 
    client.BaseAddress = new Uri(@"http:\\localhost:46098"));

builder.Services.AddHttpClient("AdminApi", client => 
    client.BaseAddress = new Uri(@"http:\\localhost:57716"));

builder.Services.AddBlazoredLocalStorage();
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();

await builder.Build().RunAsync();

