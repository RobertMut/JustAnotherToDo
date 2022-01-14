using AutoMapper;
using JustAnotherToDo.Application.Common.Mappings;
using JustAnotherToDo.Domain.Entities;

namespace JustAnotherToDo.Application.Todos.Queries.GetUserTodosList;

public class UserTodoDto : IMapFrom<ToDo>
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public DateTime CreationDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string Category { get; set; }
    public string Color { get; set; }
    public Guid ProfileId { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<Category, UserTodoDto>()
            .ForMember(c => c.Color, opt => opt.MapFrom(a => a.Color))
            .ForMember(c => c.Category, opt => opt.MapFrom(ca => ca.Name));
        profile.CreateMap<ToDo, UserTodoDto>()
            .ForMember(i => i.Id, opt => opt.MapFrom(id => id.Id))
            .ForMember(p => p.ProfileId, opt => opt.MapFrom(up => up.ProfileId))
            .ForMember(p => p.Name, opt => opt.MapFrom(u => u.Name));
    }
}