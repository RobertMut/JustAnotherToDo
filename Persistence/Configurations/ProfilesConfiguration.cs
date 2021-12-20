using JustAnotherToDo.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JustAnotherToDo.Persistence.Configurations;

public class ProfilesConfiguration : IEntityTypeConfiguration<UserProfile>
{
    public void Configure(EntityTypeBuilder<UserProfile> builder)
    {
        builder.HasKey(k => k.Id);
        builder.Property(e => e.Id).ValueGeneratedOnAdd();
        builder.Property(e => e.Username).IsRequired();
        builder.Property(e => e.Password).IsRequired();
        builder.HasMany(c => c.Categories)
            .WithOne(c => c.Profile)
            .HasForeignKey(fk => fk.ProfileId)
            .OnDelete(DeleteBehavior.NoAction)
            .HasConstraintName("FK_Categories_UserProfile_ProfileId");
        builder.HasMany(t => t.ToDos)
            .WithOne(u => u.Profile)
            .HasForeignKey(fk => fk.ProfileId)
            .OnDelete(DeleteBehavior.NoAction)
            .HasForeignKey("FK_Todos_UserProfile_ProfileId");
    }
}