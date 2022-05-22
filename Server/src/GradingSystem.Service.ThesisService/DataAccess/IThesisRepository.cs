using GradingSystem.Service.ThesisService.Models;
using GradingSystem.Service.ThesisService.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradingSystem.Service.ThesisService.DataAccess
{
    public interface IThesisRepository
    {
        Task AddThesisAsync(ThesisModel model);
        Task<ThesisModel> GetThesisByExamStudentId(Guid studentId, Guid examId);
        Task<ThesisModel> GetThesisById(Guid thesisId);
        Task<List<ThesisModel>> GetThesesByStudentId(Guid id);

    }
}
