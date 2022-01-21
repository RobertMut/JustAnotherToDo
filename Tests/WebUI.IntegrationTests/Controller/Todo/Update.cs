using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using JustAnotherToDo.Application.Todos.Commands.UpdateTodo;
using JustAnotherToDo.WebUI.IntegrationTests.Common;
using WebUI.Controllers;
using Xunit;

namespace JustAnotherToDo.WebUI.IntegrationTests.Controller.Todo;

public class Update : IClassFixture<CustomWebApplicationFactory<ToDoController>>
{
    private CustomWebApplicationFactory<ToDoController> _factory;
    private HttpClient _client;
    private readonly StringContent _todo;

    public Update(CustomWebApplicationFactory<ToDoController> factory)
    {
        _factory = factory;
        _client = _factory.GetAuthenticatedClient().Result;
        _todo = Utilities.GetRequestContent(new UpdateTodoCommand
        {
            Id = Utilities.ToDo2Id,
            Name = "Updated",
            EndTime = DateTime.Now.AddYears(1),
            CategoryId = Utilities.CategoryId
        });
    }
    [Fact]
    public async Task SuccessfullyUpdatesTodo()
    {
        var response = await _client
            .PutAsync($"api/Todo", _todo);
        response.EnsureSuccessStatusCode();

    }
    [Fact]
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
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);

    }
    [Fact]
    public async Task AnonymousCantUpdate()
    {
        var client = _factory.CreateClient();
        var response = await client
            .PutAsync($"api/Todo", _todo);
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }
}