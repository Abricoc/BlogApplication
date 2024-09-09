using System.ComponentModel.DataAnnotations;

namespace BlogApplication.Models.Posts;

public class PostCreateRequest
{
    [Required(ErrorMessage = "Название является обязательным полем")]
    public required string Title { get; init; }

    public string? Tags { get; init; } = "";
    
    [Required(ErrorMessage = "Содержание является обязательным полем")]
    public required string Content { get; init; }
    public required bool Hidden { get; init; }
}