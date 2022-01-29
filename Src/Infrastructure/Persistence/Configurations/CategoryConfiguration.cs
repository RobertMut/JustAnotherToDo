using JustAnotherToDo.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JustAnotherToDo.Persistence.Configurations;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.HasKey(k => k.Id);
        builder.Property(k => k.Id).ValueGeneratedOnAdd();
        builder.Property(e => e.Name).IsRequired();
        builder.HasOne(c => c.Profile).WithMany(c => c.Categories).HasForeignKey(c => c.ProfileId);
    }
}