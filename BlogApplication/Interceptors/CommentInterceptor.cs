using BlogApplication.Data;
using BlogApplication.Data.Entities;
using BlogApplication.Models.Auth;
using BlogApplication.Models.Posts;
using Microsoft.EntityFrameworkCore;
using BC = BCrypt.Net.BCrypt;

namespace BlogApplication.Interceptors;

public class CommentInterceptor(BlogContext context)
{
    public async Task CreateComment(Guid postId, string commentText, Guid userId)
    {
        var post = await context.Posts.FirstOrDefaultAsync(p => p.Id == postId) ?? throw new InvalidOperationException();
        var user = await context.Users.FirstOrDefaultAsync(u => u.Id == userId) ??
                   throw new InvalidOperationException();
        post.Comments.Add(new Comment
        {
            Content = commentText,
            CreatedAt = DateTime.Now
        });
        
        await context.SaveChangesAsync();
    }
}