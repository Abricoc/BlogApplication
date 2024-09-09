namespace BlogApplication.Data.Entities;

public class Post
{
    public Guid Id { get; init; }
    public required string Title { get; init; }
    public required string Content { get; init; }
    public required bool Visible { get; init; }
    public DateTime CreatedAt { get; init; }
    public required User Author { get; init; }
    public List<Tag> Tags { get; init; } = [];
    public List<Comment> Comments { get; init; } = [];
}