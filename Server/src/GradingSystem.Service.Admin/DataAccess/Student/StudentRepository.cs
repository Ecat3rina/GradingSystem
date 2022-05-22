using Dapper;
using GradingSystem.Service.Admin.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradingSystem.Service.Admin.DataAccess.Student
{
    public class StudentRepository:IStudentRepository
    {
        private readonly string _studentDbConnectionString;
        public StudentRepository(string connectionString)
        {
            _studentDbConnectionString = connectionString;
        }

        public async Task AddStudent(StudentModel model)
        {
            using var connection = new SqlConnection(_studentDbConnectionString);
            await connection.OpenAsync();
            var query = @"INSERT INTO Students (Id, FirstName, LastName, IDNP, BirthDate, Address, GroupId) VALUES (@Id, @FirstName, @LastName, @IDNP, @BirthDate, @Address, @GroupId)";
            await connection.ExecuteAsync(query, model);
        }
        public async Task DeleteStudent(Guid id)
        {
            using var connection = new SqlConnection(_studentDbConnectionString);
            await connection.OpenAsync();
            await connection.ExecuteAsync(@"DELETE FROM Students WHERE Id=@studentId", new { studentId = id });
        }

        public async Task<StudentModel> GetStudentById(Guid id)
        {
            using var connection = new SqlConnection(_studentDbConnectionString);
            await connection.OpenAsync();
            var student = connection.QueryFirst<StudentModel>("SELECT * FROM Students WHERE Id=@studentId",
                new { studentId = id });
            return student;
        }
        public async Task<List<StudentModel>> GetStudents()
        {
            using var connection = new SqlConnection(_studentDbConnectionString);
            await connection.OpenAsync();
            var studentList = await connection.QueryAsync<StudentModel>(@"SELECT * FROM Students");
            return studentList.ToList();
        }

        public async Task<List<StudentModel>> GetStudentsByGroupId(Guid id)
        {
            using var connection = new SqlConnection(_studentDbConnectionString);
            await connection.OpenAsync();
            var students = await connection.QueryAsync<StudentModel>(@"SELECT * FROM Students WHERE GroupId=@groupId",
                new { groupId = id });
            return students.ToList();
        }

        public async Task UpdateStudent(StudentModel model)
        {
            using var connection = new SqlConnection(_studentDbConnectionString);
            await connection.OpenAsync();
            await connection.ExecuteAsync(@"UPDATE Students SET FirstName=@firstName, LastName=@lastName, IDNP=@idnp, BirthDate=@birth, Address=@address, GroupId=@groupId WHERE Id=@studentId",
                new { firstName = model.FirstName, lastName = model.LastName, idnp = model.IDNP, birth=model.BirthDate, address=model.Address, studentId = model.Id, groupId=model.GroupId });
        }
    }
}
