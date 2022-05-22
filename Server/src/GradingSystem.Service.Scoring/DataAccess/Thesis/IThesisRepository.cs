using GradingSystem.Service.Scoring.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GradingSystem.Service.Scoring.DataAccess
{
    public interface IThesisRepository
    {
        Task UpdateThesis(ThesisModel model);
        Task<string> GetExamId(string thesisId);
        Task<IEnumerable<ThesisModel>> GetThesesForStudentAsync(Guid studentId);
        Task<ThesisModel> GetThesisByIdAsync(Guid thesisId);
        Task UpdateThesisWithFinalDetailsAsync(UpdateThesisModel model);

    }
}
