using AutoMapper;
using JustAnotherToDo.Application.Common.Mappings;
using JustAnotherToDo.Domain.Entities;

namespace JustAnotherToDo.Application.Todos.Queries.GetUserTodosList;

public class UserTodosListVm
{
    public IList<UserTodoDto> Todos { get; set; }
}