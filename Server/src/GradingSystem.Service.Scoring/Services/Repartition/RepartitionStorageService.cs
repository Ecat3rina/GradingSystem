using GradingSystem.Service.Admin.Models;
using GradingSystem.Service.Scoring.DataAccess.Repartition;
using GradingSystem.Service.Scoring.Models;
using GradingSystem.Service.Scoring.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradingSystem.Service.Scoring.Services.Repartition
{
    public class RepartitionStorageService : IRepartitionStorageService
    {
        private readonly IRepartitionRepository _repartitionRepository;

        public RepartitionStorageService(IRepartitionRepository repartitionRepository)
        {
            _repartitionRepository = repartitionRepository;

        }
        public async Task<List<RepartitionViewModel>> GetEvaluationRepartitionAsync(string evaluatorId)
        {
            var repartitions = await _repartitionRepository.GetEvaluationRepartitionByEvaluatorId(evaluatorId);
            var selectTasks =  repartitions.Select(async (x) => new RepartitionViewModel
            {
                Id = x.Id,
                EvaluationStatus = x.EvaluationStatus,
                RepartitionDate = x.RepartitionDate,
                ThesisId = x.ThesisId,
                Score = await GetFinalScore(x.Id.ToString())
            });

            await Task.WhenAll(selectTasks);
            return selectTasks.Select(x => x.Result).ToList();
        }
        public async Task GenerateRepartition(EvaluatorRepartitionModel model)
        {
            //evId thesisId
            /*
            nr de candidati (select count() from Thsesis where examId = @examId
           nr de evaluatori (din parametru)
           evaluatori per teza (din parametri)

           50 teze total x 2eval dif => 100 evaluari
           15 evaluatori => 100/15 => fiecare verifica 6-7 teze distincte

            */
            var thesesList = await _repartitionRepository.GetThesesList(model.ExamId);
            var nrOfTheses = thesesList.Count;
            var evaluatorsList = model.Evaluators;
            var nrOfEvaluatorsPerThesis = model.NrOfEvaluators;
            var totalNrOfEvaluators = model.Evaluators.Count;

            var nrOfEvaluations = nrOfTheses * nrOfEvaluatorsPerThesis;
            var nrOfThesesPerEvaluator = nrOfEvaluations / totalNrOfEvaluators;
            var nrOfThesesPerEvaluatorMod = nrOfEvaluations % totalNrOfEvaluators;
            var thesesNr = 0;
            int i = 0, j = 0, k = 0;

            for (i = 0; i < evaluatorsList.Count; i++)
            {
                for (j = thesesNr; j < nrOfTheses; j++)
                {

                    if (j == nrOfThesesPerEvaluator - 1)
                    {
                        i++;
                        thesesNr += nrOfThesesPerEvaluator - 1;
                    }

                    var evaluationRepartitionModel = new EvaluationRepartitionModel
                    {
                        Id = Guid.NewGuid(),
                        EvaluatorId = evaluatorsList[i],
                        ThesisId = thesesList[j],
                        RepartitionDate = DateTime.Now,
                        EvaluationStatus=false
                    };

                    await _repartitionRepository.AddEvaluationRepartition(evaluationRepartitionModel);
                }
            }

            if (nrOfThesesPerEvaluatorMod > 0)
            {

                for (int a = 0; a < nrOfEvaluatorsPerThesis - 1; a++)
                {
                    for (int b = 0; b < nrOfThesesPerEvaluatorMod; b++)
                    {
                        if (b == nrOfThesesPerEvaluator - 1)
                        {
                            a++;
                            b += nrOfThesesPerEvaluator - 1;
                        }
                        var evaluationRepartitionModel = new EvaluationRepartitionModel
                        {
                            Id = Guid.NewGuid(),
                            EvaluatorId = evaluatorsList[a],
                            ThesisId = thesesList[b],
                            RepartitionDate = DateTime.Now,
                            EvaluationStatus = false
                        };
                        k++;
                        await _repartitionRepository.AddEvaluationRepartition(evaluationRepartitionModel);
                    }
                }

            }

        }

        public async Task<int> GetFinalScore(string repartitionId)
        {
            return await _repartitionRepository.GetFinalScore(repartitionId);
        }
    }
}
