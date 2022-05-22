using System;

namespace GradingSystem.Service.Scoring.ViewModels
{
    public class ItemScoreViewModel
    {
        public Guid EvaluationRepartitionId { get; set; }
        public int ItemNumber { get; set; }
        public int Score { get; set; }
        public string Comments { get; set; }
    }
}
