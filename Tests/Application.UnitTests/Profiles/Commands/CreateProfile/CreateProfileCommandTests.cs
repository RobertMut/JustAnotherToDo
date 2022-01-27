using System;
using System.Threading;
using JustAnotherToDo.Application.Profiles.Commands.CreateProfile;
using JustAnotherToDo.Application.UnitTests.Common;
using MediatR;
using Moq;
using NUnit.Framework;

namespace JustAnotherToDo.Application.UnitTests.Profiles.Commands.CreateProfile;
public class CreateProfileCommandTests : ProfileCommandTestBase
{
    private Mock<IMediator> Mediator;
    private CreateProfileCommand.CreateProfileCommandHandler Handler;
    [SetUp]
    public void SetUp()
    {
        Mediator = new Mock<IMediator>();
        Handler = new CreateProfileCommand.CreateProfileCommandHandler(Service);
    }
    [Test]
    public void Handle()
    {

        Mediator.Setup(
                m => m.Send(It.IsAny<CreateProfileCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new Guid())
            .Verifiable("Profile not created");
        var profile = new CreateProfileCommand
        {
            Username = "Test3",
            Password = "Test3"
        };
        Mediator.Object.Send(profile, CancellationToken.None);
        Mediator.Verify(x => x.Send(It.IsAny<CreateProfileCommand>(),It.IsAny<CancellationToken>()), Times.Once);
        var result = Handler.Handle(profile, CancellationToken.None).Result;
        Assert.AreNotEqual(Guid.Empty, result);
    }
}