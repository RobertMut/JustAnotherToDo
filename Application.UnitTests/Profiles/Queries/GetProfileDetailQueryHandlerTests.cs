using System;
using System.Threading;
using System.Threading.Tasks;
using JustAnotherToDo.Application.Categories.Queries.GetUserCategoriesList;
using JustAnotherToDo.Application.Profiles.Queries.GetProfileDetail;
using JustAnotherToDo.Application.UnitTests.Common;
using JustAnotherToDo.Domain.Enums;
using NUnit.Framework;

namespace JustAnotherToDo.Application.UnitTests.Profiles.Queries;

public class GetProfileDetailQueryHandlerTests : QueriesTestFixture
{
    [Test]
    public async Task GetProfileDetails()
    {
        var handler = new GetProfileDetailQueryHandler(Service, Mapper);
        var result = await handler.Handle(new GetProfileDetailQuery()
        {
            Username = "TestUser",
        }, CancellationToken.None);
        Assert.IsInstanceOf(typeof(ProfileDetailVm), result);
        Assert.IsTrue(result.AccessLevel == AccessLevel.User);
    }
}