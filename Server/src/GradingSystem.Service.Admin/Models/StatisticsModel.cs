using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradingSystem.Service.Admin.Models
{
    public class StatisticsModel
    {
        public string ExamName { get; set; }
        public string SubjectName { get; set; }
        public int TotalNrOfEvaluators { get; set; }
        public int NumberOfEvaluatorsPerThesis { get; set; }
        public bool WasGenerated { get; set; }
    }
}
