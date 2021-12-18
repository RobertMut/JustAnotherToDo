using AutoMapper;
using JustAnotherToDo.Application.Common.Mappings;
using JustAnotherToDo.Domain.Entities;

namespace JustAnotherToDo.Application.Categories.Queries.GetUserCategoriesList;

public class CategoryDto : IMapFrom<Category>
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string Name { get; set; }
    public string Color { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<Category, UserCategoriesListVm>()
            .ForMember(u => u.ProfileId, opt => opt.MapFrom(i => i.UserId));
    }
}