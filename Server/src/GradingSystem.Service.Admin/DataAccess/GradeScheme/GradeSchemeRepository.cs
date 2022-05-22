using Dapper;
using GradingSystem.Service.Admin.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradingSystem.Service.Admin.DataAccess.GradeScheme
{
    public class GradeSchemeRepository : IGradeSchemeRepository
    {
        private readonly string _gradeSchemeDbConnectionString;
        public GradeSchemeRepository(string connectionString)
        {
            _gradeSchemeDbConnectionString = connectionString;
        }
        public async Task<List<GradeSchemeModel>> GetGradeSchemes()
        {
            using var connection = new SqlConnection(_gradeSchemeDbConnectionString);
            await connection.OpenAsync();
            var gradeSchemeList = await connection.QueryAsync<GradeSchemeModel>(@"SELECT * FROM GradeSchemes");
            return gradeSchemeList.ToList();
        }
        public async Task DeleteGradeScheme(Guid id)
        {
            using var connection = new SqlConnection(_gradeSchemeDbConnectionString);
            await connection.OpenAsync();
            await connection.ExecuteAsync(@"DELETE FROM GradeSchemeComponents WHERE GradeSchemeId=@gradeSchemeId", new { gradeSchemeId = id });
            await connection.ExecuteAsync(@"DELETE FROM GradeSchemes WHERE Id=@gradeSchemeId", new { gradeSchemeId = id });

        }

        public async Task AddGradeScheme(GradeSchemeModel model)
        {
            using var connection = new SqlConnection(_gradeSchemeDbConnectionString);
            await connection.OpenAsync();
            var queryGradeScheme = @"INSERT INTO GradeSchemes (Id, Name, ExamId, NumberOfItems) VALUES (@Id, @Name, @ExamId, @NumberOfItems)";
            await connection.ExecuteAsync(queryGradeScheme, model);
            for (int i = 0; i < model.GradeSchemeComponents.Count(); i++)
            {
                var queryGradeSchemeComponents = @"INSERT INTO GradeSchemeComponents (Id, GradeSchemeId, Grade, MinimumScore, MaximumScore) 
                                            VALUES (@Id, @GradeSchemeId, @Grade, @MinimumScore, @MaximumScore)";
                await connection.ExecuteAsync(queryGradeSchemeComponents, model.GradeSchemeComponents[i]);
            }
            
        }

        public async Task<GradeSchemeModel> GetGradeSchemeById(Guid id)
        {
            using var connection = new SqlConnection(_gradeSchemeDbConnectionString);
            await connection.OpenAsync();
            var gradeScheme = connection.QueryFirst<GradeSchemeModel>("SELECT * FROM GradeSchemes WHERE Id=@gradeSchemeId",
                new { gradeSchemeId = id });
            var gradeSchemeComponentList = await connection.QueryAsync<GradeSchemeComponentModel>(@"SELECT * FROM GradeSchemeComponents Where GradeSchemeId=@gradeSchemeId",
                new { gradeSchemeId = id });
            gradeScheme.GradeSchemeComponents = gradeSchemeComponentList.ToList();
            return gradeScheme;
        }

        public async Task UpdateGradeScheme(GradeSchemeModel model)
        {
            using var connection = new SqlConnection(_gradeSchemeDbConnectionString);
            await connection.OpenAsync();
            await connection.ExecuteAsync(@"UPDATE GradeSchemes SET Name=@name WHERE Id=@gradeSchemeId",
                new { name = model.Name, gradeSchemeId = model.Id });
            for (int i = 0; i < model.GradeSchemeComponents.Count(); i++)
            {
                await connection.ExecuteAsync(@"UPDATE GradeSchemeComponents SET Grade=@grade, MinimumScore=@min, MaximumScore=@max WHERE Id=@componentId AND GradeSchemeId=@gradeSchemeId",
                   new { componentId=model.GradeSchemeComponents[i].Id, grade = model.GradeSchemeComponents[i].Grade, min=model.GradeSchemeComponents[i].MinimumScore, max = model.GradeSchemeComponents[i].MaximumScore, gradeSchemeId = model.Id });
            }
        }

        public async Task<GradeSchemeModel> GetGradeSchemeByExamId(Guid examId)
        {
            using var connection = new SqlConnection(_gradeSchemeDbConnectionString);
            await connection.OpenAsync();
            var query = @"SELECT GS.ID, GS.Name FROM GradeSchemes GS " +
                         "INNER JOIN Exams ON GS.Id=Exams.GradeSchemeId WHERE Exams.Id=@examId";

            var gradeScheme = connection.QueryFirst<GradeSchemeModel>(query, new { examId });
            var gradeSchemeComponentList = await connection.QueryAsync<GradeSchemeComponentModel>(@"SELECT * FROM GradeSchemeComponents "+
                "WHERE GradeSchemeId=@gradeSchemeId",
                new { gradeSchemeId = gradeScheme.Id });
            gradeScheme.GradeSchemeComponents = gradeSchemeComponentList.ToList();
            return gradeScheme;
        }
    }
}
