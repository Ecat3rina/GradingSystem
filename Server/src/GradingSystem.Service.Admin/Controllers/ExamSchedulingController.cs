using GradingSystem.Service.Admin.Models;
using GradingSystem.Service.Admin.Services.ScheduleExam;
using GradingSystem.Service.Admin.Services.Student;
using GradingSystem.Service.Admin.ViewModels;
using GradingSystem.Service.Admin.ViewModels.ScheduleExam;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradingSystem.Service.Admin.Controllers
{
    [Route("scheduleexam")]
    [ApiController]
    [Authorize(Policy = Startup.DefaultPolicy)]
    public class ExamSchedulingController : ControllerBase
    {
        private readonly IScheduleExamStorageService _scheduleExamStorageService;

        public ExamSchedulingController(IScheduleExamStorageService scheduleExamStorageService)
        {
            _scheduleExamStorageService = scheduleExamStorageService;
        }
        [HttpPost]
        public async Task<IActionResult> ScheduleExam(NewScheduleExamViewModel model)
        {
            var result = await _scheduleExamStorageService.AddNewScheduleExam(model);
           

            return Ok(new AddNewEntityResponseViewModel { EntityId = result });
        }

        [HttpGet]
        public async Task<List<GroupExamsModel>> GetScheduleExams()
        {
            return await _scheduleExamStorageService.GetScheduleExams();
        }
       
            

    }
}
