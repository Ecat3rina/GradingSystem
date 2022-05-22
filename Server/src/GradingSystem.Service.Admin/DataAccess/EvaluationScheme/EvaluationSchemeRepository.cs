using Dapper;
using GradingSystem.Service.Admin.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradingSystem.Service.Admin.DataAccess.EvaluationScheme
{
    public class EvaluationSchemeRepository : IEvaluationSchemeRepository
    {
        private readonly string _evaluationSchemeDbConnectionString;
        public EvaluationSchemeRepository(string connectionString)
        {
            _evaluationSchemeDbConnectionString = connectionString;
        }
        public async Task AddEvaluationScheme(EvaluationSchemeModel model)
        {
            using var connection = new SqlConnection(_evaluationSchemeDbConnectionString);
            await connection.OpenAsync();
            var queryEvaluationScheme = @"INSERT INTO EvaluationSchemes (Id, ExamId, Name, NumberOfItems) VALUES (@Id, @ExamId, @Name, @NumberOfItems)";
            await connection.ExecuteAsync(queryEvaluationScheme, model);
            for (int i = 0; i < model.EvaluationSchemeComponents.Count(); i++)
            {
                var queryEvaluationSchemeComponents = @"INSERT INTO EvaluationSchemeComponents  (Id, EvaluationSchemeId, ItemNr, PageNr, MinimumScore, MaximumScore, CorrectAnswer, Specifications) 
                                            VALUES (@Id, @EvaluationSchemeId, @ItemNr, @PageNr, @MinimumScore, @MaximumScore, @CorrectAnswer, @Specifications)";
                await connection.ExecuteAsync(queryEvaluationSchemeComponents, model.EvaluationSchemeComponents[i]);
            }
        }

        public async Task DeleteEvaluationScheme(Guid id)
        {
            using var connection = new SqlConnection(_evaluationSchemeDbConnectionString);
            await connection.OpenAsync();
            await connection.ExecuteAsync(@"DELETE FROM EvaluationSchemeComponents WHERE EvaluationSchemeId=@evaluationSchemeId", new { evaluationSchemeId = id });
            await connection.ExecuteAsync(@"DELETE FROM EvaluationSchemes WHERE Id=@evaluationSchemeId", new { evaluationSchemeId = id });

        }

        public async Task<EvaluationSchemeModel> GetEvaluationSchemeById(Guid id)
        {
            using var connection = new SqlConnection(_evaluationSchemeDbConnectionString);
            await connection.OpenAsync();
            var evaluationScheme = connection.QueryFirst<EvaluationSchemeModel>("SELECT * FROM EvaluationSchemes WHERE Id=@evaluationSchemeId",
                new { evaluationSchemeId = id });
            var evaluationSchemeComponentList = await connection.QueryAsync<EvaluationSchemeComponentModel>(@"SELECT * FROM EvaluationSchemeComponents Where EvaluationSchemeId=@evaluationSchemeId",
                new { evaluationSchemeId = id });
            evaluationScheme.EvaluationSchemeComponents = evaluationSchemeComponentList.ToList();
            return evaluationScheme;
        }

        public async Task<List<EvaluationSchemeModel>> GetEvaluationSchemes()
        {
            using var connection = new SqlConnection(_evaluationSchemeDbConnectionString);
            await connection.OpenAsync();
            var evaluationSchemesList = await connection.QueryAsync<EvaluationSchemeModel>(@"SELECT * FROM EvaluationSchemes");
            return evaluationSchemesList.ToList();
        }

        public async Task<EvaluationSchemeModel> GetEvaluationSchemesByExamIdAsync(Guid id)
        {
            using var connection = new SqlConnection(_evaluationSchemeDbConnectionString);
            await connection.OpenAsync();
            var evaluationScheme = connection.QueryFirst<EvaluationSchemeModel>("SELECT * FROM EvaluationSchemes WHERE ExamId=@examId",
                new { examId = id });
            var evaluationSchemeComponentList = await connection.QueryAsync<EvaluationSchemeComponentModel>(@"SELECT * FROM EvaluationSchemeComponents Where EvaluationSchemeId=@evaluationSchemeId",
                new { evaluationSchemeId = evaluationScheme.Id });
            evaluationScheme.EvaluationSchemeComponents = evaluationSchemeComponentList.ToList();
            return evaluationScheme;

        }

        public async Task UpdateEvaluationScheme(EvaluationSchemeModel model)
        {
            using var connection = new SqlConnection(_evaluationSchemeDbConnectionString);
            await connection.OpenAsync();
            await connection.ExecuteAsync(@"UPDATE EvaluationSchemes SET ExamId=@examId, Name=@name, NumberOfItems=@nrItems WHERE Id=@evSchemeId",
                  new { examId = model.ExamId, name=model.Name, nrItems=model.NumberOfItems,  evSchemeId = model.Id });
            for (int i = 0; i < model.EvaluationSchemeComponents.Count(); i++)
            {
                await connection.ExecuteAsync(@"UPDATE EvaluationSchemeComponents SET ItemNr=@itemNr, PageNr=@pageNr, MinimumScore=@min, MaximumScore=@max, 
                CorrectAnswer=@answer, Specifications=@specifications WHERE Id=@componentId AND EvaluationSchemeId=@evSchemeId",
                   new { componentId = model.EvaluationSchemeComponents[i].Id, itemNr = model.EvaluationSchemeComponents[i].ItemNr, pageNr = model.EvaluationSchemeComponents[i].PageNr, 
                       min = model.EvaluationSchemeComponents[i].MinimumScore, max = model.EvaluationSchemeComponents[i].MaximumScore, answer=model.EvaluationSchemeComponents[i].CorrectAnswer,
                       specifications=model.EvaluationSchemeComponents[i].Specifications, evSchemeId = model.Id });
            }
        }
    }
}
