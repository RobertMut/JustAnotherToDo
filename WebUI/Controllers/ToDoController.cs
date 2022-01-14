using JustAnotherToDo.Application.Common.Interfaces;
using JustAnotherToDo.Application.Profiles.Queries.GetProfileDetail;
using JustAnotherToDo.Application.Todos.Commands.CreateTodo;
using JustAnotherToDo.Application.Todos.Commands.DeleteTodo;
using JustAnotherToDo.Application.Todos.Commands.UpdateTodo;
using JustAnotherToDo.Application.Todos.Queries.GetUserTodosList;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers;
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Route("api/[controller]")]

public class ToDoController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ICurrentUserService _currentUser;

    public ToDoController(IMediator mediator, ICurrentUserService currentUser)
    {
        _mediator = mediator;
        _currentUser = currentUser;
    }

    // GET
    [HttpGet]
    public async Task<ActionResult<UserTodosListVm>> GetUserToDos()
    {
        var user = await _mediator.Send(new GetProfileDetailQuery
        {
            Username = _currentUser.UserName
        });
        if (user == null) return BadRequest("User does not exist");
        var todos = await _mediator.Send(new GetUserTodosListQuery
        {
            ProfileId = user.Id
        });
        return todos;
    }

    [HttpPost]
    public async Task<IActionResult> PostTodo(CreateTodoCommand command)
    {
        var user = await _mediator.Send(new GetProfileDetailQuery
        {
            Username = _currentUser.UserName
        });
        command.ProfileId = user.Id;
        var guid = await _mediator.Send(command);
        return Ok(guid);
    }
    [HttpPut]
    public async Task<IActionResult> UpdateTodo(UpdateTodoCommand command)
    {
        var guid = await _mediator.Send(command);
        return Ok(guid);
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(DeleteTodoCommand command)
    {
        var guid = await _mediator.Send(command);
        return Ok(guid);
    }
}