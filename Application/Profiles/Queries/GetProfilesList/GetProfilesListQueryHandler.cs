using AutoMapper;
using AutoMapper.QueryableExtensions;
using JustAnotherToDo.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace JustAnotherToDo.Application.Profiles.Queries.GetProfilesList;

public class GetProfilesListQueryHandler : IRequestHandler<GetProfilesListQuery, ProfilesListVm>
{
    private readonly IJustAnotherToDoDbContext _context;
    private readonly IMapper _mapper;

    public GetProfilesListQueryHandler(IJustAnotherToDoDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ProfilesListVm> Handle(GetProfilesListQuery request, CancellationToken cancellationToken)
    {
        var profiles = await _context.Profiles.ProjectTo<ProfileLookupDto>(_mapper.ConfigurationProvider)
            .ToArrayAsync();
        var vm = new ProfilesListVm
        {
            Profiles = profiles
        };
        return vm;
    }
}