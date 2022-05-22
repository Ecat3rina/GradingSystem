using GradingSystem.Service.Admin.ViewModels.EvaluationScheme;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradingSystem.Service.Admin.Services.EvaluationScheme
{
    public interface IEvaluationSchemeStorageService
    {
        Task<Guid> AddNewEvaluationScheme(NewEvaluationSchemeViewModel model);
        Task<EvaluationSchemeViewModel> GetEvaluationScheme(Guid id);
        Task<List<EvaluationSchemeViewModel>> GetEvaluationSchemes();
        Task<Guid> UpdateEvaluationScheme(UpdateEvaluationSchemeViewModel model);
        Task<EvaluationSchemeViewModel> GetEvaluationSchemesByExamIdAsync(Guid id);
        Task<Guid> DeleteEvaluationScheme(Guid id);
    }
}
