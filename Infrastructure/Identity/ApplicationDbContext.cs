using JustAnotherToDo.Application.Common.Interfaces;
using JustAnotherToDo.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace JustAnotherToDo.Infrastructure.Identity
{
    public class ApplicationDBContext : DbContext, IApplicationDbContext
    {
        
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {

        }
        public DbSet<UserProfile> Profiles { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<UserProfile>().HasKey(k => k.UserId);
        }
    }
}