using System;
using System.Threading;
using System.Threading.Tasks;
using JustAnotherToDo.Application.Categories.Commands.DeleteCategory;
using JustAnotherToDo.Application.Common.Exceptions;
using JustAnotherToDo.Application.Profiles.Commands.DeleteProfile;
using JustAnotherToDo.Application.UnitTests.Common;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace JustAnotherToDo.Application.UnitTests.Categories.Commands.DeleteCategory;
public class DeleteCategoryCommandTests : CommandTestBase
{

    [Test]
    public async Task HandleThrowsNotFound()
    {
        var handler = new DeleteCategoryCommand.DeleteCategoryCommandHandler(Context);
        var command = new DeleteCategoryCommand
        {
            Id = Guid.NewGuid()
        };

        Assert.ThrowsAsync<NotFoundException>(() => handler.Handle(command, CancellationToken.None));
    }
    [Test]
    public async Task HandleDeleteCategory()
    {
        var handler = new DeleteCategoryCommand.DeleteCategoryCommandHandler(Context);
        var category = await Context.Categories.FirstOrDefaultAsync(c => c.Name == "Not important");
        var command = new DeleteCategoryCommand
        {
            Id = category.Id
        };
        await handler.Handle(command, CancellationToken.None);
        var result = await Context.Categories.FindAsync(category.Id);
        Assert.IsNull(result);
    }
}