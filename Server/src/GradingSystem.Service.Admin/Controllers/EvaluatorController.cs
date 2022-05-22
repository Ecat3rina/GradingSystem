using GradingSystem.Service.Admin.Models;
using GradingSystem.Service.Admin.Services;
using GradingSystem.Service.Admin.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GradingSystem.Service.Admin.Controllers
{
    [Route("evaluator")]
    [ApiController]
    //[Authorize(Policy = Startup.DefaultPolicy)]
    public class EvaluatorController : ControllerBase
    {
        private readonly IEvaluatorStorageService _evaluatorStorageService;
        public EvaluatorController(IEvaluatorStorageService evaluatorStorageService)
        {
            _evaluatorStorageService = evaluatorStorageService;
        }

        [HttpPost]
        public async Task<IActionResult> AddEvaluator(NewEvaluatorViewModel model)
        {
            var result = await _evaluatorStorageService.AddNewEvaluator(model);
            return Ok(new AddNewEntityResponseViewModel { EntityId = result });
        }

        public async Task<List<EvaluatorViewModel>> GetAllEvaluators()
        {
            return await _evaluatorStorageService.GetEvaluators();
        }

        [HttpGet("by-subject-id/{subjectId}")]
        public async Task<List<EvaluatorViewModel>> GetEvaluatorsBySubjectId(Guid subjectId)
        {
            return await _evaluatorStorageService.GetEvaluatorsBySubjectId(subjectId);
        }

        [HttpGet("{evaluatorId}")]
        public async Task<EvaluatorViewModel> GetEvaluatorById(Guid evaluatorId)
        {
            return await _evaluatorStorageService.GetEvaluator(evaluatorId);
        }
        [HttpGet("getEvaluatorName/{evaluatorId}")]
        public async Task<IActionResult> GetEvaluatoNamerById(Guid evaluatorId)
        {
            var result= await _evaluatorStorageService.GetEvaluator(evaluatorId);
            var evaluatorName = result.FirstName + " "+result.LastName;
            return Ok(evaluatorName);
        }

        [HttpDelete("{evaluatorId}")]
        public async Task<IActionResult> DeleteEvaluator(Guid evaluatorId)
        {
            await _evaluatorStorageService.DeleteEvaluator(evaluatorId);
            return Ok(new DeleteEntityResponseViewModel { EntityId = evaluatorId });
        }

        [HttpPut]
        public async Task<IActionResult> UpdateEvaluator(UpdateEvaluatorViewModel model)
        {
            await _evaluatorStorageService.UpdateEvaluator(model);
            return Ok(new  UpdateEntityResponseViewModel{ EntityId = model.Id });
        }
    }
}
