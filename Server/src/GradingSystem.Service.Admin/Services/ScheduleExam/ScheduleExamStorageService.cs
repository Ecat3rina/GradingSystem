using GradingSystem.Service.Admin.DataAccess.ScheduleExam;
using GradingSystem.Service.Admin.Models;
using GradingSystem.Service.Admin.Services.Student;
using GradingSystem.Service.Admin.ViewModels.ScheduleExam;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace GradingSystem.Service.Admin.Services.ScheduleExam
{
    public class ScheduleExamStorageService:IScheduleExamStorageService
    {
        private readonly IScheduleExamRepository _scheduleExamRepository;
        private readonly HttpClient _scoringServiceHttpClient;
        private readonly IStudentStorageService _studentExamStorageService;
        public ScheduleExamStorageService(IScheduleExamRepository scheduleExamRepository,
            IStudentStorageService studentExamStorageService,
            IHttpClientFactory httpClientFactory)
        {
            _scheduleExamRepository= scheduleExamRepository;
            _scoringServiceHttpClient = httpClientFactory.CreateClient("ScoringService");
            _studentExamStorageService = studentExamStorageService;
        }

        public async Task<Guid> AddNewScheduleExam(NewScheduleExamViewModel model)
        {
            var scheduleExamModel = new ScheduleExamModel
            {
                Id = Guid.NewGuid(),
                GroupId=model.GroupId,
                ExamId=model.ExamId
            };
            await _scheduleExamRepository.AddScheduleExam(scheduleExamModel);


            var students = await _studentExamStorageService.GetStudentsByGroupId(model.GroupId);
            var payload = new
            {
                model.ExamId,
                Students = students.Select(x => x.Id).ToList()
            };

            var httpContent = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");
            using var httpRequest = new HttpRequestMessage(HttpMethod.Post, "schedule");
            httpRequest.Content = httpContent;

            var httpResponse = await _scoringServiceHttpClient.SendAsync(httpRequest);
            
            httpResponse.EnsureSuccessStatusCode();

            return scheduleExamModel.Id;
        }

        public async Task<List<GroupExamsModel>> GetScheduleExams()
        {
            return await _scheduleExamRepository.GetScheduleExams();
        }
    }
}
