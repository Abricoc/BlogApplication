using BlogApplication.Data.Entities;

namespace BlogApplication.Models.Posts;

public class PostListModel
{
    public required List<Post> Posts { get; init; }
    public required List<Tag> Tags { get; init; }
}