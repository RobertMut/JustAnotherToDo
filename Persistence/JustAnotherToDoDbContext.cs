using JustAnotherToDo.Application.Common.Interfaces;
using JustAnotherToDo.Domain.Entities;
using JustAnotherToDo.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;

namespace JustAnotherToDo.Persistence
{
    public class JustAnotherToDoDbContext : DbContext, IJustAnotherToDoDbContext
    {
        public JustAnotherToDoDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<UserProfile> Profiles  { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ToDo> ToDos { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(JustAnotherToDoDbContext).Assembly);
        }
    }
    
}