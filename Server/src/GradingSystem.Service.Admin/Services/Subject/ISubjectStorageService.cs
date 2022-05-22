using GradingSystem.Service.Admin.ViewModels.Subject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradingSystem.Service.Admin.Services.Subject
{
    public interface ISubjectStorageService
    {
        Task<Guid> AddNewSubject(NewSubjectViewModel model);
        Task<List<SubjectViewModel>> GetSubjects();
        Task<SubjectViewModel> GetSubject(Guid id);
        Task<Guid> DeleteSubject(Guid id);
        Task<Guid> UpdateSubject(UpdateSubjectViewModel model);
    }
}
