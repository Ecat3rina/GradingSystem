using GradingSystem.Service.Admin.DataAccess;
using GradingSystem.Service.Admin.DataAccess.EvaluationScheme;
using GradingSystem.Service.Admin.DataAccess.Exam;
using GradingSystem.Service.Admin.DataAccess.GradeScheme;
using GradingSystem.Service.Admin.DataAccess.Group;
using GradingSystem.Service.Admin.DataAccess.ScheduleExam;
using GradingSystem.Service.Admin.DataAccess.Student;
using GradingSystem.Service.Admin.DataAccess.Subject;
using GradingSystem.Service.Admin.Services;
using GradingSystem.Service.Admin.Services.EvaluationScheme;
using GradingSystem.Service.Admin.Services.Exam;
using GradingSystem.Service.Admin.Services.GradeScheme;
using GradingSystem.Service.Admin.Services.Group;
using GradingSystem.Service.Admin.Services.ScheduleExam;
using GradingSystem.Service.Admin.Services.Student;
using GradingSystem.Service.Admin.Services.Subject;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace GradingSystem.Service.Admin
{
    public class Dependencies
    {
        public static void Register(IServiceCollection services, IConfiguration configuration)
        {
            //services
            services.AddScoped<IEvaluatorStorageService, EvaluatorStorageService>();
            services.AddScoped<IStudentStorageService, StudentStorageService>();
            services.AddScoped<ISubjectStorageService, SubjectStorageService>();
            services.AddScoped<IEvaluationSchemeStorageService, EvaluationSchemeStorageService>();
            services.AddScoped<IExamStorageService, ExamStorageService>();
            services.AddScoped<IGradeSchemeStorageService, GradeSchemeStorageService>();
            services.AddScoped<IGroupStorageService, GroupStorageService>();
            services.AddScoped<IScheduleExamStorageService, ScheduleExamStorageService>();
            services.AddScoped<IEvaluatorRepartitionStorageService, EvaluatorRepartitionStorageService>();


            //repositories
            services.AddScoped<IEvaluatorRepository>(x => new EvaluatorRepository(configuration.GetConnectionString("Admin")));
            services.AddScoped<IStudentRepository>(x => new StudentRepository(configuration.GetConnectionString("Admin")));
            services.AddScoped<ISubjectRepository>(x => new SubjectRepository(configuration.GetConnectionString("Admin")));
            services.AddScoped<IEvaluationSchemeRepository>(x => new EvaluationSchemeRepository(configuration.GetConnectionString("Admin")));
            services.AddScoped<IExamRepository>(x => new ExamRepository(configuration.GetConnectionString("Admin")));
            services.AddScoped<IGradeSchemeRepository>(x => new GradeSchemeRepository(configuration.GetConnectionString("Admin")));
            services.AddScoped<IGroupRepository>(x => new GroupRepository(configuration.GetConnectionString("Admin")));
            services.AddScoped<IScheduleExamRepository>(x => new ScheduleExamRepository(configuration.GetConnectionString("Admin")));
            services.AddScoped<IEvaluatorRepartitionRepository>(x => new EvaluatorRepartitionRepository(configuration.GetConnectionString("Admin")));

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
