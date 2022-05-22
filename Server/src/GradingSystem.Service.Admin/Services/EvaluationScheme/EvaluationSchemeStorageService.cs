using GradingSystem.Service.Admin.DataAccess.EvaluationScheme;
using GradingSystem.Service.Admin.Models;
using GradingSystem.Service.Admin.ViewModels.EvaluationScheme;
using GradingSystem.Service.Admin.ViewModels.EvaluationScheme.EvaluationSchemeComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradingSystem.Service.Admin.Services.EvaluationScheme
{
    public class EvaluationSchemeStorageService : IEvaluationSchemeStorageService
    {
        private readonly IEvaluationSchemeRepository _evaluationSchemeRepository;
        public EvaluationSchemeStorageService(IEvaluationSchemeRepository evaluationSchemeRepository)
        {
            _evaluationSchemeRepository = evaluationSchemeRepository;
        }
        public async Task<Guid> AddNewEvaluationScheme(NewEvaluationSchemeViewModel model)
        {
            var evaluationSchemeModel = new EvaluationSchemeModel
            {
                Id = Guid.NewGuid(),
                Name = model.Name,
                ExamId=model.ExamId,
                NumberOfItems=model.NumberOfItems
            };

            foreach (var component in model.EvaluationSchemeComponents)
            {
                component.Id = Guid.NewGuid();
                component.EvaluationSchemeId = evaluationSchemeModel.Id;
            }
            evaluationSchemeModel.EvaluationSchemeComponents = model.EvaluationSchemeComponents;

            await _evaluationSchemeRepository.AddEvaluationScheme(evaluationSchemeModel);

            return evaluationSchemeModel.Id;
        }

        public async Task<Guid> DeleteEvaluationScheme(Guid id)
        {
            await _evaluationSchemeRepository.DeleteEvaluationScheme(id);
            return id;
        }

        public async Task<EvaluationSchemeViewModel> GetEvaluationScheme(Guid id)
        {
            var evaluationSchemeModel = await _evaluationSchemeRepository.GetEvaluationSchemeById(id);
            var evaluationSchemeComponentViewModels = new List<EvaluationSchemeComponentViewModel>();
            var evaluationSchemeViewModel = new EvaluationSchemeViewModel
            {
                Id = evaluationSchemeModel.Id,
                Name = evaluationSchemeModel.Name,
                ExamId=evaluationSchemeModel.ExamId,
                NumberOfItems=evaluationSchemeModel.NumberOfItems
            };

            foreach (var evaluationSchemeComponent in evaluationSchemeModel.EvaluationSchemeComponents)
            {
                var evaluationSchemeComponentViewModel = new EvaluationSchemeComponentViewModel
                {
                    Id = evaluationSchemeComponent.Id,
                    EvaluationSchemeId = evaluationSchemeComponent.EvaluationSchemeId,
                    ItemNr = evaluationSchemeComponent.ItemNr,
                    PageNr = evaluationSchemeComponent.PageNr,
                    MinimumScore = evaluationSchemeComponent.MinimumScore,
                    MaximumScore = evaluationSchemeComponent.MaximumScore,
                    CorrectAnswer = evaluationSchemeComponent.CorrectAnswer,
                    Specifications = evaluationSchemeComponent.Specifications
                };
                evaluationSchemeComponentViewModels.Add(evaluationSchemeComponentViewModel);
            }
            evaluationSchemeComponentViewModels = evaluationSchemeComponentViewModels.OrderBy(x=>x.ItemNr).ToList();
            evaluationSchemeViewModel.EvaluationSchemeComponents = evaluationSchemeComponentViewModels;
            return evaluationSchemeViewModel;
        }

        public async Task<List<EvaluationSchemeViewModel>> GetEvaluationSchemes()
        {
            var evaluationSchemesViewModels = new List<EvaluationSchemeViewModel>();
            var evaluationSchemesModels = await _evaluationSchemeRepository.GetEvaluationSchemes();

            foreach (var evaluationSchemeModel in evaluationSchemesModels)
            {
                var evaluationSchemeViewModel = new EvaluationSchemeViewModel
                {
                    Id = evaluationSchemeModel.Id,
                    Name=evaluationSchemeModel.Name,
                    ExamId=evaluationSchemeModel.ExamId,
                    NumberOfItems=evaluationSchemeModel.NumberOfItems
                };
                evaluationSchemesViewModels.Add(evaluationSchemeViewModel);
            }

            return evaluationSchemesViewModels;
        }

       

        public async Task<EvaluationSchemeViewModel> GetEvaluationSchemesByExamIdAsync(Guid id)
        {
            var evaluationSchemeModel = await _evaluationSchemeRepository.GetEvaluationSchemesByExamIdAsync(id);
            var evaluationSchemeComponentViewModels = new List<EvaluationSchemeComponentViewModel>();
            var evaluationSchemeViewModel = new EvaluationSchemeViewModel
            {
                Id = evaluationSchemeModel.Id,
                Name = evaluationSchemeModel.Name,
                ExamId = evaluationSchemeModel.ExamId,
                NumberOfItems = evaluationSchemeModel.NumberOfItems
            };

            foreach (var evaluationSchemeComponent in evaluationSchemeModel.EvaluationSchemeComponents)
            {
                var evaluationSchemeComponentViewModel = new EvaluationSchemeComponentViewModel
                {
                    Id = evaluationSchemeComponent.Id,
                    EvaluationSchemeId = evaluationSchemeComponent.EvaluationSchemeId,
                    ItemNr = evaluationSchemeComponent.ItemNr,
                    PageNr = evaluationSchemeComponent.PageNr,
                    MinimumScore = evaluationSchemeComponent.MinimumScore,
                    MaximumScore = evaluationSchemeComponent.MaximumScore,
                    CorrectAnswer = evaluationSchemeComponent.CorrectAnswer,
                    Specifications = evaluationSchemeComponent.Specifications
                };
                evaluationSchemeComponentViewModels.Add(evaluationSchemeComponentViewModel);
            }
            evaluationSchemeComponentViewModels = evaluationSchemeComponentViewModels.OrderBy(x => x.ItemNr).ToList();
            evaluationSchemeViewModel.EvaluationSchemeComponents = evaluationSchemeComponentViewModels;
            return evaluationSchemeViewModel;
        }

        public async Task<Guid> UpdateEvaluationScheme(UpdateEvaluationSchemeViewModel model)
        {
            var evaluationSchemeToUpdate = new EvaluationSchemeModel
            {
                Id = model.Id,
                Name = model.Name,
                ExamId=model.ExamId,
                NumberOfItems=model.NumberOfItems
            };
            evaluationSchemeToUpdate.EvaluationSchemeComponents = model.EvaluationSchemeComponents;
            await _evaluationSchemeRepository.UpdateEvaluationScheme(evaluationSchemeToUpdate);
            return evaluationSchemeToUpdate.Id;
        }
    }
}
