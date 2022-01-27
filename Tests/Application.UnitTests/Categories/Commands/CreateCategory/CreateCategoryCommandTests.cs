using System;
using System.Threading;
using JustAnotherToDo.Application.Categories.Commands.CreateCategory;
using JustAnotherToDo.Application.UnitTests.Common;
using MediatR;
using Moq;
using NUnit.Framework;

namespace JustAnotherToDo.Application.UnitTests.Categories.Commands.CreateCategory;
public class CreateCategoryCommandTests : CommandTestBase
{
    private Mock<IMediator> _mediator;
    private CreateCategoryCommand.CreateCategoryCommandHandler _handler;
    [SetUp]
    public void SetUp()
    {
        _mediator = new Mock<IMediator>();
        _handler = new CreateCategoryCommand.CreateCategoryCommandHandler(Context);
    }
    [Test]
    public void Handle()
    {

        _mediator.Setup(
                m => m.Send(It.IsAny<CreateCategoryCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new Guid())
            .Verifiable("Category not created");
        var category = new CreateCategoryCommand
        {
            Name = "Example",
            Color = "#00FF00",
            ProfileId = JustAnotherToDoContextFactory.ProfileId
        };
        _mediator.Object.Send(category, CancellationToken.None);
        _mediator.Verify(x => x.Send(It.IsAny<CreateCategoryCommand>(),It.IsAny<CancellationToken>()), Times.Once);
        var result = _handler.Handle(category, CancellationToken.None).Result;
        Assert.AreNotEqual(Guid.Empty, result);
    }
}