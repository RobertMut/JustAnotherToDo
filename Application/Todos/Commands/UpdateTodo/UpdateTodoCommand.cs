using MediatR;

namespace JustAnotherToDo.Application.Todos.Commands.UpdateTodo;

public class UpdateTodoCommand : IRequest
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public DateTime EndTime { get; set; }
    public Guid? CategoryId { get; set; }
}