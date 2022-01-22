using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using JustAnotherToDo.Application.Categories.Commands.CreateCategory;
using JustAnotherToDo.WebUI.IntegrationTests.Common;
using NUnit.Framework;
using WebUI.Controllers;

namespace JustAnotherToDo.WebUI.IntegrationTests.Controller.Category;

public class Post
{
    private CustomWebApplicationFactory<CategoryController> _factory;
    private StringContent _category;
    [SetUp]
    public async Task SetUp()
    {
        _factory = new CustomWebApplicationFactory<CategoryController>();
        _category = Utilities.GetRequestContent(new CreateCategoryCommand
        {
            Name = "Test",
            Color = "#FF00FF",
            ProfileId = Utilities.TestUserId
        });
    }

    [Test]
    public async Task ShouldPostNewCategory()
    {
        var client = await _factory.GetAuthenticatedClient();

        var response = await client.PostAsync("api/Category", _category);
        response.EnsureSuccessStatusCode();
    }
    [Test]
    public async Task AnonymousCantPost()
    {
        var client = _factory.CreateClient();
        var response = await client.PostAsync("api/Category", _category);
        Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
    }
}