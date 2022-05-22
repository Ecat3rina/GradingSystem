using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GradingSystem.Service.Admin.Models
{
    public class EvaluationSchemeModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        [Required]
        public Guid ExamId { get; set; }
        public int NumberOfItems { get; set; }
        public List<EvaluationSchemeComponentModel> EvaluationSchemeComponents { get; set; }
        //public int ItemNr { get; set; }
        //public int PageNr { get; set; }
        //public int ScoreValue { get; set; }
        //public string ScoreComment { get; set; }

    }
}
