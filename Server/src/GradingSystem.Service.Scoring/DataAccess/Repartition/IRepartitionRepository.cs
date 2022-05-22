using GradingSystem.Service.Scoring.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GradingSystem.Service.Scoring.DataAccess.Repartition
{
    public interface IRepartitionRepository
    {
        Task AddEvaluationRepartition(EvaluationRepartitionModel model);
        Task<List<Guid>> GetThesesList(Guid examId);
        Task<List<EvaluationRepartitionModel>> GetEvaluationRepartitionByEvaluatorId(string evaluatorId);
        Task<List<EvaluationRepartitionModel>> GetEvaluationRepartitionByThesisIdAsync(Guid thesisId);
        Task ChangeEvaluationStatus(Guid repartitionId);
        Task<int> GetFinalScore(string repartitionId);
        Task<Guid> GetThesisIdFromRepartitionAsync(Guid repartitionId);
    }
}
