using System.ComponentModel.DataAnnotations;

namespace BlogApplication.Models.Auth;

public class LoginRequest
{
    [Required(ErrorMessage = "Email обязательное поле")]
    [EmailAddress(ErrorMessage = "Некорректный email")]
    public required string Email { get; set; }
    [Required(ErrorMessage = "Пароль обязательное поле")]
    public required string Password { get; set; }
}