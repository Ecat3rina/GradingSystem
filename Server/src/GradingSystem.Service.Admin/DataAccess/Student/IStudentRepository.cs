using GradingSystem.Service.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradingSystem.Service.Admin.DataAccess.Student
{
    public interface IStudentRepository
    {
        Task AddStudent(StudentModel model);
        Task<List<StudentModel>> GetStudents();
        Task<StudentModel> GetStudentById(Guid id);
        Task<List<StudentModel>> GetStudentsByGroupId(Guid id);

        Task DeleteStudent(Guid id);
        Task UpdateStudent(StudentModel model);
    }
}
