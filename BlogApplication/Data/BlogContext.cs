using BlogApplication.Data.Configuration;
using BlogApplication.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace BlogApplication.Data;

public sealed class BlogContext : DbContext
{
    public DbSet<Tag> Tags { get; init; } = null!;
    public DbSet<User> Users { get; init; } = null!;
    public DbSet<Post> Posts { get; init; } = null!;
    
    public BlogContext(DbContextOptions<BlogContext> options) : base(options)
    {
        Database.EnsureCreated();
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new TagConfiguration());
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new PostConfiguration());
        modelBuilder.ApplyConfiguration(new CommentConfiguration());
    }
}