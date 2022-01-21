using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace JustAnotherToDo.Persistence;

public abstract class DesignTimeDbContextFactoryBase<TContext> : IDesignTimeDbContextFactory<TContext> where TContext : DbContext
{
    private const string ConnectionStringName = "JustAnotherToDoDatabase";
    private const string AspNetCoreEnvironment = "ASPNETCORE_ENVIRONMENT";

    public TContext CreateDbContext(string[] args)
    {
        var path = Directory.GetCurrentDirectory() +
                   $"{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}WebUI";
        return Create(path, Environment.GetEnvironmentVariable(AspNetCoreEnvironment));
    }

    protected abstract TContext CreateNewInstance(DbContextOptions<TContext> options);

    private TContext Create(string path, string environmentName)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(path)
            .AddJsonFile("appsettings.json")
            .AddJsonFile("appsettings.Local.json", optional: true)
            .AddJsonFile($"appsettings.{environmentName}.json", optional:true)
            .AddEnvironmentVariables()
            .Build();
        var connectionString = configuration.GetConnectionString(ConnectionStringName);
        return Create(connectionString);
    }

    private TContext Create(string connectionString)
    {
        if (string.IsNullOrEmpty(connectionString))
            throw new ArgumentException($"Connection string '{ConnectionStringName}' is null or empty.",
                nameof(connectionString));
        Console.WriteLine($"DesignTimeDbContextFactoryBase.Create(string): Connection string: '{connectionString}'.");
        var optionsBuilder = new DbContextOptionsBuilder<TContext>();
        optionsBuilder.UseSqlServer(connectionString);
        return CreateNewInstance(optionsBuilder.Options);
    }
}