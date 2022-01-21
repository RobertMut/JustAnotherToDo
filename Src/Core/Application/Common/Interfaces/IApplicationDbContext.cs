using JustAnotherToDo.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace JustAnotherToDo.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<UserProfile> Profiles { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}