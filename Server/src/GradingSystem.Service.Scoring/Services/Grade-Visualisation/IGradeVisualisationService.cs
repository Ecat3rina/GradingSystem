using GradingSystem.Service.Scoring.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GradingSystem.Service.Scoring.Services.Grade_Visualisation
{
    public interface IGradeVisualisationService
    {
        Task<IEnumerable<EvaluationViewModel>> GetStudentEvaluationSummaryAsync(Guid studentId);
        Task<IEnumerable<EvaluationDetailsViewModel>> GetThesisEvaluationDetailsAsync(Guid thesisId);
    }
}
