using GradingSystem.Service.Scoring.Models;
using GradingSystem.Service.Scoring.Services.Exam;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradingSystem.Service.Scoring.Controllers
{
    [ApiController]
    [Route("thesis")]
    [Authorize(Policy = Startup.DefaultPolicy)]
    public class ThesisController : ControllerBase
    {
        private readonly IThesisStorageService _thesisStorageService;
        public ThesisController(IThesisStorageService thesisStorageService)
        {
            _thesisStorageService = thesisStorageService;
        }
        [HttpPost]
        public async Task UpdateThesisAsync(ThesisModel model)
        {
            /*
             ExamId.
            StudentId.
            BlobId


            update theses
            set blobId = @BlobId where SudentId = @StudentID and ExamID = @ExamID
             */
            await _thesisStorageService.UpdateThesis(model);
        }
        [HttpGet("{thesisId}")]
        public async Task<IActionResult> GetEvaluationSchemeByThesisIdAsync(string thesisId)
        {
            var result = await _thesisStorageService.GetExamId(thesisId);
            return Ok(result);
        }
    }
}
