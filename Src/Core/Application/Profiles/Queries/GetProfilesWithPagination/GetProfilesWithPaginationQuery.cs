using JustAnotherToDo.Application.Models;
using MediatR;

namespace JustAnotherToDo.Application.Profiles.Queries.GetProfilesWithPagination;

public class GetProfilesWithPaginationQuery : IRequest<PaginatedList<ProfilesDto>>
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}