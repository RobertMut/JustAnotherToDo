using System.Threading;
using System.Threading.Tasks;
using JustAnotherToDo.Application.Common.Wrappers;
using JustAnotherToDo.Application.Models;
using JustAnotherToDo.Application.Profiles.Queries.GetProfileDetail;
using JustAnotherToDo.Application.Profiles.Queries.GetProfilesWithPagination;
using JustAnotherToDo.Application.UnitTests.Common;
using JustAnotherToDo.Domain.Enums;
using NUnit.Framework;

namespace JustAnotherToDo.Application.UnitTests.Profiles.Queries;

public class GetProfilesWithPaginationQueryHandlerTests : QueriesTestFixture
{
    [Test]
    public async Task GetPagedProfiles()
    {
        var handler = new GetProfilesWithPaginationQueryHandler(Context, Mapper);
        var result = await handler.Handle(new ContextualRequest<GetProfilesWithPaginationQuery, PaginatedList<ProfilesDto>>(
            new GetProfilesWithPaginationQuery(), JustAnotherToDoContextFactory.ProfileId), CancellationToken.None);
        Assert.IsInstanceOf(typeof(PaginatedList<ProfilesDto>), result);
        Assert.IsTrue(result.TotalCount == 3);
    }

}