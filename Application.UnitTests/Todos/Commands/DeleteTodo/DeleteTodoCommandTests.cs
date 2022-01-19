using System;
using System.Threading;
using System.Threading.Tasks;
using JustAnotherToDo.Application.Common.Exceptions;
using JustAnotherToDo.Application.Todos.Commands.DeleteTodo;
using JustAnotherToDo.Application.UnitTests.Common;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace JustAnotherToDo.Application.UnitTests.Todos.Commands.DeleteTodo;
[TestFixture]
public class DeleteTodoCommandTests : CommandTestBase
{
    [Test]
    public async Task Handle_ThrowsNotFound()
    {
        var handle = new DeleteTodoCommandHandler(Context);
        var command = new DeleteTodoCommand()
        {
            Id = Guid.NewGuid()
        };
        Assert.ThrowsAsync<NotFoundException>(() => handle.Handle(command, CancellationToken.None));
    }
    [Test]
    public async Task Handle_DeleteTodo()
    {
        var handle = new DeleteTodoCommandHandler(Context);
        var todo = await Context.ToDos.FirstOrDefaultAsync(t => t.Name == "Task1");
        var command = new DeleteTodoCommand
        {
            Id = todo.Id,
        };
        await handle.Handle(command, CancellationToken.None);
        var result = await Context.ToDos.FindAsync(todo.Id);
        Assert.IsNull(result);
    }
}