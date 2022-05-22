using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradingSystem.Service.Scoring.Models
{
    public class EvaluatorScoreModel
    {
        public Guid EvaluatorId { get; set; }
        public int Score { get; set; }
        public DateTime EvaluationDate { get; set; }
    }
}
