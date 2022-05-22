using GradingSystem.Service.Admin.DataAccess.GradeScheme;
using GradingSystem.Service.Admin.Models;
using GradingSystem.Service.Admin.ViewModels.GradeScheme;
using GradingSystem.Service.Admin.ViewModels.GradeScheme.GradeSchemeComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradingSystem.Service.Admin.Services.GradeScheme
{
    public class GradeSchemeStorageService : IGradeSchemeStorageService
    {
        private readonly IGradeSchemeRepository _gradeSchemeRepository;
        public GradeSchemeStorageService(IGradeSchemeRepository gradeSchemeRepository)
        {
            _gradeSchemeRepository = gradeSchemeRepository;
        }

        public async Task<List<GradeSchemeViewModel>> GetGradeSchemes()
        {
            var gradeSchemeViewModels = new List<GradeSchemeViewModel>();
            var gradeSchemeModels = await _gradeSchemeRepository.GetGradeSchemes();

            foreach (var gradeSchemeModel in gradeSchemeModels)
            {
                var gradeSchemeViewModel = new GradeSchemeViewModel
                {
                    Id = gradeSchemeModel.Id,
                    Name = gradeSchemeModel.Name
                };
                gradeSchemeViewModels.Add(gradeSchemeViewModel);
            }

            return gradeSchemeViewModels;
        }
        public async Task<Guid> DeleteGradeScheme(Guid id)
        {
            await _gradeSchemeRepository.DeleteGradeScheme(id);
            return id;
        }

        public async Task<Guid> AddNewGradeScheme(NewGradeSchemeViewModel model)
        {
            var gradeSchemeModel = new GradeSchemeModel
            {
                Id = Guid.NewGuid(),
                Name = model.Name,
            };

            foreach (var component in model.GradeSchemeComponents)
            {
                component.Id = Guid.NewGuid();
                component.GradeSchemeId = gradeSchemeModel.Id;
            }
            gradeSchemeModel.GradeSchemeComponents = model.GradeSchemeComponents;

            await _gradeSchemeRepository.AddGradeScheme(gradeSchemeModel);

            return gradeSchemeModel.Id;
        }

        public async Task<GradeSchemeViewModel> GetGradeScheme(Guid id)
        {
            var gradeSchemeModel = await _gradeSchemeRepository.GetGradeSchemeById(id);
            var gradeSchemeComponentsViewModels = new List<GradeSchemeComponentViewModel>();
            var gradeSchemeViewModel = new GradeSchemeViewModel
            {
                Id = gradeSchemeModel.Id,
                Name = gradeSchemeModel.Name,
            };

            foreach (var gradeSchemeComponent in gradeSchemeModel.GradeSchemeComponents)
            {
                var gradeSchemeComponentViewModel = new GradeSchemeComponentViewModel
                {
                    Id = gradeSchemeComponent.Id,
                    Grade = gradeSchemeComponent.Grade,
                    GradeSchemeId = gradeSchemeComponent.GradeSchemeId,
                    MinimumScore = gradeSchemeComponent.MinimumScore,
                    MaximumScore = gradeSchemeComponent.MaximumScore
                };
                gradeSchemeComponentsViewModels.Add(gradeSchemeComponentViewModel);
            }
            gradeSchemeComponentsViewModels = gradeSchemeComponentsViewModels.OrderBy(x=>x.Grade).ToList();
            gradeSchemeViewModel.GradeSchemeComponents = gradeSchemeComponentsViewModels;
          
            return gradeSchemeViewModel;
        }

        public async Task<Guid> UpdateGradeScheme(UpdateGradeSchemeViewModel model)
        {
            var gradeSchemeToUpdate = new GradeSchemeModel
            {
                Id = model.Id,
                Name = model.Name,
            };

            gradeSchemeToUpdate.GradeSchemeComponents = model.GradeSchemeComponents;
            await _gradeSchemeRepository.UpdateGradeScheme(gradeSchemeToUpdate);
            return gradeSchemeToUpdate.Id;
        }

        public async Task<GradeSchemeViewModel> GetGradeSchemeByExamId(Guid examId)
        {
            var gradeSchemeModel = await _gradeSchemeRepository.GetGradeSchemeByExamId(examId);
            var gradeSchemeComponentsViewModels = new List<GradeSchemeComponentViewModel>();
            var gradeSchemeViewModel = new GradeSchemeViewModel
            {
                Id = gradeSchemeModel.Id,
                Name = gradeSchemeModel.Name,
            };

            foreach (var gradeSchemeComponent in gradeSchemeModel.GradeSchemeComponents)
            {
                var gradeSchemeComponentViewModel = new GradeSchemeComponentViewModel
                {
                    Id = gradeSchemeComponent.Id,
                    Grade = gradeSchemeComponent.Grade,
                    GradeSchemeId = gradeSchemeComponent.GradeSchemeId,
                    MinimumScore = gradeSchemeComponent.MinimumScore,
                    MaximumScore = gradeSchemeComponent.MaximumScore
                };
                gradeSchemeComponentsViewModels.Add(gradeSchemeComponentViewModel);
            }
            gradeSchemeComponentsViewModels = gradeSchemeComponentsViewModels.OrderBy(x => x.Grade).ToList();
            gradeSchemeViewModel.GradeSchemeComponents = gradeSchemeComponentsViewModels;

            return gradeSchemeViewModel;
        }
    }
}
