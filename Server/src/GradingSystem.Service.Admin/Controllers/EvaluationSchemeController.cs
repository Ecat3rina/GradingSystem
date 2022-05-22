using GradingSystem.Service.Admin.Services.EvaluationScheme;
using GradingSystem.Service.Admin.ViewModels;
using GradingSystem.Service.Admin.ViewModels.EvaluationScheme;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradingSystem.Service.Admin.Controllers
{
    [Route("evaluationscheme")]
    [ApiController]
    [Authorize(Policy = Startup.DefaultPolicy)]
    public class EvaluationSchemeController : ControllerBase
    {
        private readonly IEvaluationSchemeStorageService _evaluationSchemeStorageService;

        public EvaluationSchemeController(IEvaluationSchemeStorageService evaluationSchemeStorageService)
        {
            _evaluationSchemeStorageService = evaluationSchemeStorageService;
        }

        [HttpGet]
        public async Task<List<EvaluationSchemeViewModel>> GetAllEvaluationSchemes()
        {
            return await _evaluationSchemeStorageService.GetEvaluationSchemes();
        }

        [HttpPost]
        public async Task<IActionResult> AddEvaluationScheme(NewEvaluationSchemeViewModel model)
        {
            var result = await _evaluationSchemeStorageService.AddNewEvaluationScheme(model);
            return Ok(new AddNewEntityResponseViewModel { EntityId = result });
        }

        [HttpGet("examId/{examId}")]
        public async Task<IActionResult> GetEvaluationSchemesByExamId(Guid examId)
        {
            var result = await _evaluationSchemeStorageService.GetEvaluationSchemesByExamIdAsync(examId);
            return Ok(result);
        }

        [HttpGet("GetEvaluationSchemesById/{evaluationSchemeId}")]
        public async Task<EvaluationSchemeViewModel> GetEvaluationSchemesById(Guid evaluationSchemeId)
        {
            return await _evaluationSchemeStorageService.GetEvaluationScheme(evaluationSchemeId);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateEvaluationScheme(UpdateEvaluationSchemeViewModel model)
        {
            await _evaluationSchemeStorageService.UpdateEvaluationScheme(model);
            return Ok(new UpdateEntityResponseViewModel { EntityId = model.Id });
        }

        [HttpDelete("{evaluationSchemeId}")]
        public async Task<IActionResult> DeleteEvaluationScheme(Guid evaluationSchemeId)
        {
            await _evaluationSchemeStorageService.DeleteEvaluationScheme(evaluationSchemeId);
            return Ok(new DeleteEntityResponseViewModel { EntityId = evaluationSchemeId });
        }
    }
}
