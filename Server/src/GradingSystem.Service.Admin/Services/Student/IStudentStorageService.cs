using GradingSystem.Service.Admin.Models;
using GradingSystem.Service.Admin.ViewModels.Student;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradingSystem.Service.Admin.Services.Student
{
    public interface IStudentStorageService
    {
        Task<Guid> AddNewSudent(NewStudentViewModel model);
        Task<List<StudentViewModel>> GetStudents();
        Task<StudentViewModel> GetStudent(Guid id);
        Task<List<StudentModel>> GetStudentsByGroupId(Guid id);

        Task<Guid> DeleteStudent(Guid id);
        Task<Guid> UpdateStudent(UpdateStudentViewModel model);
    }
}
