using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradingSystem.Service.Admin.ViewModels.EvaluationScheme.EvaluationSchemeComponent
{
    public class UpdateEvaluationSchemeComponentViewModel
    {
        public Guid Id { get; set; }
        public Guid EvaluationSchemeId { get; set; }
        public int ItemNr { get; set; }
        public int PageNr { get; set; }
        public int MinimumScore { get; set; }
        public int MaximumScore { get; set; }
        public string CorrectAnswer { get; set; }
        public string Specifications { get; set; }
    }
}
