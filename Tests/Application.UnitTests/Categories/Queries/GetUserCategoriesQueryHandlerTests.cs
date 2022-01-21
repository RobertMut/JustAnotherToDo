using System;
using System.Threading;
using System.Threading.Tasks;
using JustAnotherToDo.Application.Categories.Queries.GetUserCategoriesList;
using JustAnotherToDo.Application.UnitTests.Common;
using NUnit.Framework;

namespace JustAnotherToDo.Application.UnitTests.Categories.Queries;
[TestFixture]
public class GetUserCategoriesQueryHandlerTests : QueriesTestFixture
{

    private Guid _profileId = JustAnotherToDoContextFactory.ProfileId;
    [Test]
    public async Task GetUserCategories()
    {
        Console.WriteLine(_profileId.ToString());
        var handler = new GetUserCategoriesListQueryHandler(Context, Mapper);
        var result = await handler.Handle(new GetUserCategoriesListQuery
        {
            ProfileId = _profileId
        }, CancellationToken.None);
        Assert.IsInstanceOf(typeof(UserCategoriesListVm), result);
        Assert.IsTrue(result.Categories.Count == 2);
    }
}