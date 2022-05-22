using GradingSystem.Service.Admin.DataAccess.Student;
using GradingSystem.Service.Admin.Models;
using GradingSystem.Service.Admin.ViewModels.Student;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradingSystem.Service.Admin.Services.Student
{
    public class StudentStorageService : IStudentStorageService
    {
        private readonly IStudentRepository _studentRepository;
        public StudentStorageService(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public async Task<Guid> AddNewSudent(NewStudentViewModel model)
        {
            var studentModel = new StudentModel
            {
                Id=Guid.NewGuid(),
                FirstName=model.FirstName,
                LastName=model.LastName,
                IDNP=model.IDNP,
                Address=model.Address,
                BirthDate=model.BirthDate,
                GroupId=model.GroupId
            };
            await _studentRepository.AddStudent(studentModel);

            return studentModel.Id;
        }
        public async Task<Guid> DeleteStudent(Guid id)
        {
            await _studentRepository.DeleteStudent(id);
            return id;
        }

        public async Task<StudentViewModel> GetStudent(Guid id)
        {
            var studentModel = await _studentRepository.GetStudentById(id);
            var studentViewModel = new StudentViewModel
            {
                Id = studentModel.Id,
                FirstName = studentModel.FirstName,
                LastName = studentModel.LastName,
                IDNP = studentModel.IDNP,
                Address = studentModel.Address,
                BirthDate = studentModel.BirthDate,
                GroupId=studentModel.GroupId
            };

            return studentViewModel;
        }

        public async Task<List<StudentViewModel>> GetStudents()
        {
            var studentViewModels = new List<StudentViewModel>();
            var studentModels = await _studentRepository.GetStudents();

            foreach (var studentModel in studentModels)
            {
                var studentViewModel = new StudentViewModel
                {
                    Id = studentModel.Id,
                    FirstName = studentModel.FirstName,
                    LastName = studentModel.LastName,
                    IDNP = studentModel.IDNP,
                    Address = studentModel.Address,
                    BirthDate = studentModel.BirthDate,
                    GroupId=studentModel.GroupId
                };
                studentViewModels.Add(studentViewModel);
            }

            return studentViewModels;
        }

        public async Task<List<StudentModel>> GetStudentsByGroupId(Guid id)
        {
            return await _studentRepository.GetStudentsByGroupId(id);
        }

        public async Task<Guid> UpdateStudent(UpdateStudentViewModel model)
        {
            var studentToUpdate = new StudentModel
            {
                Id = model.Id,
                FirstName = model.FirstName,
                LastName = model.LastName,
                IDNP = model.IDNP,
                Address = model.Address,
                BirthDate = model.BirthDate,
                GroupId=model.GroupId
            };
            await _studentRepository.UpdateStudent(studentToUpdate);
            return studentToUpdate.Id;
        }
    }
}
