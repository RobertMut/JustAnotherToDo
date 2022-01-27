using JustAnotherToDo.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace JustAnotherToDo.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<JustAnotherToDoDbContext>(opt =>
            opt.UseSqlServer(configuration.GetConnectionString("JustAnotherToDoDatabase")), ServiceLifetime.Transient);
        services.AddScoped<IJustAnotherToDoDbContext>(provider => provider.GetService<JustAnotherToDoDbContext>());
        return services;
    }
}