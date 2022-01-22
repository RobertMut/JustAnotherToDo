using System.Net;
using System.Threading.Tasks;
using JustAnotherToDo.Application.Categories.Queries.GetUserCategoriesList;
using JustAnotherToDo.WebUI.IntegrationTests.Common;
using NUnit.Framework;
using WebUI.Controllers;

namespace JustAnotherToDo.WebUI.IntegrationTests.Controller.Category;

public class Get 
{
    private CustomWebApplicationFactory<CategoryController> _factory;
    [SetUp]
    public void SetUp()
    {
        _factory = new CustomWebApplicationFactory<CategoryController>();
    }

    [Test]
    public async Task ReturnsUserCategories()
    {
        var client = await _factory.GetAuthenticatedClient();
        var response = await client.GetAsync("api/Category");
        response.EnsureSuccessStatusCode();
        var vm = await Utilities.GetResponseContent<UserCategoriesListVm>(response);
        Assert.IsInstanceOf<UserCategoriesListVm>(vm);
        Assert.NotNull(vm.Categories);
    }
    [Test]
    public async Task AnonymousCantGet()
    {
        var client = _factory.CreateClient();
        var response = await client.GetAsync("api/Category");
        Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
    }
}