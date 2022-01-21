using System;
using System.Threading;
using JustAnotherToDo.Application.Todos.Commands.CreateTodo;
using JustAnotherToDo.Application.UnitTests.Common;
using MediatR;
using Moq;
using NUnit.Framework;

namespace JustAnotherToDo.Application.UnitTests.Todos.Commands.CreateTodo;
[TestFixture]
public class CreateTodoCommandTests : CommandTestBase
{
    [Test]
    public void Handle()
    {
        var mediator = new Mock<IMediator>();

        mediator.Setup(
                m => m.Send(It.IsAny<CreateTodoCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new Guid())
            .Verifiable("Todo not created");
        var handler = new CreateTodoCommandHandler(Context);
        var todo = new CreateTodoCommand
        {
            Name = "Todo",
            EndDate = DateTime.Now.AddDays(6),
            CategoryId = null,
            ProfileId = default
        };
        mediator.Object.Send(todo, CancellationToken.None);
        mediator.Verify(x => x.Send(It.IsAny<CreateTodoCommand>(), It.IsAny<CancellationToken>()), Times.Once);
        var result = handler.Handle(todo, CancellationToken.None).Result;
        Assert.AreNotEqual(Guid.Empty, result);
    }
}