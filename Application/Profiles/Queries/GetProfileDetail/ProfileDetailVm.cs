using AutoMapper;
using JustAnotherToDo.Application.Common.Mappings;
using JustAnotherToDo.Domain.Entities;
using JustAnotherToDo.Domain.Enums;

namespace JustAnotherToDo.Application.Profiles.Queries.GetProfileDetail;

public class ProfileDetailVm : IMapFrom<UserProfile>
{
    public Guid Id { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public AccessLevel AccessLevel { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<UserProfile, ProfileDetailVm>()
            .ForMember(i => i.Id, opt => opt.MapFrom(u => u.UserId));
    }
}