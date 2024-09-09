using BlogApplication.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlogApplication.Data.Configuration;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("users");
        builder.HasKey(u => u.Id);
        builder.HasIndex(u => u.Email).IsUnique();
        builder.Property(u => u.Id)
            .HasColumnType("uuid")
            .ValueGeneratedOnAdd();
        builder.Property(u => u.Email)
            .IsRequired();
        builder.Property(u => u.Password)
            .IsRequired();
        builder.Property(u => u.FullName)
            .IsRequired();
        
        builder.HasMany<Post>()
            .WithOne(p => p.Author)
            .HasForeignKey("author_id")
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasMany<User>()
            .WithMany(u => u.Followings)
            .UsingEntity<Dictionary<string, object>>(
                "user_followings",
                j => j
                    .HasOne<User>()
                    .WithMany()
                    .HasForeignKey("follower_id")
                    .OnDelete(DeleteBehavior.Cascade),
                j => j
                    .HasOne<User>()
                    .WithMany()
                    .HasForeignKey("following_id")
                    .OnDelete(DeleteBehavior.Cascade),
                j => j
                    .HasKey("follower_id", "following_id"));
    }
}