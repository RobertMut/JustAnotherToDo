using JustAnotherToDo.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JustAnotherToDo.Persistence.Configurations;

public class ToDosConfiguration : IEntityTypeConfiguration<ToDo>
{
    public void Configure(EntityTypeBuilder<ToDo> builder)
    {
        builder.HasKey(k => k.Id);
        builder.Property(e => e.Id).ValueGeneratedOnAdd();
        builder.Property(e => e.Name).IsRequired();
        builder.Property(e => e.CreationDate).IsRequired();
        builder.Property(e => e.EndDate).IsRequired(false);
        builder.Property(e => e.Category).IsRequired(false);
    }
}