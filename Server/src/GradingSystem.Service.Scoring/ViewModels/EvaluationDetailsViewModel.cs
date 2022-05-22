using System;
using System.Collections.Generic;

namespace GradingSystem.Service.Scoring.ViewModels
{
    public class EvaluationDetailsViewModel
    {
        public Guid ThesisId { get; set; }
        public decimal FinalGrade { get; set; }
        public decimal FinalScore { get; set; }
        public string EvaluatorName { get; set; }
        public List<ItemScoreViewModel> Scores { get; set; }
        public int TotalScore { get; set; }
    }
}
