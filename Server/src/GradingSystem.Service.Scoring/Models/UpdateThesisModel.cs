using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradingSystem.Service.Scoring.Models
{
    public class UpdateThesisModel
    {
        public Guid ThesisId { get; set; }
        public int FinalScore { get; set; }
        public DateTime GradationDate { get; set; }
        public decimal FinalGrade { get; set; }
    }
}
