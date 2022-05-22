using Dapper;
using GradingSystem.Service.Scoring.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradingSystem.Service.Scoring.DataAccess.Repartition
{
    public class RepartitionRepository:IRepartitionRepository
    {
        private readonly string _repartitionDbConnectionString;
        public RepartitionRepository(string connectionString)
        {
            _repartitionDbConnectionString = connectionString;
        }
        public async Task<List<EvaluationRepartitionModel>> GetEvaluationRepartitionByEvaluatorId(string evaluatorId)
        {
            using var connection = new SqlConnection(_repartitionDbConnectionString);
            await connection.OpenAsync();
            var evaluationRepartitions = await connection.QueryAsync<EvaluationRepartitionModel>(@"select * from EvaluationRepartitions where EvaluatorId=@evaluatorId", new { evaluatorId = evaluatorId });
            return evaluationRepartitions.ToList();
        }
        public async Task AddEvaluationRepartition(EvaluationRepartitionModel model)
        {
            using var connection = new SqlConnection(_repartitionDbConnectionString);
            await connection.OpenAsync();
            var query = @"INSERT INTO EvaluationRepartitions (Id, EvaluatorId, ThesisId, RepartitionDate,EvaluationStatus) 
                        VALUES (@Id, @EvaluatorId, @ThesisId, @RepartitionDate, @EvaluationStatus)";
            await connection.ExecuteAsync(query, model);
        }


        public async Task<List<Guid>> GetThesesList(Guid examId)
        {
            using var connection = new SqlConnection(_repartitionDbConnectionString);
            await connection.OpenAsync();
            var thesesList = await connection.QueryAsync<Guid>(@"SELECT Id FROM Theses WHERE ExamId=@examId",new { examId=examId});
            return thesesList.ToList();
        }

        public  async Task ChangeEvaluationStatus(Guid repartitionId)
        {
            using var connection = new SqlConnection(_repartitionDbConnectionString);
            await connection.OpenAsync();
            await connection.ExecuteAsync(@"UPDATE EvaluationRepartitions SET EvaluationStatus=1 
                                            WHERE Id=@repartitionId", new { repartitionId=repartitionId });
        }

        public async Task<int> GetFinalScore(string repartitionId)
        {
            using var connection = new SqlConnection(_repartitionDbConnectionString);
            await connection.OpenAsync();
            var score = await connection.ExecuteScalarAsync<int>(@"select sum(score) TotalScore from scores
  where EvaluationRepartitionId =@repartitionId", new { repartitionId = repartitionId });
            return score;
        }

        public async Task<List<EvaluationRepartitionModel>> GetEvaluationRepartitionByThesisIdAsync(Guid thesisId)
        {
            using var connection = new SqlConnection(_repartitionDbConnectionString);
            await connection.OpenAsync();
            var evaluationRepartitions = await connection.QueryAsync<EvaluationRepartitionModel>(@"SELECT * from EvaluationRepartitions where ThesisId=@thesisId", new { thesisId });
            return evaluationRepartitions.ToList();
        }

        public async Task<Guid> GetThesisIdFromRepartitionAsync(Guid repartitionId)
        {
            using var connection = new SqlConnection(_repartitionDbConnectionString);
            await connection.OpenAsync();
            var thesisId = await connection.QueryFirstAsync<Guid>(@"SELECT ThesisId FROM EvaluationRepartitions WHERE Id=@id", new { id=repartitionId});
            return thesisId;
        }

    }
}
