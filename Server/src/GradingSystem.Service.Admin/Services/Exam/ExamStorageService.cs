using GradingSystem.Service.Admin.DataAccess.Exam;
using GradingSystem.Service.Admin.Models;
using GradingSystem.Service.Admin.ViewModels.Exam;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradingSystem.Service.Admin.Services.Exam
{
    public class ExamStorageService : IExamStorageService
    {
        private readonly IExamRepository _examRepository;
        public ExamStorageService(IExamRepository examRepository)
        {
            _examRepository = examRepository;
        }
        public async Task<Guid> AddNewExam(NewExamViewModel model)
        {
            var examModel = new ExamModel
            {
                Id = Guid.NewGuid(),
                Name=model.Name,
                StartDate=model.StartDate,
                EndDate=model.EndDate,
                NumberOfPages=model.NumberOfPages,
                NumberOfEvaluators=model.NumberOfEvaluators,
                SubjectId=model.SubjectId,
                GradeSchemeId=model.GradeSchemeId,
                WasGenerated=false
            };

            await _examRepository.AddExam(examModel);

            return examModel.Id;
        }

        public async Task<Guid> DeleteExam(Guid id)
        {
            await _examRepository.DeleteExam(id);
            return id;
        }

        public async Task<List<ExamViewModel>> GetExams()
        {
            var examViewModels = new List<ExamViewModel>();
            var examModels = await _examRepository.GetExams();

            foreach (var examModel in examModels)
            {
                var examViewModel = new ExamViewModel
                {
                    Id = examModel.Id,
                    Name=examModel.Name,
                    StartDate = examModel.StartDate,
                    EndDate = examModel.EndDate,
                    NumberOfEvaluators = examModel.NumberOfEvaluators,
                    NumberOfPages = examModel.NumberOfPages,
                    SubjectId = examModel.SubjectId,
                    GradeSchemeId=examModel.GradeSchemeId,
                };
                examViewModels.Add(examViewModel);
            }

            return examViewModels;
        }

        public async Task<ExamViewModel> GetExam(Guid id)
        {
            var examModel = await _examRepository.GetExamById(id);
            var examViewModel = new ExamViewModel
            {
                Id = examModel.Id,
                Name=examModel.Name,
                StartDate = examModel.StartDate,
                EndDate = examModel.EndDate,
                NumberOfPages = examModel.NumberOfPages,
                NumberOfEvaluators = examModel.NumberOfEvaluators,
                SubjectId = examModel.SubjectId,
                GradeSchemeId = examModel.GradeSchemeId
            };

            return examViewModel;
        }

        public async Task<List<ExamViewModel>> GetExamsBySubjectId(Guid id)
        {
            var examViewModels = new List<ExamViewModel>();
            var examModels = await _examRepository.GetExamsBySubjectId(id);

            foreach (var examModel in examModels)
            {
                var evaluationSchemeViewModel = new ExamViewModel
                {
                    Id = examModel.Id,
                    Name=examModel.Name,
                    StartDate = examModel.StartDate,
                    EndDate = examModel.EndDate,
                    NumberOfPages = examModel.NumberOfPages,
                    NumberOfEvaluators = examModel.NumberOfEvaluators,
                    SubjectId = examModel.SubjectId,
                    GradeSchemeId = examModel.GradeSchemeId
                };
                examViewModels.Add(evaluationSchemeViewModel);
            }

            return examViewModels;
        }

        public async Task<Guid> UpdateExam(UpdateExamViewModel model)
        {
            var examToUpdate = new ExamModel
            {
                Id = model.Id,
                Name=model.Name,
                StartDate = model.StartDate,
                EndDate = model.EndDate,
                NumberOfPages = model.NumberOfPages,
                NumberOfEvaluators = model.NumberOfEvaluators,
                SubjectId = model.SubjectId,
                GradeSchemeId = model.GradeSchemeId,
            };
            await _examRepository.UpdateExam(examToUpdate);
            return examToUpdate.Id;
        }
    }
}
