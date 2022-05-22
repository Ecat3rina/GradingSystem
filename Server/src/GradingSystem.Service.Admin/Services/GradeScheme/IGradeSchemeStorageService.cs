using GradingSystem.Service.Admin.ViewModels.GradeScheme;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradingSystem.Service.Admin.Services.GradeScheme
{
    public interface IGradeSchemeStorageService
    {
        Task<List<GradeSchemeViewModel>> GetGradeSchemes();
        Task<Guid> AddNewGradeScheme(NewGradeSchemeViewModel model);
        Task<GradeSchemeViewModel> GetGradeScheme(Guid id);
        Task<Guid> UpdateGradeScheme(UpdateGradeSchemeViewModel model);
        Task<Guid> DeleteGradeScheme(Guid id);
        Task<GradeSchemeViewModel> GetGradeSchemeByExamId(Guid examId);

    }
}
