using GradingSystem.Service.Scoring.Models.Schedule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradingSystem.Service.Scoring.Services.Exam
{
    public interface IExamStorageService
    {
        Task ScheduleExams(ScheduleModel model);
    }
}
