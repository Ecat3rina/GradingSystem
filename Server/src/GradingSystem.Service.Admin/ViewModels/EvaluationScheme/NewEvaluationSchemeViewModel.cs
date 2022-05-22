using GradingSystem.Service.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradingSystem.Service.Admin.ViewModels.EvaluationScheme
{
    public class NewEvaluationSchemeViewModel
    {
        public Guid ExamId { get; set; }
        public string Name { get; set; }
        public int NumberOfItems { get; set; }

        public List<EvaluationSchemeComponentModel> EvaluationSchemeComponents { get; set; }

        //public int ItemNr { get; set; }
        //public int PageNr { get; set; }
        //public int ScoreValue { get; set; }
        //public string ScoreComment { get; set; }
    }
}
