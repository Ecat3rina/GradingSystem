using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradingSystem.Service.Scoring.Models
{
    public class StudentRepartitionModel
    {
        public string ExamId  { get; set; }
        public int FinalScore { get; set; }
        public int FinalGrade { get; set; }
        public Guid BlobId { get; set; }
        public List<EvaluatorScoreModel> EvaluatorsScores { get; set; }
    }
}
