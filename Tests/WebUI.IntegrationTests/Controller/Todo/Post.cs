using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using JustAnotherToDo.Application.Todos.Commands.CreateTodo;
using JustAnotherToDo.WebUI.IntegrationTests.Common;
using NUnit.Framework;
using WebUI.Controllers;

namespace JustAnotherToDo.WebUI.IntegrationTests.Controller.Todo;

public class Post 
{
    private CustomWebApplicationFactory<ToDoController> _factory;
    private StringContent _todo;

    [SetUp]
    public async Task SetUp()
    {
        _factory = new CustomWebApplicationFactory<ToDoController>();
        _todo = Utilities.GetRequestContent(new CreateTodoCommand
        {
            Name = "New todo",
            EndDate = DateTime.Now.AddDays(5),
            ProfileId = Utilities.Test2Id
        });
    }

    [Test]
    public async Task ShouldPostNewTodo()
    {
        var client = await _factory.GetAuthenticatedClient();

        var response = await client.PostAsync("api/Todo", _todo);
        response.EnsureSuccessStatusCode();
    }
    [Test]
    public async Task AnonymousCantPost()
    {
        var client = _factory.CreateClient();
        var response = await client.PostAsync("api/Todo", _todo);
        Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
    }
}