using JustAnotherToDo.Application.Common.Interfaces;
using JustAnotherToDo.Domain.Entities;
using JustAnotherToDo.Domain.Enums;

namespace JustAnotherToDo.Application.System.Commands.InitialData;

public class InitialDataSeeder
{
    private readonly IJustAnotherToDoDbContext _context;
    private readonly IUserManager _manager;

    public InitialDataSeeder(IJustAnotherToDoDbContext context, IUserManager manager)
    {
        _context = context;
        _manager = manager;
    }

    public async Task SeedAsync(CancellationToken cancellationToken)
    {
        await SeedUser(cancellationToken);
        await SeedCategory(cancellationToken);
        await SeedTodo(cancellationToken);
    }

    public async Task SeedUser(CancellationToken cancellationToken)
    {

        await _manager.CreateUserAsync("Administrator", "1234",AccessLevel.Administrator, cancellationToken);

    }

    public async Task SeedCategory(CancellationToken cancellationToken)
    {
        var user = await _manager.GetUserAsync("Administrator", cancellationToken);
        var category = new Category
        {
            Name = "green",
            Color = "#006400",
            ProfileId = user.UserId
        };
        await _context.Categories.AddAsync(category, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task SeedTodo(CancellationToken cancellationToken)
    {
        var user = await _manager.GetUserAsync("Administrator", cancellationToken);
        var category = _context.Categories.Single(c => c.Name == "green" && c.ProfileId == user.UserId);
        var todo = new ToDo
        {
            Name = "Test Todo",
            CreationDate = DateTime.Now,
            CategoryId = category.Id,
            ProfileId = user.UserId
        };
        await _context.ToDos.AddAsync(todo, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }
}