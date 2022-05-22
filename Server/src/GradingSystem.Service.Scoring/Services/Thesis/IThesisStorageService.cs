using GradingSystem.Service.Scoring.Models;
using GradingSystem.Service.Scoring.Models.Schedule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradingSystem.Service.Scoring.Services.Exam
{
    public interface IThesisStorageService
    {
        Task UpdateThesis(ThesisModel model);
        Task<string> GetExamId(string thesisId);
    }
}
