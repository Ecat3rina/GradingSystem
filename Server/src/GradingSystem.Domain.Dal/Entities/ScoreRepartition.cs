using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistributedBACControlSystem.DAL.Entities
{
    public class ScoreRepartition
    {
        public Guid EvaluationRepartitionId { get; set; }
        public Guid EvaluationSchemeId { get; set; }
        public int FinalScore { get; set; }
        public DateTime EvaluationDate { get; set; }

    }
}
