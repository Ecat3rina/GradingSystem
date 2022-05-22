using GradingSystem.Service.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradingSystem.Service.Admin.DataAccess.Exam
{
    public interface IExamRepository
    {
        Task<List<ExamModel>> GetExams();
        Task AddExam(ExamModel model);
        Task UpdateExam(ExamModel model);
        Task<ExamModel> GetExamById(Guid id);
        Task<List<ExamModel>> GetExamsBySubjectId(Guid id);
        Task DeleteExam(Guid id);
    }
}
