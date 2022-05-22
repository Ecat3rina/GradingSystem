using Dapper;
using GradingSystem.Service.Admin.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradingSystem.Service.Admin.DataAccess
{
    public class EvaluatorRepository : IEvaluatorRepository
    {
        private readonly string _evaluatorDbConnectionString;
        public EvaluatorRepository(string connectionString)
        {
            _evaluatorDbConnectionString = connectionString;
        }

        public async Task AddEvaluator(EvaluatorModel model)
        {
            using var connection = new SqlConnection(_evaluatorDbConnectionString);
            await connection.OpenAsync();
            var query = @"INSERT INTO Evaluators (Id, FirstName, LastName, SubjectId) VALUES (@Id, @FirstName, @LastName, @SubjectId)";
            await connection.ExecuteAsync(query, model);
        }

        public async Task DeleteEvaluator(Guid id)
        {
            using var connection = new SqlConnection(_evaluatorDbConnectionString);
            await connection.OpenAsync();
            await connection.ExecuteAsync(@"DELETE FROM Evaluators WHERE Id=@evaluatorId", new { evaluatorId = id });
        }

        public async Task<EvaluatorModel> GetEvaluatorById(Guid id)
        {
            using var connection = new SqlConnection(_evaluatorDbConnectionString);
            await connection.OpenAsync();
            var evaluator = connection.QueryFirst<EvaluatorModel>(@"SELECT * FROM Evaluators WHERE Id=@evaluatorId",
                new { evaluatorId = id });
            return evaluator;
        }

        public async Task<List<EvaluatorModel>> GetEvaluators()
        {
            using var connection = new SqlConnection(_evaluatorDbConnectionString);
            await connection.OpenAsync();
            var evaluatorList = await connection.QueryAsync<EvaluatorModel>(@"SELECT * FROM Evaluators");
            return evaluatorList.ToList();

        }

        public async Task<List<EvaluatorModel>> GetEvaluatorsBySubjectId(Guid id)
        {
            using var connection = new SqlConnection(_evaluatorDbConnectionString);
            await connection.OpenAsync();
            var evaluatorList = await connection.QueryAsync<EvaluatorModel>(@"SELECT * FROM Evaluators WHERE SubjectId=@subjectId",
                new { subjectId=id});
            return evaluatorList.ToList();
        }

        public async Task UpdateEvaluator(EvaluatorModel model)
        {
            using var connection = new SqlConnection(_evaluatorDbConnectionString);
            await connection.OpenAsync();
            await connection.ExecuteAsync(@"UPDATE Evaluators SET FirstName=@firstName, LastName=@lastName, SubjectId=@subjectId WHERE Id=@evaluatorId",
                new { firstName=model.FirstName, lastName=model.LastName, subjectId = model.SubjectId, evaluatorId=model.Id });
        }
    }
}
