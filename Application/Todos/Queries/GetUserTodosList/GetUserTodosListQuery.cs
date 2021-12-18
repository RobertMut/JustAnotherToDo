using MediatR;

namespace JustAnotherToDo.Application.Todos.Queries.GetUserTodosList;

public class GetUserTodosListQuery : IRequest<UserTodosListVm>
{
    public Guid ProfileId { get; set; }   
}