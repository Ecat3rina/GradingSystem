using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradingSystem.Service.Scoring.Models
{
    public class ItemScoreModel
    {
        public Guid Id { get; set; }
        public Guid EvaluationRepartitionId { get; set; }
        public int ItemNumber { get; set; }
        public int Score { get; set; }
        public string Comments { get; set; }
        public DateTime EvaluationDate { get; set; }
    }
}
