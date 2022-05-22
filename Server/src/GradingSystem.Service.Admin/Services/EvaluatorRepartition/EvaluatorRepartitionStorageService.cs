using GradingSystem.Service.Admin.DataAccess;
using GradingSystem.Service.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace GradingSystem.Service.Admin.Services
{
    public class EvaluatorRepartitionStorageService : IEvaluatorRepartitionStorageService
    {
        private readonly IEvaluatorRepartitionRepository _evaluatorRepartitionRepository;
        private readonly HttpClient _scoringServiceHttpClient;

        public EvaluatorRepartitionStorageService(IEvaluatorRepartitionRepository evaulatorRepartitionRepository,
            IHttpClientFactory httpClientFactory)
        {
            _evaluatorRepartitionRepository = evaulatorRepartitionRepository;
            _scoringServiceHttpClient= httpClientFactory.CreateClient("ScoringService");
        }

        public async Task<List<StatisticsModel>> GetStatistics()
        {
            return await _evaluatorRepartitionRepository.GetStatistics();
        }

        public async Task TriggerRepartition(string examName)
        {
            var repartitionModel=await _evaluatorRepartitionRepository.TriggerEvaluatorRepartition(examName);
           
            var httpContent = new StringContent(JsonSerializer.Serialize(repartitionModel), Encoding.UTF8, "application/json");
            using var httpRequest = new HttpRequestMessage(HttpMethod.Post, "repartition");
            httpRequest.Content = httpContent;

            var httpResponse = await _scoringServiceHttpClient.SendAsync(httpRequest);

            if(!httpResponse.IsSuccessStatusCode)
            {
                var errorResponse = await httpResponse.Content.ReadAsStringAsync();
            }
            httpResponse.EnsureSuccessStatusCode();
        }
    }
}
