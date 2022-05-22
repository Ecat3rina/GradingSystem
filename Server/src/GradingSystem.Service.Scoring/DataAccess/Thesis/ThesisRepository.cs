using Dapper;
using GradingSystem.Service.Scoring.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GradingSystem.Service.Scoring.DataAccess
{
    public class ThesisRepository : IThesisRepository
    {
        private readonly string _thesisDbConnectionString;
        public ThesisRepository(string connectionString)
        {
            _thesisDbConnectionString = connectionString;
        }
        public async Task<string> GetExamId(string thesisId)
        {
            using var connection = new SqlConnection(_thesisDbConnectionString);
            await connection.OpenAsync();
            var examId = await connection.QueryFirstAsync<string>("SELECT ExamId FROM Theses WHERE Id=@thesisId",
                new { thesisId = thesisId });
            return examId;
        }

        public async Task UpdateThesis(ThesisModel model)
        {
            using var connection = new SqlConnection(_thesisDbConnectionString);
            await connection.OpenAsync();
            await connection.ExecuteAsync(@"UPDATE Theses SET BlobId=@blobId 
                        WHERE ExamId=@examId AND StudentId=@studentId",
                        new { blobId = model.BlobId, examId = model.ExamId, studentId = model.StudentId });
        }

        public async Task<IEnumerable<ThesisModel>> GetThesesForStudentAsync(Guid studentId)
        {
            using var connection = new SqlConnection(_thesisDbConnectionString);
            await connection.OpenAsync();
            var sql = @"SELECT * from Theses where StudentId = @studentId";
            return await connection.QueryAsync<ThesisModel>(sql, new { studentId });
        }

        public async Task<ThesisModel> GetThesisByIdAsync(Guid thesisId)
        {
            using var connection = new SqlConnection(_thesisDbConnectionString);
            await connection.OpenAsync();
            var sql = @"SELECT * from Theses where Id = @thesisId";
            return await connection.QueryFirstAsync<ThesisModel>(sql, new { thesisId });
        }

        public async Task UpdateThesisWithFinalDetailsAsync(UpdateThesisModel model)
        {
            using var connection = new SqlConnection(_thesisDbConnectionString);
            await connection.OpenAsync();
            await connection.ExecuteAsync(@"UPDATE Theses SET FinalScore=@score, FinalGrade=@grade, GradationDate=@date 
                        WHERE Id=@thesisId",
                        new { score = model.FinalScore, grade = model.FinalGrade, date = model.GradationDate, thesisId=model.ThesisId });
        }
    }
}

