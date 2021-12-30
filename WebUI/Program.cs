using JustAnotherToDo.Application;
using JustAnotherToDo.Application.Common.Interfaces;
using JustAnotherToDo.Infrastructure;
using JustAnotherToDo.Infrastructure.Identity;
using JustAnotherToDo.Infrastructure.Swagger;
using JustAnotherToDo.Persistence;
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
builder.Services.AddHealthChecks().AddDbContextCheck<JustAnotherToDoDbContext>();
builder.Services.AddHealthChecks().AddDbContextCheck<ApplicationDBContext>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddMvc()
    .AddNewtonsoftJson();
builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();

builder.Services.AddSpaStaticFiles(conf =>
{
    conf.RootPath = "ClientApp/dist";
});

// Add Swagger
builder.Services.AddSwagger(builder.Configuration);

var app = builder.Build();
await app.Services.DatabaseInitializer<JustAnotherToDoDbContext, ApplicationDBContext>();


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
