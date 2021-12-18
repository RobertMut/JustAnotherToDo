using JustAnotherToDo.Application.Common.Exceptions;
using JustAnotherToDo.Application.Common.Interfaces;
using MediatR;

namespace JustAnotherToDo.Application.Todos.Commands.UpdateTodo;

public class UpdateTodoCommandHandler : IRequestHandler<UpdateTodoCommand>
{
    private readonly IJustAnotherToDoDbContext _context;

    public UpdateTodoCommandHandler(IJustAnotherToDoDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(UpdateTodoCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.ToDos.FindAsync(request.Id);
        if (entity == null) throw new NotFoundException(nameof(Todos), request.Id);
        entity.Name = request.Name;
        entity.CategoryId = (Guid)request.CategoryId;
        entity.EndDate = request.EndTime;
        entity.Id = request.Id;
        await _context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}