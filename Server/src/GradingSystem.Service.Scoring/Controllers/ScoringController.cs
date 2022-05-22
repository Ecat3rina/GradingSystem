using GradingSystem.Service.Scoring.Services.Scoring;
using GradingSystem.Service.Scoring.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GradingSystem.Service.Scoring.Controllers
{
    [ApiController]
    [Route("scoring")]
    [Authorize(Policy =Startup.DefaultPolicy)]
    public class ScoringController : ControllerBase
    {
        private readonly IScoringStorageService _scoringStorageService;
        public ScoringController(IScoringStorageService scoringStorageService)
        {
            _scoringStorageService = scoringStorageService;
        }
        [HttpPost]
        public async Task<IActionResult> SubmitScoringAsync(IEnumerable<ItemScoreViewModel> scoring)
        {
            await _scoringStorageService.SubmitFinalScoreAsync(scoring);
            return Ok();
        }
    }
}
