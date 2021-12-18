using JustAnotherToDo.Application.Common.Interfaces;
using JustAnotherToDo.Domain.Entities;
using MediatR;

namespace JustAnotherToDo.Application.Profiles.Commands.CreateProfile;

public class CreateTodoCommandHandler : IRequestHandler<CreateTodoCommand, Guid>
{
    private readonly IJustAnotherToDoDbContext _context;

    public CreateTodoCommandHandler(IJustAnotherToDoDbContext _context)
    {
        this._context = _context;
    }

    public async Task<Guid> Handle(CreateTodoCommand request, CancellationToken cancellationToken)
    {
        var entity = new UserProfile
        {
            Username = request.Username,
            Password = request.Password
        };
        _context.Profiles.Add(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return entity.Id;
    }
}