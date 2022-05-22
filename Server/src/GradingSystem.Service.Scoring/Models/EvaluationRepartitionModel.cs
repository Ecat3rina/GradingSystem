using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradingSystem.Service.Scoring.Models
{
    public class EvaluationRepartitionModel
    {
        public Guid Id { get; set; }
        public Guid EvaluatorId { get; set; }
        public DateTime RepartitionDate { get; set; }
        public Guid ThesisId { get; set; }
        public bool EvaluationStatus { get; set; }
    }
}
