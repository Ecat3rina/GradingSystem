using GradingSystem.Service.Admin.Models;
using GradingSystem.Service.Admin.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradingSystem.Service.Admin.Services
{
    public interface IEvaluatorStorageService
    {
        Task<Guid> AddNewEvaluator(NewEvaluatorViewModel model);
        Task<List<EvaluatorViewModel>> GetEvaluators();
        Task<EvaluatorViewModel> GetEvaluator(Guid id);
        Task<Guid> DeleteEvaluator(Guid id);
        Task<Guid> UpdateEvaluator(UpdateEvaluatorViewModel model);
        Task<List<EvaluatorViewModel>> GetEvaluatorsBySubjectId(Guid id);
    }
}
