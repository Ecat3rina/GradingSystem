using GradingSystem.Service.Scoring.DataAccess;
using GradingSystem.Service.Scoring.Models;
using GradingSystem.Service.Scoring.Models.Schedule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace GradingSystem.Service.Scoring.Services.Exam
{
    public class ExamStorageService : IExamStorageService
    {
        private readonly IExamRepository _examRepository;
        
        public ExamStorageService(IExamRepository examRepository)
        {
            _examRepository = examRepository;
           
        }
        public async Task ScheduleExams(ScheduleModel model)
        {
            foreach (var student in model.Students)
            {
                var thesisModel = new ThesisModel
                {
                    Id = Guid.NewGuid(),
                    StudentId = student,
                    ExamId = model.ExamId.ToString(),
                };
                await _examRepository.AddThesis(thesisModel);

               
            }
        }
    }
}
