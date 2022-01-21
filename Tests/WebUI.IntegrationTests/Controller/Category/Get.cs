using System.Net;
using System.Threading.Tasks;
using JustAnotherToDo.Application.Categories.Queries.GetUserCategoriesList;
using JustAnotherToDo.WebUI.IntegrationTests.Common;
using WebUI.Controllers;
using Xunit;

namespace JustAnotherToDo.WebUI.IntegrationTests.Controller.Category;

public class Get : IClassFixture<CustomWebApplicationFactory<CategoryController>>
{
    private CustomWebApplicationFactory<CategoryController> _factory;
    public Get(CustomWebApplicationFactory<CategoryController> factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task ReturnsUserCategories()
    {
        var client = await _factory.GetAuthenticatedClient();
        var response = await client.GetAsync("api/Category");
        response.EnsureSuccessStatusCode();
        var vm = await Utilities.GetResponseContent<UserCategoriesListVm>(response);
        Assert.IsType<UserCategoriesListVm>(vm);
        Assert.NotEmpty(vm.Categories);
    }
    [Fact]
    public async Task AnonymousCantGet()
    {
        var client = _factory.CreateClient();
        var response = await client.GetAsync("api/Category");
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }
}