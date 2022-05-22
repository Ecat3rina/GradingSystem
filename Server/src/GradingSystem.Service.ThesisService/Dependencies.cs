using GradingSystem.Service.ThesisService.DataAccess;
using GradingSystem.Service.ThesisService.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace GradingSystem.Service.ThesisService
{
    public static class Dependencies
    {
        public static void Register(IServiceCollection services, IConfiguration configuration)
        {
            //services
            services.AddScoped<IThesisStorageService, ThesisStorageService>();

            //repositories
            services.AddScoped<IThesisRepository>(x => new ThesisRepository(configuration.GetConnectionString("Theses")));

            //typed clients

            services.AddHttpClient("ScoringService", httpClient =>
            {
                var baseAddress = configuration.GetSection("Services").GetValue<string>("Scoring");
                httpClient.BaseAddress = new Uri(baseAddress, UriKind.Absolute);
                httpClient.Timeout = new TimeSpan(0, 0, 30);
            });
        }
    }
}
