using System.ComponentModel.DataAnnotations;

namespace BlogApplication.Data.Entities;

public class Comment
{
    public Guid Id { get; init; }
    [MaxLength(255)]
    public required string Content { get; init; }
    public DateTime CreatedAt { get; init; }
}