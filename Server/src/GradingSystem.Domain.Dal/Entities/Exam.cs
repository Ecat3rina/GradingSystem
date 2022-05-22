using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistributedBACControlSystem.DAL.Entities
{
    public class Exam
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int NumberOfPages { get; set; }
        public int NumberOfEvaluators { get; set; }
        public Guid SubjectId { get; set; }
        public Guid EvaluationSchemeId { get; set; }

    }
}
