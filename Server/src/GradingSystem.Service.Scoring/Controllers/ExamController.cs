using GradingSystem.Service.Scoring.Models.Schedule;
using GradingSystem.Service.Scoring.Services.Exam;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradingSystem.Service.Scoring.Controllers
{
    [Route("schedule")]
    [ApiController]
    [Authorize(Policy = Startup.DefaultPolicy)]
    public class ExamController : ControllerBase
    {
        private readonly IExamStorageService _examStorageService;
        public ExamController(IExamStorageService examStorageService)
        {
            _examStorageService = examStorageService;
        }

        [HttpPost]
        public async Task ScheduleAsync(ScheduleModel model)
        {
            await _examStorageService.ScheduleExams(model);
        }
    }
}
