using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradingSystem.Service.Admin.Models
{
    public class GradeSchemeComponentModel
    {
        public Guid Id { get; set; }
        public Guid GradeSchemeId { get; set; }
        public int Grade { get; set; }
        public int MinimumScore { get; set; }
        public int MaximumScore { get; set; }

    }
}
