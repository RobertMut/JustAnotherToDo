using AutoMapper;
using AutoMapper.QueryableExtensions;
using JustAnotherToDo.Application.Common.Exceptions;
using JustAnotherToDo.Application.Common.Interfaces;
using JustAnotherToDo.Application.Common.Mappings;
using JustAnotherToDo.Application.Common.Wrappers;
using JustAnotherToDo.Application.Models;
using MediatR;

namespace JustAnotherToDo.Application.Profiles.Queries.GetProfilesWithPagination;

public class GetProfilesWithPaginationQuery : IRequest<PaginatedList<ProfilesDto>>
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}
public class GetProfilesWithPaginationQueryHandler : IRequestHandler<ContextualRequest<GetProfilesWithPaginationQuery, PaginatedList<ProfilesDto>>, PaginatedList<ProfilesDto>>
{
    private readonly IJustAnotherToDoDbContext _context;
    private readonly IMapper _mapper;

    public GetProfilesWithPaginationQueryHandler(IJustAnotherToDoDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginatedList<ProfilesDto>> Handle(ContextualRequest<GetProfilesWithPaginationQuery, PaginatedList<ProfilesDto>> request, CancellationToken cancellationToken)
    {
        var paginated = await _context.Profiles.ProjectTo<ProfilesDto>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(request.Data.PageNumber, request.Data.PageSize);
        if (paginated == null || paginated.Items.Count == 0)
            throw new NotFoundException(nameof(PaginatedList<ProfilesDto>), request.UserId);
        return paginated;
    }
}