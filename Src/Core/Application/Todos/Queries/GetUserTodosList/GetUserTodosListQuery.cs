using MediatR;

namespace JustAnotherToDo.Application.Todos.Queries.GetUserTodosList;

public class GetUserTodosListQuery : IRequest<UserTodosListVm>
{
    public string Username { get; set; }   
}