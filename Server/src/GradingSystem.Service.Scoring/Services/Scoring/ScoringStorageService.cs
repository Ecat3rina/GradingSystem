using GradingSystem.Service.Scoring.DataAccess;
using GradingSystem.Service.Scoring.DataAccess.Repartition;
using GradingSystem.Service.Scoring.DataAccess.Scoring;
using GradingSystem.Service.Scoring.Models;
using GradingSystem.Service.Scoring.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace GradingSystem.Service.Scoring.Services.Scoring
{
    public class ScoringStorageService : IScoringStorageService
    {
        private readonly IScoringRepository _scoringRepository;
        private readonly IRepartitionRepository _repartitionRepository;
        private readonly IThesisRepository _thesisRepository;
        private readonly HttpClient _adminServiceHttpClient;

        public ScoringStorageService(IScoringRepository scoringRepository,
            IRepartitionRepository repartitionRepository,
            IThesisRepository thesisRepository,
            IHttpClientFactory httpClientFactory)
        {
            _scoringRepository = scoringRepository;
            _repartitionRepository = repartitionRepository;
            _thesisRepository = thesisRepository;
            _adminServiceHttpClient = httpClientFactory.CreateClient("Admin");
        }
        public async Task SubmitFinalScoreAsync(IEnumerable<ItemScoreViewModel> scoring)
        {
            var scoringmodels = new List<ItemScoreModel>();
            foreach (var item in scoring)
            {
                var model = new ItemScoreModel
                {
                    Id = Guid.NewGuid(),
                    EvaluationRepartitionId = item.EvaluationRepartitionId,
                    Score = item.Score,
                    Comments = item.Comments,
                    EvaluationDate = DateTime.Now,
                    ItemNumber = item.ItemNumber
                };
                scoringmodels.Add(model);
            }

            await _scoringRepository.SubmitFinalScoreAsync(scoringmodels);
            await _repartitionRepository.ChangeEvaluationStatus(scoringmodels[0].EvaluationRepartitionId);

            await ComputeFinalGradeAsync(scoringmodels[0].EvaluationRepartitionId);
            
        }

        public async Task ComputeFinalGradeAsync(Guid repartitionId)
        {
            var thesisId = await _repartitionRepository.GetThesisIdFromRepartitionAsync(repartitionId);
            var allRepartition = await _repartitionRepository.GetEvaluationRepartitionByThesisIdAsync(thesisId);
            if (!allRepartition.Any(x => x.EvaluationStatus == false))
            {
                //teza a fost verificata de toti evaluatorii
                //facem scor final si nota finala
                var allScores = await _scoringRepository.GetThesisScoringAsync(thesisId);

                var totalScore = allScores.Sum(x => x.Score);

                totalScore /= allRepartition.Count; // scorul care trebuie pus in dbo.Scoring
                //var gradationScheme
                var examId = await _thesisRepository.GetExamId(thesisId.ToString());

                //var gradingScheme = await GetGradingSchemeAsync(examId);
                using var httpRequest = new HttpRequestMessage(HttpMethod.Get, $"gradescheme/examId/{examId}");


                var httpResponse = await _adminServiceHttpClient.SendAsync(httpRequest);

                if (!httpResponse.IsSuccessStatusCode)
                {
                    var errorResponse = await httpResponse.Content.ReadAsStringAsync();
                }
                httpResponse.EnsureSuccessStatusCode();

                var result = await httpResponse.Content.ReadAsStringAsync();
                var gradingScheme = JsonSerializer.Deserialize<GradeSchemeModel>(result, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase});

                var finalGrade = 0;

                foreach (var component in gradingScheme.GradeSchemeComponents)
                {
                    if (totalScore >= component.MinimumScore && totalScore <= component.MaximumScore)
                        finalGrade = component.Grade;
                }

                var updateThesisModel = new UpdateThesisModel
                {
                    ThesisId = thesisId,
                    FinalScore = totalScore,
                    GradationDate = DateTime.Now,
                    FinalGrade = finalGrade
                };
                await _thesisRepository.UpdateThesisWithFinalDetailsAsync(updateThesisModel);
            }
        }
    }
}
