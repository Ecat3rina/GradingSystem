using GradingSystem.Service.Scoring.DataAccess;
using GradingSystem.Service.Scoring.Models;
using GradingSystem.Service.Scoring.Models.Schedule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace GradingSystem.Service.Scoring.Services.Exam
{
    public class ThesisStorageService : IThesisStorageService
    {
        private readonly IThesisRepository _thesisRepository;

        public ThesisStorageService(IThesisRepository thesisRepository)
        {
            _thesisRepository = thesisRepository;

        }

      

        public async Task<string> GetExamId(string thesisId)
        {
            return await _thesisRepository.GetExamId(thesisId);
        }

        public async Task UpdateThesis(ThesisModel model)
        {
            await _thesisRepository.UpdateThesis(model);

        }
    }
}
