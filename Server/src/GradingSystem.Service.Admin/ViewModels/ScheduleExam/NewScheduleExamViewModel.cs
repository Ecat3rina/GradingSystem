using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradingSystem.Service.Admin.ViewModels.ScheduleExam
{
    public class NewScheduleExamViewModel
    {
        public Guid GroupId { get; set; }
        public Guid ExamId { get; set; }
    }
}
