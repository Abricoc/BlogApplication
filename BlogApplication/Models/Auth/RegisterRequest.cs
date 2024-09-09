using System.ComponentModel.DataAnnotations;

namespace BlogApplication.Models.Auth;

public class RegisterRequest
{
    [Required(ErrorMessage = "Email является обязательным полем")]
    [EmailAddress(ErrorMessage = "Некорректный адрес электронной почты")]
    public required string Email { get; init; }
    [Required(ErrorMessage = "Пароль является обязательным полем")]
    public required string Password { get; init; }
    [Required(ErrorMessage = "Имя является обязательным полем")]
    public required string FullName { get; init; }
}