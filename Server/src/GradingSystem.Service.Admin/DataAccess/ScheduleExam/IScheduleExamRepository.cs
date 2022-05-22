using GradingSystem.Service.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradingSystem.Service.Admin.DataAccess.ScheduleExam
{
    public interface IScheduleExamRepository
    {
        Task AddScheduleExam(ScheduleExamModel model);
        Task<List<GroupExamsModel>> GetScheduleExams();
    }
}
