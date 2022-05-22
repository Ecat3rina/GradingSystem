using GradingSystem.Service.Authentication.Services;
using GradingSystem.Service.Authentication.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GradingSystem.Service.Authentication.Controllers
{
    [ApiController]
    [Route("auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpGet]
        [Route("{username}")]
        public async Task<IActionResult> GetCurrentUserModelAsync(string username)
        {
            
            var result =await  _authService.GetCurrentUserAsync(username);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AuthenticateUserAsync(AuthViewModel authViewModel)
        {
            var authResult = await _authService.AuthenticateUserAsync(authViewModel);
            if (string.IsNullOrWhiteSpace(authResult))
            {
                return Unauthorized();
            }
            return Ok(new { Token = authResult });
        }
    }
}
