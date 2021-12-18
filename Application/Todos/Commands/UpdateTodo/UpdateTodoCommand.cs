using MediatR;

namespace JustAnotherToDo.Application.Todos.Commands.UpdateTodo;

public class UpdateTodoCommand : IRequest<Guid>, IRequest<Unit>
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public DateTime EndTime { get; set; }
    public Guid? CategoryId { get; set; }
}