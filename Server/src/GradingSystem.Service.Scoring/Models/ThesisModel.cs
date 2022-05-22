using System;

namespace GradingSystem.Service.Scoring.Models
{
    public class ThesisModel
    {
        public Guid Id { get; set; }
        public Guid StudentId { get; set; }
        public string ExamId { get; set; }
        public int? FinalScore { get; set; }
        public DateTime? GradationDate { get; set; }
        public decimal? FinalGrade { get; set; }
        public Guid? BlobId { get; set; }

    }
}
