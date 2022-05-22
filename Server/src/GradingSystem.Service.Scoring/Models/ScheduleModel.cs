using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradingSystem.Service.Scoring.Models.Schedule
{
    public class ScheduleModel
    {
        public Guid ExamId { get; set; }
        public List<Guid> Students { get; set; }
    }
}
