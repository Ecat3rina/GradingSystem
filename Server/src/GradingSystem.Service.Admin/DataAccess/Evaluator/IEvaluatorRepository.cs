using GradingSystem.Service.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradingSystem.Service.Admin.DataAccess
{
    public interface IEvaluatorRepository
    {
        Task AddEvaluator(EvaluatorModel model);
        Task<List<EvaluatorModel>> GetEvaluators();
        Task<EvaluatorModel> GetEvaluatorById(Guid id);
        Task DeleteEvaluator(Guid id);
        Task UpdateEvaluator(EvaluatorModel model);
        Task<List<EvaluatorModel>> GetEvaluatorsBySubjectId(Guid id);
    }
}
