namespace BlogApplication.Data.Entities;

public class User
{
    public Guid Id { get; init; }
    public required string Email { get; init; }
    public required string Password { get; init; }
    public required string FullName { get; init; }
    public List<Post> Posts { get; init; } = [];
    public List<User> Followings { get; init; } = [];
}