using JustAnotherToDo.Domain.Entities;

namespace JustAnotherToDo.Application.Common.Interfaces;

public interface IUserManager
{
    Task<Guid> CreateUserAsync(string userName, string password, CancellationToken ct);
    Task<UserProfile> GetUserAsync(string userName);
    Task<UserProfile> GetUserByIdAsync(Guid userId);
    Task<Guid> UpdateProfileAsync(UserProfile profile, CancellationToken ct);
    Task<Guid> DeleteUserAsync(Guid userId, CancellationToken ct);
}