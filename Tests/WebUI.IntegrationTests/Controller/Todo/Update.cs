using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using JustAnotherToDo.Application.Todos.Commands.UpdateTodo;
using JustAnotherToDo.WebUI.IntegrationTests.Common;
using NUnit.Framework;
using WebUI.Controllers;

namespace JustAnotherToDo.WebUI.IntegrationTests.Controller.Todo;

public class Update
{
    private CustomWebApplicationFactory<ToDoController> _factory;
    private HttpClient _client;
    private StringContent _todo;
    [SetUp]
    public async Task SetUp()
    {
        _factory = new CustomWebApplicationFactory<ToDoController>();
        _client = await _factory.GetAuthenticatedClient();
        _todo = Utilities.GetRequestContent(new UpdateTodoCommand
        {
            Id = Utilities.ToDo2Id,
            Name = "Updated",
            EndTime = DateTime.Now.AddYears(1),
            CategoryId = Utilities.CategoryId
        });
    }
    [Test]
    public async Task SuccessfullyUpdatesTodo()
    {
        var response = await _client
            .PutAsync($"api/Todo", _todo);
        response.EnsureSuccessStatusCode();

    }
    [Test]
    public async Task WrongIdTodoUpdate()
    {
        var todo = new UpdateTodoCommand
        {
            Id = Guid.NewGuid(),
            Name = "Updated",
            EndTime = DateTime.Now.AddHours(5),
            CategoryId = Utilities.CategoryId,

        };
        var content = Utilities.GetRequestContent(todo);
        var response = await _client
            .PutAsync($"api/Todo", content);
        Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);

    }
    [Test]
    public async Task AnonymousCantUpdate()
    {
        var client = _factory.CreateClient();
        var response = await client
            .PutAsync($"api/Todo", _todo);
        Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
    }
}