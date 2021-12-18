using JustAnotherToDo.Application.Common.Exceptions;
using JustAnotherToDo.Application.Common.Interfaces;
using JustAnotherToDo.Domain.Entities;
using MediatR;

namespace JustAnotherToDo.Application.Profiles.Commands.DeleteProfile;

public class DeleteTodoCommandHandler : IRequestHandler<DeleteProfileCommand>
{
    private readonly IJustAnotherToDoDbContext _context;

    public DeleteTodoCommandHandler(IJustAnotherToDoDbContext _context)
    {
        this._context = _context;
    }

    public async Task<Unit> Handle(DeleteProfileCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Profiles.FindAsync(request.Id);
        if (entity == null) throw new NotFoundException(nameof(Category), request.Id);
        _context.Profiles.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}