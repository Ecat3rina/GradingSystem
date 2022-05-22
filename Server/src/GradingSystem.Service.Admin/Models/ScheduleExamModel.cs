using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradingSystem.Service.Admin.Models
{
    public class ScheduleExamModel
    {
        public Guid Id { get; set; }
        public Guid GroupId { get; set; }
        public Guid ExamId { get; set; }

    }
}
