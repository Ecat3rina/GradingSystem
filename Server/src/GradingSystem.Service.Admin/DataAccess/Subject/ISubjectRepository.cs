using GradingSystem.Service.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradingSystem.Service.Admin.DataAccess.Subject
{
    public interface ISubjectRepository
    {
        Task AddSubject(SubjectModel model);
        Task<List<SubjectModel>> GetSubjects();
        Task<SubjectModel> GetSubjectById(Guid id);
        Task DeleteSubject(Guid id);
        Task UpdateSubject(SubjectModel model);
    }
}
