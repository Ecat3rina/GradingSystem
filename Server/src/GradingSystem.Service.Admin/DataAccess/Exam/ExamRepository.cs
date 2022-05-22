using Dapper;
using GradingSystem.Service.Admin.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradingSystem.Service.Admin.DataAccess.Exam
{
    public class ExamRepository : IExamRepository
    {
        private readonly string _examDbConnectionString;
        public ExamRepository(string connectionString)
        {
            _examDbConnectionString = connectionString;
        }
        public async Task AddExam(ExamModel model)
        {
            using var connection = new SqlConnection(_examDbConnectionString);
            await connection.OpenAsync();
            var query = @"INSERT INTO Exams (Id, Name, StartDate, EndDate, NumberOfPages, NumberOfEvaluators, SubjectId, GradeSchemeId, WasGenerated) 
                        VALUES (@Id, @Name, @StartDate, @EndDate, @NumberOfPages, @NumberOfEvaluators, @SubjectId, @GradeSchemeId, @WasGenerated)";
            await connection.ExecuteAsync(query, model);
        }


        public async Task DeleteExam(Guid id)
        {
            using var connection = new SqlConnection(_examDbConnectionString);
            await connection.OpenAsync();
            await connection.ExecuteAsync(@"DELETE FROM Exams WHERE Id=@examId", new { examId = id });
        }

        public async Task<List<ExamModel>> GetExams()
        {
            using var connection = new SqlConnection(_examDbConnectionString);
            await connection.OpenAsync();
            var examList = await connection.QueryAsync<ExamModel>(@"SELECT * FROM Exams");
            return examList.ToList();
        }

        public async Task<ExamModel> GetExamById(Guid id)
        {
            using var connection = new SqlConnection(_examDbConnectionString);
            await connection.OpenAsync();
            var evaluationScheme = connection.QueryFirst<ExamModel>("SELECT * FROM Exams WHERE Id=@examId",
                new { examId = id });
            return evaluationScheme;
        }

        public async Task<List<ExamModel>> GetExamsBySubjectId(Guid id)
        {
            using var connection = new SqlConnection(_examDbConnectionString);
            await connection.OpenAsync();
            var examList = await connection.QueryAsync<ExamModel>(@"SELECT * FROM Exams WHERE SubjectId=@subjectId",
                new { subjectId = id });
            return examList.ToList();
        }

        public async Task UpdateExam(ExamModel model)
        {
            using var connection = new SqlConnection(_examDbConnectionString);
            await connection.OpenAsync();
            await connection.ExecuteAsync(@"UPDATE Exams SET Name=@name, StartDate=@start, EndDate=@end, NumberOfPages=@pages, 
                NumberOfEvaluators=@evaluators, SubjectId=@subjectId, GradeSchemeId=@gradeSchemeId, WasGenerated=@wasGenerated WHERE Id=@examId",
                new { name=model.Name, start = model.StartDate, end = model.EndDate, pages = model.NumberOfPages, 
                    evaluators = model.NumberOfEvaluators, subjectId = model.SubjectId, gradeSchemeId = model.GradeSchemeId, 
                    examId = model.Id, wasGenerated=model.WasGenerated});

        }
    }
}
