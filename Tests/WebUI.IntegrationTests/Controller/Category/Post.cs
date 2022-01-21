using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using JustAnotherToDo.Application.Categories.Commands.CreateCategory;
using JustAnotherToDo.WebUI.IntegrationTests.Common;
using WebUI.Controllers;
using Xunit;

namespace JustAnotherToDo.WebUI.IntegrationTests.Controller.Category;

public class Post : IClassFixture<CustomWebApplicationFactory<CategoryController>>
{
    private CustomWebApplicationFactory<CategoryController> _factory;
    private StringContent _category;
    public Post(CustomWebApplicationFactory<CategoryController> factory)
    {
        _factory = factory;
        _category = Utilities.GetRequestContent(new CreateCategoryCommand
        {
            Name = "Test",
            Color = "#FF00FF",
            ProfileId = Utilities.TestUserId
        });
    }

    [Fact]
    public async Task ShouldPostNewCategory()
    {
        var client = await _factory.GetAuthenticatedClient();

        var response = await client.PostAsync("api/Category", _category);
        response.EnsureSuccessStatusCode();
    }
    [Fact]
    public async Task AnonymousCantPost()
    {
        var client = _factory.CreateClient();
        var response = await client.PostAsync("api/Category", _category);
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }
}