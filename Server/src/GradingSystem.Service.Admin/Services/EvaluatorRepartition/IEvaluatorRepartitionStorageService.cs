using GradingSystem.Service.Admin.Models;
using GradingSystem.Service.Admin.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradingSystem.Service.Admin.Services
{
    public interface IEvaluatorRepartitionStorageService
    {
        Task<List<StatisticsModel>> GetStatistics();
        Task TriggerRepartition(string examName);

    }
}
