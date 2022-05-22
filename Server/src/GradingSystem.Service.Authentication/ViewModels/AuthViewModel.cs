using System.ComponentModel.DataAnnotations;

namespace GradingSystem.Service.Authentication.ViewModels
{
    public class AuthViewModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
