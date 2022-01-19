using AutoMapper;
using JustAnotherToDo.Application.Common.Mappings;

using NUnit.Framework;
using IConfigurationProvider = AutoMapper.IConfigurationProvider;

namespace JustAnotherToDo.Application.UnitTests.Mappings;

public class MappingTestsFixture
{
    public IConfigurationProvider ConfigurationProvider { get; private set; }
    public IMapper Mapper { get; private set; }
    [SetUp]
    public virtual void SetUp()
    {
        ConfigurationProvider = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<MappingProfile>();
        });
        Mapper = ConfigurationProvider.CreateMapper();
    }


}