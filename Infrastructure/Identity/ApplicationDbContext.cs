using IdentityServer4.EntityFramework.Options;
using JustAnotherToDo.Application.Common.Interfaces;
using JustAnotherToDo.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

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