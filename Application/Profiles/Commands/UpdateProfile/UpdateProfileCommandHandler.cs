using JustAnotherToDo.Application.Common.Exceptions;
using JustAnotherToDo.Application.Common.Interfaces;
using MediatR;

namespace JustAnotherToDo.Application.Profiles.Commands.UpdateProfile;

public class UpdateTodoCommandHandler : IRequestHandler<UpdateTodoCommand>
{
    private readonly IJustAnotherToDoDbContext _context;

    public UpdateTodoCommandHandler(IJustAnotherToDoDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(UpdateTodoCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Profiles.FindAsync(request.Id);
        if (entity == null) throw new NotFoundException(nameof(Profiles), request.Id);
        entity.Id = request.Id;
        entity.Password = request.Password;
        entity.Username = request.Name;
        await _context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}