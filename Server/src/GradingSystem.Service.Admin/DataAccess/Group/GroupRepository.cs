using Dapper;
using GradingSystem.Service.Admin.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradingSystem.Service.Admin.DataAccess.Group
{
    public class GroupRepository : IGroupRepository
    {
        private readonly string _groupDbConnectionString;
        public GroupRepository(string connectionString)
        {
            _groupDbConnectionString = connectionString;
        }
        public async Task AddGroup(GroupModel model)
        {
            using var connection = new SqlConnection(_groupDbConnectionString);
            await connection.OpenAsync();
            var query = @"INSERT INTO Groups (Id, Name, Description) VALUES (@Id, @Name, @Description)";
            await connection.ExecuteAsync(query, model);
        }

        public async Task DeleteGroup(Guid id)
        {
            using var connection = new SqlConnection(_groupDbConnectionString);
            await connection.OpenAsync();
            await connection.ExecuteAsync(@"DELETE FROM Groups WHERE Id=@groupId", new { groupId = id });
        }

        public async Task<GroupModel> GetGroupById(Guid id)
        {
            using var connection = new SqlConnection(_groupDbConnectionString);
            await connection.OpenAsync();
            var group = connection.QueryFirst<GroupModel>("SELECT * FROM Groups WHERE Id=@groupId",
                new { groupId = id });
            return group;
        }

        public async Task<List<GroupModel>> GetGroups()
        {
            using var connection = new SqlConnection(_groupDbConnectionString);
            await connection.OpenAsync();
            var groupList = await connection.QueryAsync<GroupModel>(@"SELECT * FROM Groups");
            return groupList.ToList();
        }

        public async Task UpdateGroup(GroupModel model)
        {
            using var connection = new SqlConnection(_groupDbConnectionString);
            await connection.OpenAsync();
            await connection.ExecuteAsync(@"UPDATE Groups SET Name=@name, Description=@description WHERE Id=@groupId",
                new { name = model.Name, description = model.Description, groupId = model.Id });
        }
    }
}
