using GradingSystem.Service.Admin.ViewModels.Exam;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradingSystem.Service.Admin.Services.Exam
{
    public interface IExamStorageService
    {
        Task<List<ExamViewModel>> GetExams();
        Task<Guid> AddNewExam(NewExamViewModel model);
        Task<ExamViewModel> GetExam(Guid id);
        Task<Guid> UpdateExam(UpdateExamViewModel model);
        Task<List<ExamViewModel>> GetExamsBySubjectId(Guid id);
        Task<Guid> DeleteExam(Guid id);
    }
}
