using BlogApplication.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlogApplication.Data.Configuration;

public class TagConfiguration : IEntityTypeConfiguration<Tag>
{
    public void Configure(EntityTypeBuilder<Tag> builder)
    {
        builder.ToTable("tags");
        builder.HasKey(t => t.Id);
        builder.HasIndex(t => t.Name).IsUnique();
        builder.Property(t => t.Id)
            .HasColumnType("uuid")
            .ValueGeneratedOnAdd();
        builder.Property(t => t.Name)
            .IsRequired();
        
        builder.HasMany<Post>(t => t.Posts)
            .WithMany(p => p.Tags)
            .UsingEntity<Dictionary<string, object>>(
                "post_tags",
                j => j
                    .HasOne<Post>()
                    .WithMany()
                    .HasForeignKey("post_id")
                    .OnDelete(DeleteBehavior.Cascade),
                j => j
                    .HasOne<Tag>()
                    .WithMany()
                    .HasForeignKey("tag_id")
                    .OnDelete(DeleteBehavior.Cascade),
                j => j
                    .HasKey("post_id", "tag_id"));
    }
}