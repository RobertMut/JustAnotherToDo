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
        builder.Property(e => e.Categories).IsRequired(false);

    }
}