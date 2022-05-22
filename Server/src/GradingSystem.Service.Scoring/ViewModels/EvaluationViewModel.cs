using System;

namespace GradingSystem.Service.Scoring.ViewModels
{
    public class EvaluationViewModel
    {
        public string ExamName { get; set; }
        public decimal FinalScore { get; set; }
        public decimal FinalGrade { get; set; }
        public Guid BlobId { get; set; }
        public Guid ThesisId { get; set; }
        public bool Status { get; set; }
       
    }
}
