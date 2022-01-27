using JustAnotherToDo.Application.Common.Exceptions;
using JustAnotherToDo.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace JustAnotherToDo.Application.Todos.Commands.UpdateTodo;

public class UpdateTodoCommand : IRequest
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public DateTime EndTime { get; set; }
    public Guid? CategoryId { get; set; }
    public class UpdateTodoCommandHandler : IRequestHandler<UpdateTodoCommand>
    {
        private readonly IJustAnotherToDoDbContext _context;

        public UpdateTodoCommandHandler(IJustAnotherToDoDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateTodoCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.ToDos.SingleOrDefaultAsync(c => c.Id == request.Id, cancellationToken);
            if (entity == null) throw new NotFoundException(nameof(Todos), request.Id);
            entity.Name = request.Name;
            //entity.CategoryId = (Guid)request.CategoryId;
            entity.EndDate = request.EndTime;
            entity.Id = request.Id;
            entity.CategoryId = request.CategoryId;
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }

    }
}