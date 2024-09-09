using System.ComponentModel.DataAnnotations;

namespace BlogApplication.Data.Entities;

public class Tag
{
    public Guid Id { get; init; }
    public required string Name { get; init; }
    public List<Post> Posts { get; init; } = [];
}