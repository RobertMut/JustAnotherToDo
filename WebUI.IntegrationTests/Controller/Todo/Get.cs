using System.Net;
using System.Threading.Tasks;
using JustAnotherToDo.Application.Todos.Queries.GetUserTodosList;
using JustAnotherToDo.WebUI.IntegrationTests.Common;
using WebUI.Controllers;
using Xunit;

namespace JustAnotherToDo.WebUI.IntegrationTests.Controller.Todo;

public class Get : IClassFixture<CustomWebApplicationFactory<ToDoController>>
{
    private CustomWebApplicationFactory<ToDoController> _factory;
    public Get(CustomWebApplicationFactory<ToDoController> factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task ReturnsUserTodos()
    {
        var client = await _factory.GetAuthenticatedClient();
        var response = await client.GetAsync("api/Todo");
        response.EnsureSuccessStatusCode();
        var vm = await Utilities.GetResponseContent<UserTodosListVm>(response);
        Assert.IsType<UserTodosListVm>(vm);
        Assert.NotEmpty(vm.Todos);
    }
    [Fact]
    public async Task AnonymousCantGet()
    {
        var client = _factory.CreateClient();
        var response = await client.GetAsync("api/ToDo");
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }
}