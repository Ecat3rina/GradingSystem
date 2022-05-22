using GradingSystem.Service.Admin.Models;
using GradingSystem.Service.Admin.ViewModels.ScheduleExam;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradingSystem.Service.Admin.Services.ScheduleExam
{
    public interface IScheduleExamStorageService
    {
        Task<Guid> AddNewScheduleExam(NewScheduleExamViewModel model);
        Task<List<GroupExamsModel>> GetScheduleExams();
    }
}
