using Dapper;
using GradingSystem.Service.Admin.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradingSystem.Service.Admin.DataAccess.Subject
{
    public class SubjectRepository : ISubjectRepository
    {
        private readonly string _subjectDbConnectionString;
        public SubjectRepository(string connectionString)
        {
            _subjectDbConnectionString = connectionString;
        }
        public async Task AddSubject(SubjectModel model)
        {
            using var connection = new SqlConnection(_subjectDbConnectionString);
            await connection.OpenAsync();
            var query = @"INSERT INTO Subjects (Id, Name, Description) VALUES (@Id, @Name, @Description)";
            await connection.ExecuteAsync(query, model);
        }

        public async Task DeleteSubject(Guid id)
        {
            using var connection = new SqlConnection(_subjectDbConnectionString);
            await connection.OpenAsync();
            await connection.ExecuteAsync(@"DELETE FROM Subjects WHERE Id=@subjectId", new { subjectId = id });
        }

        public async Task<List<SubjectModel>> GetSubjects()
        {
            using var connection = new SqlConnection(_subjectDbConnectionString);
            await connection.OpenAsync();
            var subjectList = await connection.QueryAsync<SubjectModel>(@"SELECT * FROM Subjects");
            return subjectList.ToList();
        }

        public async Task<SubjectModel> GetSubjectById(Guid id)
        {
            using var connection = new SqlConnection(_subjectDbConnectionString);
            await connection.OpenAsync();
            var subject = connection.QueryFirst<SubjectModel>("SELECT * FROM Subjects WHERE Id=@subjectId",
                new { subjectId = id });
            return subject;
        }

        public async Task UpdateSubject(SubjectModel model)
        {
            using var connection = new SqlConnection(_subjectDbConnectionString);
            await connection.OpenAsync();
            await connection.ExecuteAsync(@"UPDATE Subjects SET Name=@name, Description=@description WHERE Id=@subjectId",
                new { name=model.Name, description=model.Description, subjectId = model.Id });
        }
    }
}
