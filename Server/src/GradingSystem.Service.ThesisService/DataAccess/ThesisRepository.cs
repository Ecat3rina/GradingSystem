using Dapper;
using GradingSystem.Service.ThesisService.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradingSystem.Service.ThesisService.DataAccess
{
    public class ThesisRepository : IThesisRepository
    {
        private readonly string _thesisDbConnectionString;
        public ThesisRepository(string connectionString)
        {
            _thesisDbConnectionString = connectionString;
        }

        public async Task AddThesisAsync(ThesisModel model)
        {
            using var connection = new SqlConnection(_thesisDbConnectionString);
            await connection.OpenAsync();
            var query = @"INSERT INTO Theses (Id, StudentId, ExamId, Filename, FileContent, NumberOfPages) VALUES (@Id, @StudentId, @ExamId, @Filename, @FileContent, @NumberOfPages)";
            await connection.ExecuteAsync(query, model);
        }

        public async Task<List<ThesisModel>> GetThesesByStudentId(Guid id)
        {
            using var connection = new SqlConnection(_thesisDbConnectionString);
            await connection.OpenAsync();
            var thesisList = await connection.QueryAsync<ThesisModel>(@"SELECT * FROM Theses WHERE StudentId=@studentId",
                new { studentId=id});
            return thesisList.ToList();
        }

        public async Task<ThesisModel> GetThesisByExamStudentId(Guid studentId, Guid examId)
        {
            using var connection = new SqlConnection(_thesisDbConnectionString);
            await connection.OpenAsync();
            var thesis = connection.QueryFirst<ThesisModel>("SELECT * FROM Theses WHERE StudentId=@studentId AND ExamId=@examId",
                new { studentId = studentId, examId=examId });
            return thesis;
        }

        public async Task<ThesisModel> GetThesisById(Guid thesisId)
        {
            using var connection = new SqlConnection(_thesisDbConnectionString);
            await connection.OpenAsync();
            var thesis = connection.QueryFirst<ThesisModel>("SELECT * FROM Theses WHERE Id=@thesisId",
                new { thesisId=thesisId });
            return thesis;
        }
    }
}
