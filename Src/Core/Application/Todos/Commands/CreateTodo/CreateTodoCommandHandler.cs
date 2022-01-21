using JustAnotherToDo.Application.Common.Interfaces;
using JustAnotherToDo.Domain.Entities;
using MediatR;

namespace JustAnotherToDo.Application.Todos.Commands.CreateTodo;

public class CreateTodoCommandHandler : IRequestHandler<CreateTodoCommand, Guid>
{
    private readonly IJustAnotherToDoDbContext _context;

    public CreateTodoCommandHandler(IJustAnotherToDoDbContext context)
    {
        this._context = context;
    }

    public async Task<Guid> Handle(CreateTodoCommand request, CancellationToken cancellationToken)
    {
        var entity = new ToDo
        {
            Name = request.Name,
            CreationDate = DateTime.Now,
            EndDate = request.EndDate,
            CategoryId = request.CategoryId,
            ProfileId = request.ProfileId,
        };
        _context.ToDos.Add(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return entity.Id;
    }
}