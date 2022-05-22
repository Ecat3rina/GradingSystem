using Dapper;
using GradingSystem.Service.Scoring.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradingSystem.Service.Scoring.DataAccess.Scoring
{
    public class ScoringRepository : IScoringRepository
    {
        private readonly string _scoringDbConnectionString;

        public ScoringRepository(string connectionString)
        {
            _scoringDbConnectionString = connectionString;
        }
        public async Task SubmitFinalScoreAsync(IEnumerable<ItemScoreModel> scoring)
        {
            using var connection = new SqlConnection(_scoringDbConnectionString);
            await connection.OpenAsync();
            foreach (var item in scoring)
            {
                var query = @"INSERT INTO Scores (Id, EvaluationRepartitionId, ItemNumber, Score, Comments, EvaluationDate) 
                            VALUES (@Id, @EvaluationRepartitionId, @ItemNumber, @Score, @Comments, @EvaluationDate)";
                await connection.ExecuteAsync(query, item);
            }
        }

        public async Task<IEnumerable<ItemScoreModel>> GetThesisScoringAsync(Guid thesisId)
        {
            using var connection = new SqlConnection(_scoringDbConnectionString);
            await connection.OpenAsync();
            var query = @"SELECT * FROM [Scoring].[dbo].Scores S
                        INNER JOIN [EvaluationRepartitions] ER on ER.Id = S.EvaluationRepartitionId
                        WHERE ER.ThesisId = @thesisId ";

            return await connection.QueryAsync<ItemScoreModel>(query, new { thesisId });
        }
    }
}
