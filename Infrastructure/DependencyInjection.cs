using IdentityServer4.Models;
using JustAnotherToDo.Application.Common.Interfaces;
using JustAnotherToDo.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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
}