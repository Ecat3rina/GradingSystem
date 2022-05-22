using GradingSystem.Service.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradingSystem.Service.Admin.DataAccess.GradeScheme
{
    public interface IGradeSchemeRepository
    {
        Task<List<GradeSchemeModel>> GetGradeSchemes();
        Task AddGradeScheme(GradeSchemeModel model);
        Task UpdateGradeScheme(GradeSchemeModel model);
        Task<GradeSchemeModel> GetGradeSchemeById(Guid id);
        Task<GradeSchemeModel> GetGradeSchemeByExamId(Guid examId);
        Task DeleteGradeScheme(Guid id);
    }
}
