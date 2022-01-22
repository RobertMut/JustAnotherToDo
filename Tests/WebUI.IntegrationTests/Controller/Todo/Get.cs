using System.Net;
using System.Threading.Tasks;
using JustAnotherToDo.Application.Todos.Queries.GetUserTodosList;
using JustAnotherToDo.WebUI.IntegrationTests.Common;
using NUnit.Framework;
using WebUI.Controllers;


namespace JustAnotherToDo.WebUI.IntegrationTests.Controller.Todo;

public class Get 
{
    private CustomWebApplicationFactory<ToDoController> _factory;

    [SetUp]
    public void SetUp()
    {
        _factory = new CustomWebApplicationFactory<ToDoController>();
    }

    [Test]
    public async Task ReturnsUserTodos()
    {
        var client = await _factory.GetAuthenticatedClient();
        var response = await client.GetAsync("api/Todo");
        response.EnsureSuccessStatusCode();
        var vm = await Utilities.GetResponseContent<UserTodosListVm>(response);
        Assert.IsInstanceOf<UserTodosListVm>(vm);
        Assert.NotNull(vm.Todos);
    }
    [Test]
    public async Task AnonymousCantGet()
    {
        var client = _factory.CreateClient();
        var response = await client.GetAsync("api/ToDo");
        Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
    }
}