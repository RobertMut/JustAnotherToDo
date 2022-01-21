using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using JustAnotherToDo.Application.Common.Interfaces;
using JustAnotherToDo.Infrastructure.Identity;
using JustAnotherToDo.Persistence;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace JustAnotherToDo.WebUI.IntegrationTests.Common;

public class CustomWebApplicationFactory<TStartup>
    : WebApplicationFactory<TStartup> where TStartup : class
{


    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        //builder.UseEnvironment("Test");
        builder.UseTestServer();
        builder.ConfigureServices(services =>
        {
            services.AddEntityFrameworkInMemoryDatabase();
            services.Remove(GetDescriptor<ApplicationDBContext>(services));
            services.Remove(GetDescriptor<JustAnotherToDoDbContext>(services));
            services.AddDbContext<ApplicationDBContext>(options =>
            {
                options.UseInMemoryDatabase("InMemoryDbForTesting");
            });
            services.AddDbContext<JustAnotherToDoDbContext>(options =>
            {
                options.UseInMemoryDatabase("InMemoryDbForTesting");
            });
            services.AddScoped<IJustAnotherToDoDbContext, JustAnotherToDoDbContext>();
            services.AddScoped<IApplicationDbContext, ApplicationDBContext>();
            services.AddTransient<IAuthenticationSchemeProvider, MockSchemeProvider>();

            var sp = services.BuildServiceProvider();

            using (var scope = sp.CreateScope())
            {
                var scopedServices = scope.ServiceProvider;
                var appDb = scopedServices.GetRequiredService<ApplicationDBContext>();
                var db = scopedServices.GetRequiredService<JustAnotherToDoDbContext>();
                var logger = scopedServices
                    .GetRequiredService<ILogger<CustomWebApplicationFactory<TStartup>>>();

                try
                {
                    Utilities.InitializeDbForTests(db);
                    Utilities.InitializeApplicationDbForTests(appDb);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "An error occurred seeding the " +
                                        "database with test messages. Error: {Message}", ex.Message);
                }
            }
        });

    }

    private ServiceDescriptor GetDescriptor<TContext>(IServiceCollection services) where TContext : DbContext
    {
        return services.SingleOrDefault(
            d => d.ServiceType ==
                 typeof(DbContextOptions<TContext>));
    }

    public async Task<HttpClient> GetAuthenticatedClient()
    {
        var client = this.CreateClient(this.ClientOptions);
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Test", "Test");
        return client;
    }

}