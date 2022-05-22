using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistributedBACControlSystem.DAL.Entities
{
    public class EvaluationRepartition
    {
        public Guid Id { get; set; }
        public Guid EvaluatorId { get; set; }
        public Guid ThesisPageId{ get; set; }
        public DateTime RepartitionDate { get; set; }
    }
}
