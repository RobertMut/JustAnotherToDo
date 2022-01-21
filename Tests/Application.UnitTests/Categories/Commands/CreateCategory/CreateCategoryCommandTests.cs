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
    private Mock<IMediator> Mediator;
    private CreateCategoryCommandHandler Handler;
    [SetUp]
    public void SetUp()
    {
        Mediator = new Mock<IMediator>();
        Handler = new CreateCategoryCommandHandler(Context);
    }
    [Test]
    public void Handle()
    {

        Mediator.Setup(
                m => m.Send(It.IsAny<CreateCategoryCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new Guid())
            .Verifiable("Category not created");
        var category = new CreateCategoryCommand
        {
            Name = "Example",
            Color = "#00FF00",
            ProfileId = JustAnotherToDoContextFactory.ProfileId
        };
        Mediator.Object.Send(category, CancellationToken.None);
        Mediator.Verify(x => x.Send(It.IsAny<CreateCategoryCommand>(),It.IsAny<CancellationToken>()), Times.Once);
        var result = Handler.Handle(category, CancellationToken.None).Result;
        Assert.AreNotEqual(Guid.Empty, result);
    }
}