using GradingSystem.Service.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradingSystem.Service.Admin.DataAccess
{
    public interface IEvaluatorRepartitionRepository
    {
        Task<List<StatisticsModel>> GetStatistics();
        Task<EvaluatorRepartitionModel> TriggerEvaluatorRepartition(string examName);

    }
}
