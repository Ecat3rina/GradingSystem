using GradingSystem.Service.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradingSystem.Service.Admin.DataAccess.EvaluationScheme
{
    public interface IEvaluationSchemeRepository
    {
        Task<List<EvaluationSchemeModel>> GetEvaluationSchemes();
        Task AddEvaluationScheme(EvaluationSchemeModel model);
        Task UpdateEvaluationScheme(EvaluationSchemeModel model);
        Task<EvaluationSchemeModel> GetEvaluationSchemeById(Guid id);
        Task<EvaluationSchemeModel> GetEvaluationSchemesByExamIdAsync(Guid id);
        Task DeleteEvaluationScheme(Guid id);

    }
}
