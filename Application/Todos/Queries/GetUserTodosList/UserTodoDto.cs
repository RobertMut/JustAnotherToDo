using AutoMapper;
using JustAnotherToDo.Application.Common.Mappings;
using JustAnotherToDo.Domain.Entities;

namespace JustAnotherToDo.Application.Todos.Queries.GetUserTodosList;

public class UserTodoDto : IMapFrom<ToDo>
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public DateTime CreationDate { get; set; }
    public DateTime EndDate { get; set; }
    public Guid CategoryId { get; set; }
    public Guid ProfileId { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<ToDo, UserTodosListVm>()
            .ForMember(p => p.ProfileId, opt => opt.MapFrom(up => up.ProfileId));
    }
}