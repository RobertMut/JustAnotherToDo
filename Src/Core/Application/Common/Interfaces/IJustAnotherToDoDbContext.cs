using JustAnotherToDo.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace JustAnotherToDo.Application.Common.Interfaces;

public interface IJustAnotherToDoDbContext
{
    DbSet<Category> Categories { get; set; }
    DbSet<ToDo> ToDos { get; set; }
    DbSet<UserProfile> Profiles { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}