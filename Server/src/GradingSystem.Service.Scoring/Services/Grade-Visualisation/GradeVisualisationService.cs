using GradingSystem.Service.Scoring.DataAccess;
using GradingSystem.Service.Scoring.DataAccess.Repartition;
using GradingSystem.Service.Scoring.DataAccess.Scoring;
using GradingSystem.Service.Scoring.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace GradingSystem.Service.Scoring.Services.Grade_Visualisation
{
    public class GradeVisualisationService : IGradeVisualisationService
    {
        private readonly IThesisRepository _thesisRepository;
        private readonly IScoringRepository _scoringRepository;
        private readonly HttpClient _adminServiceHttpClient;
        private readonly IRepartitionRepository _repartitionRepository;

        public GradeVisualisationService(IThesisRepository thesisRepository,
            IRepartitionRepository repartitionRepository,
            IScoringRepository scoringRepository,
            IHttpClientFactory httpClientFactory)
        {
            _thesisRepository = thesisRepository;
            _repartitionRepository = repartitionRepository;
            _scoringRepository = scoringRepository;
            _adminServiceHttpClient = httpClientFactory.CreateClient("Admin");
        }

        public async Task<IEnumerable<EvaluationDetailsViewModel>> GetThesisEvaluationDetailsAsync(Guid thesisId)
        {
            var result = new List<EvaluationDetailsViewModel>();

            var evaluatorRepartitions = await _repartitionRepository.GetEvaluationRepartitionByThesisIdAsync(thesisId);
            var scores = await _scoringRepository.GetThesisScoringAsync(thesisId);
            foreach (var repartition in evaluatorRepartitions)
            {
                var thesis = await _thesisRepository.GetThesisByIdAsync(repartition.ThesisId);

                var details = new EvaluationDetailsViewModel
                {
                    EvaluatorName = await GetEvaluatorNameAsync(repartition.EvaluatorId.ToString()), //todo get real name
                    FinalGrade = thesis.FinalGrade ?? 0,
                    FinalScore = thesis.FinalScore ?? 0,
                    ThesisId = repartition.ThesisId,
                    Scores = scores.Where(x => x.EvaluationRepartitionId == repartition.Id).Select(x => new ItemScoreViewModel
                    {
                        ItemNumber = x.ItemNumber,
                        Score = x.Score,
                        Comments = x.Comments,
                        EvaluationRepartitionId = x.EvaluationRepartitionId
                    }).ToList(),
                   TotalScore=scores.Where(x => x.EvaluationRepartitionId == repartition.Id).Select(x=>x.Score).Sum()
                };

                result.Add(details);
            }

            return result;
        }
        public async Task<IEnumerable<EvaluationViewModel>> GetStudentEvaluationSummaryAsync(Guid studentId)
        {
            var theses = await _thesisRepository.GetThesesForStudentAsync(studentId);
            var result = new List<EvaluationViewModel>();
            foreach (var thesis in theses)
            {
                var evaluatorRepartitions = await _repartitionRepository.GetEvaluationRepartitionByThesisIdAsync(thesis.Id);
                var finalEvaluationStatus = evaluatorRepartitions.Any() && !evaluatorRepartitions.Any(x => x.EvaluationStatus == false);
                var examName = await GetExamNameAsync(thesis.ExamId);
                var finalScore = new EvaluationViewModel
                {
                    BlobId = thesis.BlobId.Value,
                    FinalGrade = thesis.FinalGrade ?? 0,
                    FinalScore = thesis.FinalScore ?? 0,
                    ExamName = examName,
                    Status = finalEvaluationStatus,
                    ThesisId = thesis.Id
                };
                result.Add(finalScore);
            }
            
            return result;
        }

        public async Task<string> GetExamNameAsync(string examId)
        {
           
            using var httpRequest = new HttpRequestMessage(HttpMethod.Get, $"exam/GetExamNameById/{examId}");
          

            var httpResponse = await _adminServiceHttpClient.SendAsync(httpRequest);

            if (!httpResponse.IsSuccessStatusCode)
            {
                var errorResponse = await httpResponse.Content.ReadAsStringAsync();
            }
            httpResponse.EnsureSuccessStatusCode();

            return await httpResponse.Content.ReadAsStringAsync();

        }
        public async Task<string> GetEvaluatorNameAsync(string evaluatorId)
        {
            using var httpRequest = new HttpRequestMessage(HttpMethod.Get, $"evaluator/getEvaluatorName/{evaluatorId}");


            var httpResponse = await _adminServiceHttpClient.SendAsync(httpRequest);

            if (!httpResponse.IsSuccessStatusCode)
            {
                var errorResponse = await httpResponse.Content.ReadAsStringAsync();
            }
            httpResponse.EnsureSuccessStatusCode();

            return await httpResponse.Content.ReadAsStringAsync();

        }
    }
}
