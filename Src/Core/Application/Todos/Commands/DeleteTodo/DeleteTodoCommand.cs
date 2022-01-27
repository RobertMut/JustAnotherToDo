using JustAnotherToDo.Application.Common.Exceptions;
using JustAnotherToDo.Application.Common.Interfaces;
using JustAnotherToDo.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace JustAnotherToDo.Application.Todos.Commands.DeleteTodo;

public class DeleteTodoCommand : IRequest
{
    public Guid Id { get; set; }
    public class DeleteTodoCommandHandler : IRequestHandler<DeleteTodoCommand>
    {
        private readonly IJustAnotherToDoDbContext _context;

        public DeleteTodoCommandHandler(IJustAnotherToDoDbContext context)
        {
            this._context = context;
        }

        public async Task<Unit> Handle(DeleteTodoCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.ToDos.SingleOrDefaultAsync(t => t.Id == request.Id, cancellationToken);
            if (entity == null) throw new NotFoundException(nameof(Category), request.Id);
            _context.ToDos.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}