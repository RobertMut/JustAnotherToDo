using System;
using System.Threading;
using System.Threading.Tasks;
using JustAnotherToDo.Application.Todos.Queries.GetUserTodosList;
using JustAnotherToDo.Application.UnitTests.Common;
using JustAnotherToDo.Infrastructure.Identity;
using NUnit.Framework;

namespace JustAnotherToDo.Application.UnitTests.Todos.Queries;

public class GetUserTodosListQueryHandlerTests : QueriesTestFixture
{

    [Test]
    public async Task GetUserTodos()
    {
        var handler = new GetUserTodosListQueryHandler(Context);
        var result = await handler.Handle(new GetUserTodosListQuery()
        {
            UserId = JustAnotherToDoContextFactory.ProfileId
        }, CancellationToken.None);
        Assert.IsInstanceOf(typeof(UserTodosListVm), result);
        Assert.IsTrue(result.Todos.Count == 3);
    }
}