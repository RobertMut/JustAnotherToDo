using AutoMapper;
using AutoMapper.QueryableExtensions;
using JustAnotherToDo.Application.Common.Interfaces;
using JustAnotherToDo.Application.Common.Mappings;
using JustAnotherToDo.Application.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace JustAnotherToDo.Application.Profiles.Queries.GetProfilesWithPagination;

public class GetProfilesWithPaginationQueryHandler : IRequestHandler<GetProfilesWithPaginationQuery, PaginatedList<ProfilesDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetProfilesWithPaginationQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginatedList<ProfilesDto>> Handle(GetProfilesWithPaginationQuery request, CancellationToken cancellationToken)
    {
        return await _context.Profiles.ProjectTo<ProfilesDto>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(request.PageNumber, request.PageSize);
    }
}