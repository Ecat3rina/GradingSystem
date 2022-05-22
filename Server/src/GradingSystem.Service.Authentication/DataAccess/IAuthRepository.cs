using GradingSystem.Service.Authentication.Models;
using System.Threading.Tasks;

namespace GradingSystem.Service.Authentication.DataAccess
{
    public interface IAuthRepository
    {
        Task<UserModel> GetUserModelAsync(string username);
    }
}
