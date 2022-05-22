using System;

namespace GradingSystem.Service.Scoring.ViewModels
{
    public class RepartitionViewModel
    {
        public Guid Id { get; set; }
        public DateTime RepartitionDate { get; set; }
        public Guid ThesisId { get; set; }
        public bool EvaluationStatus { get; set; }
        public int Score { get; set; }
    }
}
