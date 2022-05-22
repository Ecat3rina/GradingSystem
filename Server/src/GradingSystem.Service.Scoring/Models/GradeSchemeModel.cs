using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradingSystem.Service.Scoring.Models
{
    public class GradeSchemeModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<GradeSchemeComponentModel> GradeSchemeComponents { get; set; }
    }
}
