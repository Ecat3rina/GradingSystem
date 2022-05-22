using Dapper;
using GradingSystem.Service.Admin.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradingSystem.Service.Admin.DataAccess
{
    public class EvaluatorRepartitionRepository : IEvaluatorRepartitionRepository
    {
        private readonly string _evaluatorRepartitionDbConnectionString;
        public EvaluatorRepartitionRepository(string connectionString)
        {
            _evaluatorRepartitionDbConnectionString = connectionString;
        }

        public async Task<List<StatisticsModel>> GetStatistics()
        {
            using var connection = new SqlConnection(_evaluatorRepartitionDbConnectionString);
            await connection.OpenAsync();
            var statisticsList = new List<StatisticsModel>();
            var examNames= await connection.QueryAsync<string>(@"SELECT Name FROM Exams");
            var numbersOfEvaluators = await connection.QueryAsync<int>(@"SELECT NumberOfEvaluators FROM Exams");
            var subjectNames = await connection.QueryAsync<string>(@"SELECT Subjects.Name FROM Exams 
                                    inner join Subjects on Subjects.Id = Exams.SubjectId");
            var checkGeneration= await connection.QueryAsync<bool>(@"SELECT WasGenerated FROM Exams");

            foreach (var item in examNames.Zip(numbersOfEvaluators, (a, b) => new { A = a, B = b }))
            {
                var a = item.A;
                var b = item.B;

                var stat = new StatisticsModel
                {
                    ExamName=a,
                    NumberOfEvaluatorsPerThesis=b,
                    SubjectName=null,
                    WasGenerated=false
                };
                statisticsList.Add(stat);
            }
            foreach (var item in statisticsList.Zip(subjectNames, (a, b) => new { A = a, B = b }))
            {
                var a = item.A;
                var b = item.B;
                a.SubjectName = b;

            }
            foreach (var item in statisticsList.Zip(checkGeneration, (a, b) => new { A = a, B = b }))
            {
                var a = item.A;
                var b = item.B;
                
                var nrOfEvaluators= await connection.QueryAsync<int>(@"select COUNT(SubjectId) from Evaluators
                                                        inner join Subjects on Subjects.Id=Evaluators.SubjectId 
                                                        where Subjects.Name=@name", new { name=a.SubjectName});
                a.TotalNrOfEvaluators = nrOfEvaluators.First();
                a.WasGenerated = b;
            }

            return statisticsList.ToList();
        }

        public async Task<EvaluatorRepartitionModel> TriggerEvaluatorRepartition(string examName)
        {
            using var connection = new SqlConnection(_evaluatorRepartitionDbConnectionString);
            await connection.OpenAsync();
            var evaluatorRepartitionModel = new EvaluatorRepartitionModel();
            await connection.ExecuteAsync(@"UPDATE Exams SET WasGenerated=1 WHERE Name=@name",new { name=examName});
            evaluatorRepartitionModel.ExamId= connection.QueryFirst<Guid>(@"SELECT Id FROM Exams WHERE Name=@examName",
                new { examName = examName });
            evaluatorRepartitionModel.NrOfEvaluators= connection.QueryFirst<int>(@"SELECT NumberOfEvaluators FROM Exams WHERE Id=@examId",
                new { examId = evaluatorRepartitionModel.ExamId });
            var subjectId= connection.QueryFirst<Guid>(@"SELECT SubjectId FROM Exams WHERE Name=@examName",
                new { examName = examName });
            var evaluatorsList= await connection.QueryAsync<Guid>(@"select Evaluators.Id from Evaluators
                        inner join Subjects on Subjects.Id=Evaluators.SubjectId 
                        where Subjects.Id=@subjectId",new { subjectId=subjectId});
            evaluatorRepartitionModel.Evaluators = evaluatorsList.ToList();

            return evaluatorRepartitionModel;
        }
    }
}
