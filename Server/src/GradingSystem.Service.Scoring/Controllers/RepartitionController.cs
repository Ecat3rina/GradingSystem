using GradingSystem.Service.Admin.Models;
using GradingSystem.Service.Scoring.Services.Repartition;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradingSystem.Service.Scoring.Controllers
{
    [Route("repartition")]
    [ApiController]
    //[Authorize(Policy = Startup.DefaultPolicy)]
    public class RepartitionController : ControllerBase
    {
        private readonly IRepartitionStorageService _repartitionStorageService;
        public RepartitionController(IRepartitionStorageService repartitionStorageService)
        {
            _repartitionStorageService = repartitionStorageService;
        }

        [HttpPost]
        public async Task GenerateRepartitionAsync(EvaluatorRepartitionModel model)
        {
            await _repartitionStorageService.GenerateRepartition(model);
        }

        [HttpGet("GetRepartition/{evaluatorId}")]
        public async Task<IActionResult> GetRepartition(string evaluatorId)
        {
            var result = await _repartitionStorageService.GetEvaluationRepartitionAsync(evaluatorId);
            return Ok(result);
        }
        [HttpGet("{repartitionId}")]
        public async Task<IActionResult> GetRepartitionScore(string repartitionId)
        {
            var result = await _repartitionStorageService.GetFinalScore(repartitionId);
            return Ok(result);
        }
    }
}
