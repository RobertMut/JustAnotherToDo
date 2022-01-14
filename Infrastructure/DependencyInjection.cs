using System.Reflection;
using JustAnotherToDo.Application.Common.Interfaces;
using JustAnotherToDo.Application.Models.Auth;
using JustAnotherToDo.Application.System.Commands.InitialData;
using JustAnotherToDo.Infrastructure.Identity;
using JustAnotherToDo.Infrastructure.Swagger.Filters;
using JustAnotherToDo.Infrastructure.Swagger.Options;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;

namespace JustAnotherToDo.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IUserManager, UserManagerService>();

        services.AddDbContext<ApplicationDBContext>(opt =>
            opt.UseSqlServer(configuration.GetConnectionString("JustAnotherToDoDatabase")));
        services.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDBContext>());

        services.AddIdentityServer(opt =>
            {
                opt.UserInteraction.LoginUrl = "/Account/Login";
                opt.UserInteraction.LogoutUrl = "/Account/Logout";
                opt.UserInteraction.ErrorUrl = "/Home/error";
                opt.UserInteraction.LoginReturnUrlParameter = "returnUrl";
                opt.Events.RaiseErrorEvents = true;
                opt.Events.RaiseFailureEvents = true;
                opt.Events.RaiseInformationEvents = true;
                opt.Events.RaiseSuccessEvents = true;
            })
            .AddInMemoryIdentityResources(configuration.GetSection("IdentityServer:IdentityResources"))
            .AddInMemoryApiResources(configuration.GetSection("IdentityServer:Resources"))
            .AddInMemoryApiScopes(configuration.GetSection("IdentityServer:Scopes"))
            .AddInMemoryClients(configuration.GetSection("IdentityServer:Clients"))
            //.AddAspNetIdentity<UserProfile>()
            //.AddJwtBearerClientAuthentication()
            .AddDeveloperSigningCredential();
        services.AddHttpContextAccessor();
        services.AddAuthentication(auth =>
            {
                //auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                //auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(bearer =>
            {
                var section = configuration.GetSection("IdentityServer");
                bearer.Authority = section.GetValue<string>("Authority");
                bearer.RequireHttpsMetadata = false;
                bearer.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = true,
                    ValidAudiences = new []{"web_ui", "swagger_ui"},
                    ValidateIssuer = true,
                    ValidateLifetime = true
                };
                bearer.BackchannelHttpHandler = new HttpClientHandler()
                {
                    ServerCertificateCustomValidationCallback =
                        HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
                };
            }).AddLocalApi();
        services.AddAuthorization();
        services.Configure<AuthenticationOptions>(configuration);
        return services;
    }

    public static IServiceCollection AddSwagger(this IServiceCollection services, IConfiguration configuration)
    {
        var swaggerOptions = SwaggerOptions.ReadFromIConfiguration(configuration);
        services.AddTransient<IOptions<SwaggerOptions>>(provider => new OptionsWrapper<SwaggerOptions>(swaggerOptions));

        var authority = configuration["IdentityServer:Authority"];
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", swaggerOptions.ApiInfo);
            options.CustomSchemaIds(x => x.FullName);
            string xmlPath = null;

            if (swaggerOptions.XmlCommentsFilePath != null)
                xmlPath = swaggerOptions.XmlCommentsFilePath;
            else
            {
                var autoPath = Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetEntryAssembly().GetName().Name}.xml");
                if (File.Exists(autoPath))
                    xmlPath = autoPath;
            }

            if (xmlPath != null)
                options.IncludeXmlComments(xmlPath);

            if (swaggerOptions.OAuth != null)
            {
                options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.OAuth2,
                    Flows = new OpenApiOAuthFlows()
                    {
                        Implicit = new OpenApiOAuthFlow()
                        {
                            AuthorizationUrl = new Uri(Path.Combine(authority + "/connect/authorize")),
                            TokenUrl = new Uri(Path.Combine(authority + "/connect/token")),
                            Scopes = swaggerOptions.OAuth.Scopes


                        }
                    }
                });
                options.OperationFilter<AuthorizeCheckOperationFilter>();
            }
        });

        services.AddSwaggerGenNewtonsoftSupport();
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
            var appContext = services.GetRequiredService<ApplicationDBContext>();
            var user = appContext.Profiles.FirstOrDefaultAsync(u => u.Username == "Administrator");
            if (user == null)
            {
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
}