using GradingSystem.Service.Admin.DataAccess;
using GradingSystem.Service.Admin.Models;
using GradingSystem.Service.Admin.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradingSystem.Service.Admin.Services
{
    public class EvaluatorStorageService : IEvaluatorStorageService
    {
        private readonly IEvaluatorRepository _evaluatorRepository;
        public EvaluatorStorageService(IEvaluatorRepository evaulatorRepository)
        {
            _evaluatorRepository = evaulatorRepository;
        }
        public async Task<Guid> AddNewEvaluator(NewEvaluatorViewModel model)
        {
            var evaluatorModel = new EvaluatorModel
            {
                Id = Guid.NewGuid(),
                FirstName=model.FirstName,
                LastName=model.LastName,
                SubjectId=model.SubjectId
            };

            await _evaluatorRepository.AddEvaluator(evaluatorModel);

            return evaluatorModel.Id;
        }

        public async Task<Guid> DeleteEvaluator(Guid id)
        {
            await _evaluatorRepository.DeleteEvaluator(id);
            return id;
        }

        public async Task<EvaluatorViewModel> GetEvaluator(Guid id)
        {
            var evaluatorModel = await _evaluatorRepository.GetEvaluatorById(id);
            var evaluatorViewModel = new EvaluatorViewModel
            {
                Id = evaluatorModel.Id,
                FirstName = evaluatorModel.FirstName,
                LastName = evaluatorModel.LastName,
                SubjectId = evaluatorModel.SubjectId
            };

            return evaluatorViewModel;
        }

        public async Task<List<EvaluatorViewModel>> GetEvaluators()
        {
            var evaluatorViewModels = new List<EvaluatorViewModel>();
            var evaluatorModels= await _evaluatorRepository.GetEvaluators();

            foreach (var evaluatorModel in evaluatorModels)
            {
                var evaluatorViewModel = new EvaluatorViewModel
                {
                    Id = evaluatorModel.Id,
                    FirstName = evaluatorModel.FirstName,
                    LastName = evaluatorModel.LastName,
                    SubjectId = evaluatorModel.SubjectId
                };
                evaluatorViewModels.Add(evaluatorViewModel);
            }

            return evaluatorViewModels;
        }

        public async Task<List<EvaluatorViewModel>> GetEvaluatorsBySubjectId(Guid id)
        {
            var evaluatorViewModels = new List<EvaluatorViewModel>();
            var evaluatorModels = await _evaluatorRepository.GetEvaluatorsBySubjectId(id);

            foreach (var evaluatorModel in evaluatorModels)
            {
                var evaluatorViewModel = new EvaluatorViewModel
                {
                    Id = evaluatorModel.Id,
                    FirstName = evaluatorModel.FirstName,
                    LastName = evaluatorModel.LastName,
                    SubjectId = evaluatorModel.SubjectId
                };
                evaluatorViewModels.Add(evaluatorViewModel);
            }

            return evaluatorViewModels;
        }

        public async Task<Guid> UpdateEvaluator(UpdateEvaluatorViewModel model)
        {
            var evaluatorToUpdate = new EvaluatorModel
            {
                Id = model.Id,
                FirstName = model.FirstName,
                LastName = model.LastName,
                SubjectId = model.SubjectId
            };
            await _evaluatorRepository.UpdateEvaluator(evaluatorToUpdate);
            return evaluatorToUpdate.Id;
        }
    }
}
