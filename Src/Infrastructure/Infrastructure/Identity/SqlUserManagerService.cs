using JustAnotherToDo.Application.Common.Exceptions;
using JustAnotherToDo.Application.Common.Interfaces;
using JustAnotherToDo.Domain.Entities;
using JustAnotherToDo.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace JustAnotherToDo.Infrastructure.Identity
{
    public class SqlUserManagerService : IUserManager
    {
        private readonly IJustAnotherToDoDbContext _context;

        public SqlUserManagerService(IJustAnotherToDoDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> CreateUserAsync(string userName, string password,AccessLevel permissions, CancellationToken ct)
        {
            var searchUser = await _context.Profiles.SingleOrDefaultAsync(u => u.Username == userName, ct);
            if (searchUser != null) throw new UserExistsException(nameof(UserProfile), userName);
            var user = new UserProfile()
            {
                Username = userName,
                Password = password,
                AccessLevel = permissions
            };
            var entity = await _context.Profiles.AddAsync(user, ct);
            await _context.SaveChangesAsync(ct);
            return entity.Entity.UserId;
        }

        public async Task<UserProfile> GetUserAsync(string userName, CancellationToken ct)
        {
            var result = await _context.Profiles.FirstOrDefaultAsync(u => u.Username == userName, ct);
            return result;
        }

        public async Task<UserProfile> GetUserByIdAsync(Guid id, CancellationToken ct)
        {
            var result = await _context.Profiles.FirstOrDefaultAsync(c => c.UserId == id, ct);
            return result;
        }

        public async Task<Guid> UpdateProfileAsync(UserProfile profile, CancellationToken ct)
        {
            var user = await _context.Profiles.FirstOrDefaultAsync(u => u.UserId == profile.UserId, ct);
            user.UserId = profile.UserId;
            if (!string.IsNullOrEmpty(profile.Password))
                user.Password = profile.Password;
            user.Username = profile.Username;
            user.AccessLevel = profile.AccessLevel;
            await _context.SaveChangesAsync(ct);
            return user.UserId;
        }

        public async Task<Guid> DeleteUserAsync(Guid userId, CancellationToken ct)
        {
            var result = await _context.Profiles.FirstOrDefaultAsync(c => c.UserId == userId, ct);
            if (result == null) throw new NotFoundException(nameof(UserProfile), userId);
            var deleted = _context.Profiles.Remove(result);
            await _context.SaveChangesAsync(ct);
            return deleted.Entity.UserId;
        }
    }
}
