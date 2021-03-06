using FluentValidation.AspNetCore;
using JustAnotherToDo.Application;
using JustAnotherToDo.Application.Common.Interfaces;
using JustAnotherToDo.Infrastructure;
using JustAnotherToDo.Infrastructure.Swagger;
using JustAnotherToDo.Persistence;
using Serilog;
using WebUI.Filters;
using WebUI.Services;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.AddSerilog();
builder.Configuration
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
    .AddJsonFile($"appsettings.Local.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables();


builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddPersistence(builder.Configuration);
builder.Services.AddApplication();
builder.Services.AddLogging(c => c.AddSerilog());
builder.Services.AddHealthChecks().AddDbContextCheck<JustAnotherToDoDbContext>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddMvc(opt =>
    {
        opt.Filters.Add<ApiExceptionFilterAttribute>();
    }).AddFluentValidation(x => x.AutomaticValidationEnabled = false)
    .AddNewtonsoftJson();
builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();
builder.Services.AddCors();

builder.Services.AddSpaStaticFiles(conf =>
{
    conf.RootPath = "ClientApp/dist";
});

// Add Swagger
builder.Services.AddSwagger(builder.Configuration);

var app = builder.Build();
await app.Services.DatabaseInitializer<JustAnotherToDoDbContext>();


if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseDeveloperExceptionPage();
    app.UseHsts();
}

app.UseHealthChecks("/health");
app.UseCors(b =>
{
    b.AllowAnyOrigin();
    b.AllowAnyMethod();
    b.AllowAnyHeader();
});
app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseIdentityServer();
app.UseAuthorization();
app.UseSwaggerWithAuthorization();
app.UseSpa(spa =>
{
    spa.Options.SourcePath = "ClientApp";
});
app.UseEndpoints(endpoints =>
{
    endpoints.MapDefaultControllerRoute();
});
app.Run();

namespace WebUI
{
    public partial class Program {}
}
