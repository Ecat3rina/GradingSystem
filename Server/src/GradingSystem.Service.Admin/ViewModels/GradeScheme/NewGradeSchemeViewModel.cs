using GradingSystem.Service.Admin.Models;
using GradingSystem.Service.Admin.ViewModels.GradeScheme.GradeSchemeComponent;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GradingSystem.Service.Admin.ViewModels.GradeScheme
{
    public class NewGradeSchemeViewModel
    {
        public string Name { get; set; }
        public List<GradeSchemeComponentModel> GradeSchemeComponents { get; set; }

        //public int Grade { get; set; }
        //public int MinimumScore { get; set; }
        //public int MaximumScore { get; set; }
    }
}
