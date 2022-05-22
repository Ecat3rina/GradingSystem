using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GradingSystem.Service.Admin.ViewModels.Exam
{
    public class NewExamViewModel
    {
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int NumberOfPages { get; set; }
        public int NumberOfEvaluators { get; set; }
        [Required]
        public Guid SubjectId { get; set; }
       // [Required]
        public Guid GradeSchemeId { get; set; }
    }
}
