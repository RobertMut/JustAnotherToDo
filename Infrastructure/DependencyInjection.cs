using IdentityServer4.Models;
using JustAnotherToDo.Application.Common.Interfaces;
using JustAnotherToDo.Application.System.Commands.InitialData;
using JustAnotherToDo.Infrastructure.Identity;
using MediatR;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace JustAnotherToDo.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IUserManager, UserManagerService>();
        services.AddDbContext<ApplicationDBContext>(opt =>
            opt.UseSqlServer(configuration.GetConnectionString("JustAnotherToDoDatabase")));
        services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDBContext>()
            .AddDefaultTokenProviders();

        services.AddIdentityServer(opt =>
            {
                opt.UserInteraction.LoginUrl = "/Account/Login";
                opt.UserInteraction.LogoutUrl = "/Account/Logout";
                opt.UserInteraction.ErrorUrl = "/Home/error";
            })
            .AddInMemoryIdentityResources(new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile()
            })
            .AddInMemoryApiScopes(configuration.GetSection("Identity:ApiScopes"))
            .AddInMemoryClients(configuration.GetSection("Identity:Clients"))
            .AddAspNetIdentity<ApplicationUser>();
        services.AddAuthentication()
            .AddLocalApi()
            .AddCookie();
        return services;
    }

    public static IWebHostBuilder AddSerilog(this IWebHostBuilder webHost)
    {
        webHost.UseSerilog((hostingContext, loggerConfiguration) =>
            loggerConfiguration.ReadFrom.Configuration(hostingContext.Configuration));
        return webHost;
    }

    public static async Task DatabaseInitializer<TContext, TIdentityContext>(this IServiceProvider host) where TContext : DbContext
    where TIdentityContext : DbContext
    {
        using (var scope = host.CreateScope())
        {
            var services = scope.ServiceProvider;
            try
            {
                var tContext = services.GetRequiredService<TContext>();
                tContext.Database.Migrate();
                var identityContext = services.GetRequiredService<TIdentityContext>();
                identityContext.Database.Migrate();
                var mediator = services.GetRequiredService<IMediator>();
                await mediator.Send(new SeedInitialDataCommand(), CancellationToken.None);

            }
            catch (Exception ex)
            {
                
            }
        }
    }
}