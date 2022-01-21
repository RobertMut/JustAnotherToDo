using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using JustAnotherToDo.Application.Categories.Commands.UpdateCategory;
using JustAnotherToDo.WebUI.IntegrationTests.Common;
using WebUI.Controllers;
using Xunit;

namespace JustAnotherToDo.WebUI.IntegrationTests.Controller.Category;

public class Update : IClassFixture<CustomWebApplicationFactory<CategoryController>>
{
    private CustomWebApplicationFactory<CategoryController> _factory;
    private HttpClient _client;
    private StringContent _category;
    public Update(CustomWebApplicationFactory<CategoryController> factory)
    {
        _factory = factory;
        _client = _factory.GetAuthenticatedClient().Result;
        _category = Utilities.GetRequestContent(new UpdateCategoryCommand
        {
            Id = Utilities.CategoryId,
            Name = "Not important",
            Color = "#00FF00"
        });
    }
    [Fact]
    public async Task SuccessfullyUpdatesCategory()
    {

        var response = await _client
            .PutAsync($"api/Category", _category);
        response.EnsureSuccessStatusCode();

    }
    [Fact]
    public async Task WrongIdCategoryUpdate()
    {
        var category = new UpdateCategoryCommand
        {
            Id = Guid.NewGuid(),
            Name = "Test",
            Color = "#FFFF00"
        };
        var content = Utilities.GetRequestContent(category);
        var response = await _client
            .PutAsync($"api/Category", content);
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);

    }
    [Fact]
    public async Task AnonymousCantUpdate()
    {
        var client = _factory.CreateClient();
        var response = await client
            .PutAsync($"api/Category", _category);
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }
}