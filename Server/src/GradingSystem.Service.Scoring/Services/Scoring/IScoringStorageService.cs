using GradingSystem.Service.Scoring.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradingSystem.Service.Scoring.Services.Scoring
{
    public interface IScoringStorageService
    {
        Task SubmitFinalScoreAsync(IEnumerable<ItemScoreViewModel> scoring);
    }
}
