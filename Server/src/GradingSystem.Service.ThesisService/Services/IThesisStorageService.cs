using GradingSystem.Service.ThesisService.Models;
using GradingSystem.Service.ThesisService.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GradingSystem.Service.ThesisService.Services
{
    public interface IThesisStorageService
    {
        Task<Guid> AddNewThesisAsync(NewThesisViewModel model);
        Task<ThesisViewModel> GetThesisByStudentAndExamId(Guid studentId, Guid examId);
        Task<ThesisViewModel> GetThesisById(Guid thesisId);
        Task<List<ThesisViewModel>> GetThesesByStudentId(Guid id);

    }
}
