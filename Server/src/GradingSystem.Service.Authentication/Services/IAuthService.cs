using GradingSystem.Service.Authentication.Models;
using GradingSystem.Service.Authentication.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradingSystem.Service.Authentication.Services
{
    public interface IAuthService
    {
        Task<string> AuthenticateUserAsync(AuthViewModel authViewModel);
        Task<UserModel> GetCurrentUserAsync(string username);

    }
}
