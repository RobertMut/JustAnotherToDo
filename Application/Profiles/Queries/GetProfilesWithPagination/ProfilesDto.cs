using AutoMapper;
using JustAnotherToDo.Application.Common.Mappings;
using JustAnotherToDo.Domain.Entities;
using JustAnotherToDo.Domain.Enums;

namespace JustAnotherToDo.Application.Profiles.Queries.GetProfilesWithPagination;

public class ProfilesDto : IMapFrom<UserProfile>
{
    public Guid UserId { get; set; }
    public string Username { get; set; }
    public AccessLevel AccessLevel { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<UserProfile, ProfilesDto>()
            .ForMember(u => u.UserId, opt => opt.MapFrom(p => p.UserId));
    }
}