using GradingSystem.Service.Scoring.DataAccess;
using GradingSystem.Service.Scoring.Services.Exam;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using GradingSystem.Service.Scoring.Services.Repartition;
using GradingSystem.Service.Scoring.DataAccess.Repartition;
using GradingSystem.Service.Scoring.Services.Scoring;
using GradingSystem.Service.Scoring.DataAccess.Scoring;
using GradingSystem.Service.Scoring.Services.Grade_Visualisation;
using System;

namespace GradingSystem.Service.Scoring
{

    public class Dependencies
    {
        public static void Register(IServiceCollection services, IConfiguration configuration)
        {
            //services
            services.AddScoped<IExamStorageService, ExamStorageService>();
            services.AddScoped<IThesisStorageService, ThesisStorageService>();
            services.AddScoped<IRepartitionStorageService, RepartitionStorageService>();
            services.AddScoped<IScoringStorageService, ScoringStorageService>();
            services.AddScoped<IGradeVisualisationService, GradeVisualisationService>();

            //repositories
            services.AddScoped<IExamRepository>(x => new ExamRepository(configuration.GetConnectionString("Scoring")));
            services.AddScoped<IThesisRepository>(x => new ThesisRepository(configuration.GetConnectionString("Scoring")));
            services.AddScoped<IRepartitionRepository>(x => new RepartitionRepository(configuration.GetConnectionString("Scoring")));
            services.AddScoped<IScoringRepository>(x => new ScoringRepository(configuration.GetConnectionString("Scoring")));

            //typed clients

            services.AddHttpClient("Admin", httpClient =>
            {
                var baseAddress = configuration.GetSection("Services").GetValue<string>("Admin");
                httpClient.BaseAddress = new Uri(baseAddress, UriKind.Absolute);
                httpClient.Timeout = new TimeSpan(0, 5, 0);
            });
        }
    }

}
