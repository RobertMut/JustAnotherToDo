using JustAnotherToDo.Application.Common.Interfaces;
using JustAnotherToDo.Domain.Entities;

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
        //await SeedCategory(cancellationToken);
        //await SeedTodo(cancellationToken);
    }

    public async Task SeedUser(CancellationToken cancellationToken)
    {
        var user = new UserProfile
        {
            Username = "Administrator",
            Password = "1234",
        };
        await _context.Profiles.AddAsync(user, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    //public async Task SeedCategory(CancellationToken cancellationToken)
    //{
    //    var user = _context.Profiles.Single(u => u.Username == "Administrator");
    //    var category = new Category
    //    {
    //        Name = "green",
    //        Color = "#006400",
    //        UserId = user.Id,
    //    };
    //    await _context.Categories.AddAsync(category, cancellationToken);
    //    await _context.SaveChangesAsync(cancellationToken);
    //}

    //public async Task SeedTodo(CancellationToken cancellationToken)
    //{
    //    var user = _context.Profiles.Single(u => u.Username == "Administrator");
    //    var category = _context.Categories.Single(c => c.Name == "green" && c.UserId == user.Id);
    //    var todo = new ToDo
    //    {
    //        Name = "Test Todo",
    //        CreationDate = DateTime.Now,
    //        CategoryId = category.Id,
    //        ProfileId = user.Id
    //    };
    //    await _context.ToDos.AddAsync(todo, cancellationToken);
    //    await _context.SaveChangesAsync(cancellationToken);
    //}
}