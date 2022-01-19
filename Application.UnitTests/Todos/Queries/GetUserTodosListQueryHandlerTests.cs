using System;
using System.Threading;
using System.Threading.Tasks;
using JustAnotherToDo.Application.Todos.Queries.GetUserTodosList;
using JustAnotherToDo.Application.UnitTests.Common;
using NUnit.Framework;

namespace JustAnotherToDo.Application.UnitTests.Todos.Queries;

public class GetUserTodosListQueryHandlerTests : QueriesTestFixture
{

    private readonly Guid ProfileId = JustAnotherToDoContextFactory.ProfileId;


    [Test]
    public async Task GetUserTodos()
    {
        var handler = new GetUserTodosListQueryHandler(Context, Mapper);
        var result = await handler.Handle(new GetUserTodosListQuery()
        {
            ProfileId = ProfileId
        }, CancellationToken.None);
        Assert.IsInstanceOf(typeof(UserTodosListVm), result);
        Assert.IsTrue(result.Todos.Count == 3);
    }
}