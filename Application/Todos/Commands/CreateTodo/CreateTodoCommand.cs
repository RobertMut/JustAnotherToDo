using MediatR;

namespace JustAnotherToDo.Application.Todos.Commands.CreateTodo;

public class CreateTodoCommand : IRequest<Guid>
{
    public string Name { get; set; }
    public DateTime CreationDate  { get; set; }
    public DateTime EndDate { get; set; }
    public Guid? CategoryId { get; set; }
    public Guid ProfileId { get; set; }
}