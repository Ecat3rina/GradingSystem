using GradingSystem.Service.Scoring.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GradingSystem.Service.Scoring.DataAccess.Scoring
{
    public interface IScoringRepository
    {
        Task SubmitFinalScoreAsync(IEnumerable<ItemScoreModel> scoring);

        Task<IEnumerable<ItemScoreModel>> GetThesisScoringAsync(Guid thesisId);

    }
}
