using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using JustAnotherToDo.WebUI.IntegrationTests.Common;
using NUnit.Framework;
using WebUI.Controllers;

namespace JustAnotherToDo.WebUI.IntegrationTests.Controller.Todo;

public class Delete
{
    private CustomWebApplicationFactory<ToDoController> _factory;
    private HttpClient _client;
    [SetUp]
    public async Task SetUp()
    {
        _factory = new CustomWebApplicationFactory<ToDoController>();
        _client = await _factory.GetAuthenticatedClient();
    }

    [Test]
    public async Task SuccessfullyDeletesTodo()
    {
        
        var response = await _client
               .DeleteAsync($"api/Todo/{Utilities.ToDoId}");
        response.EnsureSuccessStatusCode();

    }
    [Test]
    public async Task NotFoundTodo()
    {
       
        var response = await _client
            .DeleteAsync($"api/Todo/{Guid.NewGuid()}");
        Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);

    }
    [Test]
    public async Task AnonymousCantDelete()
    {

        var response = await _factory.CreateClient()
            .DeleteAsync($"api/ToDo/{Utilities.ToDoId}");
        Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);

    }
}