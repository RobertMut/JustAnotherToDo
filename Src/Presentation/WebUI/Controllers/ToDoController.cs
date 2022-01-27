using JustAnotherToDo.Application.Common.Exceptions;
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
        var todos = await _mediator.Send(new GetUserTodosListQuery
        {
            UserId = _currentUser.User.Id
    });
        return todos;
    }

    [HttpPost]
    public async Task<IActionResult> PostTodo([FromBody]CreateTodoCommand command)
    {
        command.ProfileId = _currentUser.User.Id;
        var guid = await _mediator.Send(command);
        return Ok(guid);
    }
    [HttpPut]
    public async Task<IActionResult> UpdateTodo([FromBody]UpdateTodoCommand command)
    {
        var guid = await _mediator.Send(command);
        return Ok(guid);


    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute]Guid id)
    {

        var guid = await _mediator.Send(new DeleteTodoCommand
        {
            Id = id
        });
        return Ok(guid);


    }
}