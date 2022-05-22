using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradingSystem.Service.Admin.Models
{
    public class EvaluatorRepartitionModel
    {
        public Guid ExamId { get; set; }
        public List<Guid> Evaluators { get; set; }
        public int NrOfEvaluators { get; set; }
    }
}
