using BlogApplication.Data;
using BlogApplication.Data.Entities;
using BlogApplication.Models.Auth;
using BlogApplication.Models.Posts;
using Microsoft.EntityFrameworkCore;
using BC = BCrypt.Net.BCrypt;

namespace BlogApplication.Interceptors;

public class PostInterceptor(BlogContext context)
{
    public async Task<List<Post>> GetAllPosts(string tag = "")
    {
        var posts = context.Posts
            .Where(p => p.Visible);
        
        if (!string.IsNullOrEmpty(tag))
        {
            posts = posts
                .Where(p => p.Tags
                    .Any(t => t.Name == tag));
        }
        
        return await posts
            .ToListAsync();
    }
    
    public async Task<Post?> GetPostById(Guid id)
    {
        return await context.Posts
            .Where(p => p.Id == id)
            .Include(p => p.Author)
            .Include(p => p.Tags)
            .Include(p => p.Comments)
            .FirstOrDefaultAsync();
    }
    
    public async Task<Post> CreatePost(PostCreateRequest request, Guid userId)
    {
        var post = (await context.Posts.AddAsync(new Post
        {
            Title = request.Title,
            Content = request.Content,
            CreatedAt = DateTime.Now,
            Visible = !request.Hidden,
            Author = await context.Users.FirstOrDefaultAsync(u => u.Id == userId)
                     ?? throw new InvalidOperationException()
        })).Entity;

        foreach (var tag in request.Tags?.Split(",") ?? [])
        {
            var tagEntity = 
                (await context.Tags.FirstOrDefaultAsync(t => t.Name == tag.Trim()) ?? null) 
                ?? (await context.Tags.AddAsync(new Tag
                {
                    Name = tag.Trim()
                })).Entity;

            post.Tags.Add(tagEntity);
        }
        await context.SaveChangesAsync();
        
        return post;
    }
    
    public async Task DeletePost(Guid id)
    {
        var post = await context.Posts
            .Where(p => p.Id == id)
            .FirstOrDefaultAsync();
        
        if (post is null) return;
        
        context.Posts.Remove(post);
        await context.SaveChangesAsync();
    }
}