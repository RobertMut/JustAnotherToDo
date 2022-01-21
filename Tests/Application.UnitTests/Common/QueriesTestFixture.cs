using System;
using AutoMapper;
using JustAnotherToDo.Application.Common.Interfaces;
using JustAnotherToDo.Application.Common.Mappings;
using JustAnotherToDo.Infrastructure.Identity;
using JustAnotherToDo.Persistence;
using NUnit.Framework;

namespace JustAnotherToDo.Application.UnitTests.Common;

public class QueriesTestFixture : IDisposable
{
    public JustAnotherToDoDbContext Context { get; private set; }
    public ApplicationDBContext ApplicationContext { get; private set; }
    public IMapper Mapper { get; private set; }
    public IUserManager Service { get; private set; }
    [SetUp]
    public virtual void SetUp()
    {
        
        ApplicationContext = ApplicationContextFactory.Create();
        Service = new SqlUserManagerService(ApplicationContext);
        Context = JustAnotherToDoContextFactory.Create();
        var configurationProvider = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<MappingProfile>();
        });
        Mapper = configurationProvider.CreateMapper();
    }

    public void Dispose()
    {
        JustAnotherToDoContextFactory.Destroy(Context);
    }
}