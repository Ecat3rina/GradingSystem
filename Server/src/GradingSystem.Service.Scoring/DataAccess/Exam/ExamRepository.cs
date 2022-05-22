using Dapper;
using GradingSystem.Service.Scoring.Models;
using Microsoft.Data.SqlClient;
using System.Threading.Tasks;

namespace GradingSystem.Service.Scoring.DataAccess
{
    public class ExamRepository : IExamRepository
    {
        private readonly string _examDbConnectionString;
        public ExamRepository(string connectionString)
        {
            _examDbConnectionString = connectionString;
        }

        public async Task AddThesis(ThesisModel model)
        {
            using var connection = new SqlConnection(_examDbConnectionString);
            await connection.OpenAsync();
            var query = @"INSERT INTO Theses (Id, StudentId, ExamId, FinalScore, GradationDate, FinalGrade) VALUES (@Id, @StudentId, @ExamId, @FinalScore, @GradationDate, @FinalGrade)";
            await connection.ExecuteAsync(query, model);
        }
    }
}

