using GradingSystem.Service.Admin.ViewModels.GradeScheme.GradeSchemeComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradingSystem.Service.Admin.ViewModels.GradeScheme
{
    public class GradeSchemeViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<GradeSchemeComponentViewModel> GradeSchemeComponents { get; set; }

        //public int Grade { get; set; }
        //public int MinimumScore { get; set; }
        //public int MaximumScore { get; set; }
    }
}
