using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradingSystem.Service.Admin.Models
{
    public class GradeSchemeModel
    {
        //public Guid Id { get; set; }
        //public Guid  ExamId { get; set; }
        //public decimal Grade { get; set; }
        //public int MinimumScore { get; set; }
        //public int MaximumScore { get; set; }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<GradeSchemeComponentModel> GradeSchemeComponents { get; set; }

    }
}
