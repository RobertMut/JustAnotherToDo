using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using JustAnotherToDo.Application.Todos.Commands.CreateTodo;
using JustAnotherToDo.WebUI.IntegrationTests.Common;
using WebUI.Controllers;
using Xunit;

namespace JustAnotherToDo.WebUI.IntegrationTests.Controller.Todo;

public class Post : IClassFixture<CustomWebApplicationFactory<ToDoController>>
{
    private CustomWebApplicationFactory<ToDoController> _factory;
    private readonly StringContent _todo;

    public Post(CustomWebApplicationFactory<ToDoController> factory)
    {
        _factory = factory;
        _todo = Utilities.GetRequestContent(new CreateTodoCommand
        {
            Name = "New todo",
            EndDate = DateTime.Now.AddDays(5),
            ProfileId = Utilities.Test2Id
        });
    }

    [Fact]
    public async Task ShouldPostNewTodo()
    {
        var client = await _factory.GetAuthenticatedClient();

        var response = await client.PostAsync("api/Todo", _todo);
        response.EnsureSuccessStatusCode();
    }
    [Fact]
    public async Task AnonymousCantPost()
    {
        var client = _factory.CreateClient();
        var response = await client.PostAsync("api/Todo", _todo);
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }
}