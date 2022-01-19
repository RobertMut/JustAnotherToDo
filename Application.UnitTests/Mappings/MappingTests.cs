using JustAnotherToDo.Application.Categories.Queries.GetUserCategoriesList;
using JustAnotherToDo.Application.Profiles.Queries.GetProfileDetail;
using JustAnotherToDo.Application.Profiles.Queries.GetProfilesWithPagination;
using JustAnotherToDo.Application.Todos.Queries.GetUserTodosList;
using JustAnotherToDo.Domain.Entities;
using NUnit.Framework;

namespace JustAnotherToDo.Application.UnitTests.Mappings;
[TestFixture]
public class MappingTests : MappingTestsFixture
{

    [Test]
    public void MappingShouldBeValid()
    {
        ConfigurationProvider.AssertConfigurationIsValid();
    }

    [Test]
    public void CategoryShouldBeMappedToCategoryDto()
    {
        var entity = new Category();
        var result = Mapper.Map<CategoryDto>(entity);
        Assert.IsNotNull(result);
        Assert.IsInstanceOf(typeof(CategoryDto), result);
    }

    [Test]
    public void TodoShouldBeMappedToUserToDoDto()
    {
        var entity = new ToDo();
        var result = Mapper.Map<UserTodoDto>(entity);
        Assert.IsNotNull(result);
        Assert.IsInstanceOf(typeof(UserTodoDto), result);
    }

    [Test]
    public void UserProfileShouldBeMappedToProfilesDto()
    {
        var entity = new UserProfile();
        var result = Mapper.Map<ProfilesDto>(entity);
        Assert.IsNotNull(result);
        Assert.IsInstanceOf(typeof(ProfilesDto), result);
    }
    [Test]
    public void UserProfileShouldBeMappedToProfileDetailVm()
    {
        var entity = new UserProfile();
        var result = Mapper.Map<ProfileDetailVm>(entity);
        Assert.IsNotNull(result);
        Assert.IsInstanceOf(typeof(ProfileDetailVm), result);
    }


}