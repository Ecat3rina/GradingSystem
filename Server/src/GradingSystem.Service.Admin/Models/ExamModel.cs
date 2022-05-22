using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradingSystem.Service.Admin.Models
{
    public class ExamModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int NumberOfPages { get; set; }
        public int NumberOfEvaluators { get; set; }
        public Guid SubjectId { get; set; }
        public Guid GradeSchemeId { get; set; }
        public bool WasGenerated { get; set; }
    }
}
