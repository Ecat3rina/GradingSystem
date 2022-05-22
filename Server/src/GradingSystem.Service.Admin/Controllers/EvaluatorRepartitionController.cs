using GradingSystem.Service.Admin.Models;
using GradingSystem.Service.Admin.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradingSystem.Service.Admin.Controllers
{
    [Route("evaluatorRepartition")]
    [ApiController]
    [Authorize(Policy = Startup.DefaultPolicy)]
    public class EvaluatorRepartitionController:ControllerBase
    {
        private readonly IEvaluatorRepartitionStorageService _evaluatorRepartitionStorageService;
        public EvaluatorRepartitionController(IEvaluatorRepartitionStorageService evaluatorRepartitionStorageService)
        {
            _evaluatorRepartitionStorageService = evaluatorRepartitionStorageService;
        }
        [HttpGet]
        public async Task<List<StatisticsModel>> GetExamsStatistics()
        {
            /*
            1) ExamId => disciplina => lista de evaluatori (guids)
                        => nr. de evaluatori per teza
            2) apel la serviciul de scoring pentru generarea repartitiei (examId, [list-evaluators], nr_evaluators)

             */
            return await _evaluatorRepartitionStorageService.GetStatistics();
        }
        [HttpPost]
        public async Task TriggerEvaluatorRepartitionAsync(TriggerRepartitionRequest examName)
        {
            /*
            1) ExamId => disciplina => lista de evaluatori (guids)
                        => nr. de evaluatori per teza
            2) apel la serviciul de scoring pentru generarea repartitiei (examId, [list-evaluators], nr_evaluators)

             */

             await _evaluatorRepartitionStorageService.TriggerRepartition(examName.ExamName);
        }
    }
}
