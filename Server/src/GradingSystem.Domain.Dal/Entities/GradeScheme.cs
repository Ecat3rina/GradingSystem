using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistributedBACControlSystem.DAL.Entities
{
    public class GradeScheme
    {
        public Guid Id { get; set; }
        public Guid ExamId { get; set; }
        public decimal Grade { get; set; }
        public int MinimumScore { get; set; }
        public int MaximumScore { get; set; }

    }
}
