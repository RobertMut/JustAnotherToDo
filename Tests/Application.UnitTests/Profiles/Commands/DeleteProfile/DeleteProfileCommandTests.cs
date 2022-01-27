using System;
using System.Runtime.InteropServices.ComTypes;
using System.Threading;
using System.Threading.Tasks;
using JustAnotherToDo.Application.Categories.Commands.DeleteCategory;
using JustAnotherToDo.Application.Common.Exceptions;
using JustAnotherToDo.Application.Profiles.Commands.DeleteProfile;
using JustAnotherToDo.Application.UnitTests.Common;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace JustAnotherToDo.Application.UnitTests.Profiles.Commands.DeleteProfile;
public class DeleteProfileCommandTests : ProfileCommandTestBase
{
    private DeleteProfileCommand.DeleteProfileCommandHandler Handler;
    [SetUp]
    public void SetUp()
    {
        Handler = new DeleteProfileCommand.DeleteProfileCommandHandler(Service);
    }
    [Test]
    public async Task HandleThrowsNotFound()
    {
        var command = new DeleteProfileCommand()
        {
            UserId = Guid.NewGuid()
        };

        Assert.ThrowsAsync<NotFoundException>(() => Handler.Handle(command, CancellationToken.None));
    }
    [Test]
    public async Task HandleDeleteProfile()
    {
        var profile = await Context.Profiles.FirstOrDefaultAsync(u => u.Username == "Test");
        var command = new DeleteProfileCommand()
        {
            UserId = profile.UserId
        };
        await Handler.Handle(command, CancellationToken.None);
        var result = await Context.Profiles.FindAsync(profile.UserId);
        Assert.IsNull(result);
    }
}