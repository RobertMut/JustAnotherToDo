using MediatR;

namespace JustAnotherToDo.Application.Todos.Commands.DeleteTodo;

public class DeleteTodoCommand : IRequest
{
    public Guid Id { get; set; }
}