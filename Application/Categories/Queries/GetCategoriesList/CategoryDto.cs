using AutoMapper;
using JustAnotherToDo.Application.Common.Mappings;
using JustAnotherToDo.Domain.Entities;

namespace JustAnotherToDo.Application.Categories.Queries.GetCategoriesList;

public class CategoryDto : IMapFrom<Category>
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Color { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Category, CategoryDto>()
            .ForMember(i => i.Id, opt => opt.MapFrom(c => c.Id))
            .ForMember(n => n.Name, opt => opt.MapFrom(c => c.Color));
    }
}