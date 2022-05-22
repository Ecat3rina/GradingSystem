using GradingSystem.Service.Authentication.DataAccess;
using GradingSystem.Service.Authentication.Options;
using GradingSystem.Service.Authentication.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradingSystem.Service.Authentication
{
    public static class Dependencies
    {

        public static void Register(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IAuthService, AuthService>();
            services.Configure<AuthTokenConfiguration>(configuration.GetSection("Auth:GradingSystemToken"));


            services.AddScoped<IAuthRepository>(x => new AuthRepository(configuration.GetConnectionString("Auth")));


            services.AddHttpClient("AdminService", httpClient =>
            {
                var baseAddress = configuration.GetSection("Services").GetValue<string>("Admin");
                httpClient.BaseAddress = new Uri(baseAddress, UriKind.Absolute);
                httpClient.Timeout = new TimeSpan(0, 0, 30);
            });
        }
    }
}
