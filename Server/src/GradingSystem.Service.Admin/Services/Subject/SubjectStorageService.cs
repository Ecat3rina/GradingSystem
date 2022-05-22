using GradingSystem.Service.Admin.DataAccess.Subject;
using GradingSystem.Service.Admin.Models;
using GradingSystem.Service.Admin.ViewModels.Subject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradingSystem.Service.Admin.Services.Subject
{
    public class SubjectStorageService : ISubjectStorageService
    {
        private readonly ISubjectRepository _subjectRepository;
        public SubjectStorageService(ISubjectRepository subjectRepository)
        {
            _subjectRepository = subjectRepository;
        }
        public async Task<Guid> AddNewSubject(NewSubjectViewModel model)
        {
            var subjectModel = new SubjectModel
            {
                Id = Guid.NewGuid(),
                Name=model.Name,
                Description=model.Description
            };
            await _subjectRepository.AddSubject(subjectModel);

            return subjectModel.Id;
        }

        public async Task<Guid> DeleteSubject(Guid id)
        {
            await _subjectRepository.DeleteSubject(id);
            return id;
        }

        public async Task<SubjectViewModel> GetSubject(Guid id)
        {
            var studentModel = await _subjectRepository.GetSubjectById(id);
            var studentViewModel = new SubjectViewModel
            {
                Id = studentModel.Id,
                Name=studentModel.Name,
                Description=studentModel.Description
            };

            return studentViewModel;
        }

        public async Task<List<SubjectViewModel>> GetSubjects()
        {
            var subjectViewModels = new List<SubjectViewModel>();
            var subjectModels = await _subjectRepository.GetSubjects();

            foreach (var subjectModel in subjectModels)
            {
                var studentViewModel = new SubjectViewModel
                {
                    Id = subjectModel.Id,
                    Name=subjectModel.Name,
                    Description=subjectModel.Description
                };
                subjectViewModels.Add(studentViewModel);
            }

            return subjectViewModels;
        }

        public async Task<Guid> UpdateSubject(UpdateSubjectViewModel model)
        {
            var subjectToUpdate = new SubjectModel
            {
                Id = model.Id,
                Name=model.Name,
                Description=model.Description
            };
            await _subjectRepository.UpdateSubject(subjectToUpdate);
            return subjectToUpdate.Id;
        }
    }
}
