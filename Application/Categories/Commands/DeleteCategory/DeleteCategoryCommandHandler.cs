
using JustAnotherToDo.Application.Common.Exceptions;
using JustAnotherToDo.Application.Common.Interfaces;
using JustAnotherToDo.Domain.Entities;
using MediatR;

namespace JustAnotherToDo.Application.Categories.Commands.DeleteCategory;

public class DeleteProfileCommandHandler : IRequestHandler<DeleteTodoCommand>
{
    private readonly IJustAnotherToDoDbContext _context;

    public DeleteProfileCommandHandler(IJustAnotherToDoDbContext _context)
    {
        this._context = _context;
    }

    public async Task<Unit> Handle(DeleteTodoCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Categories.FindAsync(request.Id);
        if (entity == null) throw new NotFoundException(nameof(Category), request.Id);
        _context.Categories.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}