using BlogApplication.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlogApplication.Data.Configuration;

public class PostConfiguration : IEntityTypeConfiguration<Post>
{
    public void Configure(EntityTypeBuilder<Post> builder)
    {
        builder.ToTable("posts");
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id)
            .HasColumnType("uuid")
            .ValueGeneratedOnAdd();
        builder.Property(p => p.Content)
            .IsRequired();
        builder.Property(p => p.Title)
            .IsRequired()
            .HasMaxLength(256);
        builder.Property(p => p.CreatedAt)
            .HasDefaultValueSql("DATETIME('now')");

        builder.HasOne<User>(p => p.Author)
            .WithMany(u => u.Posts)
            .HasForeignKey("author_id")
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasMany<Comment>(p => p.Comments)
            .WithOne()
            .HasForeignKey("post_id")
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasMany<Tag>(p => p.Tags)
            .WithMany(t => t.Posts)
            .UsingEntity<Dictionary<string, object>>(
                "post_tags",
                j => j
                    .HasOne<Tag>()
                    .WithMany()
                    .HasForeignKey("tag_id")
                    .OnDelete(DeleteBehavior.Cascade),
                j => j
                    .HasOne<Post>()
                    .WithMany()
                    .HasForeignKey("post_id")
                    .OnDelete(DeleteBehavior.Cascade),
                j => j
                    .HasKey("post_id", "tag_id"));
    }
}