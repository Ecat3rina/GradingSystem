using GradingSystem.Service.Admin.Models;
using GradingSystem.Service.Scoring.Models;
using GradingSystem.Service.Scoring.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradingSystem.Service.Scoring.Services.Repartition
{
    public interface IRepartitionStorageService
    {
        Task GenerateRepartition(EvaluatorRepartitionModel model);
        Task<int> GetFinalScore(string repartitionId);

        Task<List<RepartitionViewModel>> GetEvaluationRepartitionAsync(string evaluatorId);

    }
}
