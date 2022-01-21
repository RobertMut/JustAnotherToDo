using JustAnotherToDo.Application.Common.Interfaces;
using JustAnotherToDo.Infrastructure.Identity;

namespace JustAnotherToDo.Application.UnitTests.Common;
public class ProfileCommandTestBase
{
    protected IUserManager Service;
    protected ApplicationDBContext Context;

    public ProfileCommandTestBase()
    {
        Context = ApplicationContextFactory.Create();
        Service = new SqlUserManagerService(Context);
    }

}