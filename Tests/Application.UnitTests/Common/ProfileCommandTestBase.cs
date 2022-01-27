using JustAnotherToDo.Application.Common.Interfaces;
using JustAnotherToDo.Infrastructure.Identity;
using JustAnotherToDo.Persistence;

namespace JustAnotherToDo.Application.UnitTests.Common;
public class ProfileCommandTestBase
{
    protected IUserManager Service;
    protected JustAnotherToDoDbContext Context;

    public ProfileCommandTestBase()
    {
        Context = JustAnotherToDoContextFactory.Create();
        Service = new SqlUserManagerService(Context);
    }

}