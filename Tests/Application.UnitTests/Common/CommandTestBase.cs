using System;
using JustAnotherToDo.Persistence;

namespace JustAnotherToDo.Application.UnitTests.Common;
public class CommandTestBase : IDisposable
{
    protected JustAnotherToDoDbContext Context;

    public CommandTestBase()
    {
        Context = JustAnotherToDoContextFactory.Create();
    }

    public void Dispose()
    {
        JustAnotherToDoContextFactory.Destroy(Context);
    }
}