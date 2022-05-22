using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradingSystem.Service.Admin.ViewModels.GradeScheme.GradeSchemeComponent
{
    public class NewGradeSchemeComponentViewModel
    {
        public Guid GradeSchemeId { get; set; }
        public int Grade { get; set; }
        public int MinimumScore { get; set; }
        public int MaximumScore { get; set; }
    }
}
