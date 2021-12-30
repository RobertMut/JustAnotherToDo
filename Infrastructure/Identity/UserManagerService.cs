using JustAnotherToDo.Application.Common.Exceptions;
using JustAnotherToDo.Application.Common.Interfaces;
using JustAnotherToDo.Application.Models;
using JustAnotherToDo.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace JustAnotherToDo.Infrastructure.Identity
{
    public class UserManagerService : IUserManager
    {
        private readonly IApplicationDbContext _context;

        public UserManagerService(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> CreateUserAsync(string userName, string password, CancellationToken ct)
        {
            var user = new UserProfile()
            {
                Username = userName,
                Password = password
            };
            var entity = await _context.Profiles.AddAsync(user, ct);
            await _context.SaveChangesAsync(ct);
            return entity.Entity.UserId;
        }

        public async Task<UserProfile> GetUserAsync(string userName)
        {
            var result = await _context.Profiles.FirstOrDefaultAsync(c => c.Username == userName);
            if (result == null) throw new NotFoundException(nameof(UserProfile), userName);
            return result;
        }

        public async Task<UserProfile> GetUserByIdAsync(Guid id)
        {
            var result = await _context.Profiles.FirstOrDefaultAsync(c => c.UserId == id);
            if (result == null) throw new NotFoundException(nameof(UserProfile), id);
            return result;
        }

        public async Task<Guid> UpdateProfileAsync(UserProfile profile, CancellationToken ct)
        {
            var result = _context.Profiles.Update(profile);
            await _context.SaveChangesAsync(ct);
            return result.Entity.UserId;
        }

        public async Task<Guid> DeleteUserAsync(Guid userId)
        {
            var result = await _context.Profiles.FirstOrDefaultAsync(c => c.UserId == userId);
            if (result == null) throw new NotFoundException(nameof(UserProfile), userId);
            var deleted = _context.Profiles.Remove(result);
            return deleted.Entity.UserId;
        }
    }
}
