using GradingSystem.Service.Authentication.DataAccess;
using GradingSystem.Service.Authentication.Models;
using GradingSystem.Service.Authentication.Options;
using GradingSystem.Service.Authentication.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace GradingSystem.Service.Authentication.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepository;
        private readonly AuthTokenConfiguration _authConfig;
        public AuthService(IOptions<AuthTokenConfiguration> options, IAuthRepository authRepository)
        {
            _authConfig = options.Value;
            _authRepository = authRepository;
        }
        public async Task<string> AuthenticateUserAsync(AuthViewModel authViewModel)
        {
            var userModel = await _authRepository.GetUserModelAsync(authViewModel.Username);

            if (userModel == null)
            {
                return null;
            }
            if (userModel.Password != authViewModel.Password)
            {
                //implement a hashing algorithm for passwords
                return null;
            }
            return GenerateJwtToken(userModel);
        }

        public async Task<UserModel> GetCurrentUserAsync(string username)
        {
            var result = await _authRepository.GetUserModelAsync(username);
            return result;
        }

        private string GenerateJwtToken(UserModel model)
        {
            var tokenKey = _authConfig.Key;
            var mySecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(tokenKey));
            var tokenIssuer = _authConfig.Issuer;
            var tokenAudience = _authConfig.Audience;
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, model.Id.ToString()),
                    new Claim(ClaimTypes.Name, model.Username),
                    new Claim(ClaimTypes.GivenName, model.Firstname),
                    new Claim(ClaimTypes.Surname, model.Lastname)

                }),
                Expires = DateTime.UtcNow.AddHours(2),
                Issuer = tokenIssuer,
                Audience = tokenAudience,
                NotBefore = DateTime.UtcNow,
                SigningCredentials = new SigningCredentials(mySecurityKey, SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
