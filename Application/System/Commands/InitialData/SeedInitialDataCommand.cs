using JustAnotherToDo.Application.Common.Interfaces;
using MediatR;

namespace JustAnotherToDo.Application.System.Commands.InitialData;

public class SeedInitialDataCommand : IRequest
{
}

public class SeedSampleDataCommandHandler : IRequestHandler<SeedInitialDataCommand>
{
    private readonly IJustAnotherToDoDbContext _context;
    private readonly IUserManager _manager;

    public SeedSampleDataCommandHandler(IJustAnotherToDoDbContext context, IUserManager manager)
    {
        _context = context;
        _manager = manager;
    }

    public async Task<Unit> Handle(SeedInitialDataCommand request, CancellationToken cancellationToken)
    {
        var seeder = new InitialDataSeeder(_context, _manager);
        await seeder.SeedAsync(cancellationToken);
        return Unit.Value;;
    }
}