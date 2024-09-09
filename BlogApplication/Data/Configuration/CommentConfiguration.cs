using BlogApplication.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlogApplication.Data.Configuration;

public class CommentConfiguration : IEntityTypeConfiguration<Comment>
{
    public void Configure(EntityTypeBuilder<Comment> builder)
    {
        builder.ToTable("comments");
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id)
            .HasColumnType("uuid")
            .ValueGeneratedOnAdd();
        builder.Property(c => c.Content)
            .IsRequired();
        builder.Property(c => c.CreatedAt)
            .HasDefaultValueSql("DATETIME('now')");
        
        // builder.HasOne<Post>()
        //     .WithMany(p => p.Comments)
        //     .HasForeignKey("post_id")
        //     .OnDelete(DeleteBehavior.Cascade);
    }
}