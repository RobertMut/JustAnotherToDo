using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using JustAnotherToDo.WebUI.IntegrationTests.Common;
using WebUI.Controllers;
using Xunit;

namespace JustAnotherToDo.WebUI.IntegrationTests.Controller.Todo;

public class Delete : IClassFixture<CustomWebApplicationFactory<ToDoController>>
{
    private CustomWebApplicationFactory<ToDoController> _factory;
    private HttpClient _client;
    public Delete(CustomWebApplicationFactory<ToDoController> factory)
    {
        _factory = factory;
        _client = _factory.GetAuthenticatedClient().Result;
    }

    [Fact]
    public async Task SuccessfullyDeletesTodo()
    {
        
        var response = await _client
               .DeleteAsync($"api/Todo/{Utilities.ToDoId}");
        response.EnsureSuccessStatusCode();

    }
    [Fact]
    public async Task NotFoundTodo()
    {
       
        var response = await _client
            .DeleteAsync($"api/Todo/{Guid.NewGuid()}");
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);

    }
    [Fact]
    public async Task AnonymousCantDelete()
    {

        var response = await _factory.CreateClient()
            .DeleteAsync($"api/ToDo/{Utilities.ToDoId}");
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);

    }
}