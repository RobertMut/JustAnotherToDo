﻿using AutoMapper;
using JustAnotherToDo.Application.Common.Mappings;
using JustAnotherToDo.Domain.Entities;

namespace JustAnotherToDo.Application.Profiles.Queries.GetProfilesList;

public class ProfileLookupDto : IMapFrom<UserProfile>
{
    public Guid Id { get; set; }
    public string Username { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<UserProfile, ProfileLookupDto>()
            .ForMember(i => i.Id, opt => opt.MapFrom(o => o.UserId));
    }
}