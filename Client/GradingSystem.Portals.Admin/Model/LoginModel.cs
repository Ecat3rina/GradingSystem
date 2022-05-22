using System.ComponentModel.DataAnnotations;
namespace GradingSystem.Portals.Admin.Models;

public class LoginModel{
    [Required(ErrorMessage ="Numele de utilizator este obligatoriu")]
    public string Username { get; set; }
    [Required(ErrorMessage ="Parola este obligatorie")]
    public string Password { get; set; }
}
