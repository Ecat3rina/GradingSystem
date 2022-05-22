using GradingSystem.Service.Scoring.Services.Grade_Visualisation;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace GradingSystem.Service.Scoring.Controllers
{
    [ApiController]
    [Route("grade-visualisation")]
    public class GradeVisualisationController : ControllerBase
    {
        private readonly IGradeVisualisationService _gradeVisualisationService;
        public GradeVisualisationController(IGradeVisualisationService gradeVisualisationService)
        {
            _gradeVisualisationService = gradeVisualisationService;
        }

        [HttpGet("{studentId}")]
        public async Task<IActionResult> GetStudentEvaluationSummaryAsync(Guid studentId)
        {
            var grades = await _gradeVisualisationService.GetStudentEvaluationSummaryAsync(studentId);
            return Ok(grades);
        }

        [HttpGet("details/{thesisId}")]
        public async Task<IActionResult> GetThesisEvaluationDetailsAsync(Guid thesisId)
        {
            var evaluationDetails = await _gradeVisualisationService.GetThesisEvaluationDetailsAsync(thesisId);
            return Ok(evaluationDetails);
        }
    }
}
