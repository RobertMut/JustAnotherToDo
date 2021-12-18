using Microsoft.EntityFrameworkCore;

namespace JustAnotherToDo.Persistence;

public class JustAnotherToDoDbContextFactory : DesignTimeDbContextFactoryBase<JustAnotherToDoDbContext>
{
    protected override JustAnotherToDoDbContext CreateNewInstance(DbContextOptions<JustAnotherToDoDbContext> options)
    {
        return new JustAnotherToDoDbContext(options);
    }
}