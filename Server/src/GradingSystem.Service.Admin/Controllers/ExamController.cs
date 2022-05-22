using GradingSystem.Service.Admin.Services.Exam;
using GradingSystem.Service.Admin.ViewModels;
using GradingSystem.Service.Admin.ViewModels.Exam;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradingSystem.Service.Admin.Controllers
{
    [Route("exam")]
    [ApiController]
    //[Authorize(Policy = Startup.DefaultPolicy)]
    public class ExamController : ControllerBase
    {
        private readonly IExamStorageService _examStorageService;

        public ExamController(IExamStorageService examStorageService)
        {
            _examStorageService=examStorageService;
        }

        [HttpGet]
        public async Task<List<ExamViewModel>> GetAllExams()
        {
            return await _examStorageService.GetExams();
        }

        [HttpPost]
        public async Task<IActionResult> AddExam(NewExamViewModel model)
        {
            var result = await _examStorageService.AddNewExam(model);
            return Ok(new AddNewEntityResponseViewModel { EntityId = result });
        }

        [HttpGet("GetExamsBySubjectId/{subjectId}")]
        public async Task<List<ExamViewModel>> GetExamsBySubjectId(Guid subjectId)
        {
            return await _examStorageService.GetExamsBySubjectId(subjectId);
        }

        [HttpGet("GetExamById/{examId}")]
        public async Task<IActionResult> GetExamById(Guid examId)
        {
            return Ok(await _examStorageService.GetExam(examId));
        }
        [HttpGet("GetExamNameById/{examId}")]
        public async Task<IActionResult> GetExamNameById(Guid examId)
        {
            var result = await _examStorageService.GetExam(examId);

            return Ok(result.Name);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateExam(UpdateExamViewModel model)
        {
            await _examStorageService.UpdateExam(model);
            return Ok(new UpdateEntityResponseViewModel { EntityId = model.Id });
        }

        [HttpDelete("{examId}")]
        public async Task<IActionResult> DeleteExam(Guid examId)
        {
            await _examStorageService.DeleteExam(examId);
            return Ok(new DeleteEntityResponseViewModel { EntityId = examId });
        }
    }
}
