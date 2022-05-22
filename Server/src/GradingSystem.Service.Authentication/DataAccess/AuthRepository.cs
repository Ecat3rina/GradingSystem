using Dapper;
using GradingSystem.Service.Authentication.Models;
using Microsoft.Data.SqlClient;
using System.Threading.Tasks;

namespace GradingSystem.Service.Authentication.DataAccess
{
    public class AuthRepository : IAuthRepository
    {
        private readonly string _authDbConnectionString;
        public AuthRepository(string connectionString)
        {
            _authDbConnectionString = connectionString;
        }

        public async Task<UserModel> GetUserModelAsync(string username)
        {
            using var connection = new SqlConnection(_authDbConnectionString);
            await connection.OpenAsync();
            var user = await connection.QueryFirstOrDefaultAsync<UserModel>(@"SELECT * FROM Users WHERE Username=@username",
                new { username = username });
            return user;
        }
    }
}
