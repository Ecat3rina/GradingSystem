using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistributedBACControlSystem.DAL.Entities
{
    public class EvaluationScheme
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid ExamId { get; set; }
        public int ItemNr { get; set; }
        public int PageNr { get; set; }
        public int ScoreValue { get; set; }
        public string ScoreComment { get; set; }
    }
}
