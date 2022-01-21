using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using JustAnotherToDo.WebUI.IntegrationTests.Common;
using WebUI.Controllers;
using Xunit;

namespace JustAnotherToDo.WebUI.IntegrationTests.Controller.Category;

public class Delete : IClassFixture<CustomWebApplicationFactory<CategoryController>>
{
    private CustomWebApplicationFactory<CategoryController> _factory;
    private HttpClient _client;
    public Delete(CustomWebApplicationFactory<CategoryController> factory)
    {
        _factory = factory;
        _client = _factory.GetAuthenticatedClient().Result;
    }

    [Fact]
    public async Task SuccessfullyDeletesCategory()
    {

        var response = await _client
               .DeleteAsync($"api/Category/{Utilities.Category2Id}");
        response.EnsureSuccessStatusCode();

    }
    [Fact]
    public async Task NotFoundCategory()
    {
       
        var response = await _client
            .DeleteAsync($"api/Category/{Guid.NewGuid()}");
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);

    }
    [Fact]
    public async Task AnonymousCantDelete()
    {

        var response = await _factory.CreateClient()
            .DeleteAsync($"api/Category/{Utilities.CategoryId}");
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);

    }
}