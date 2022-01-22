using JustAnotherToDo.Application.Categories.Commands.CreateCategory;
using JustAnotherToDo.Application.Categories.Commands.DeleteCategory;
using JustAnotherToDo.Application.Categories.Commands.UpdateCategory;
using JustAnotherToDo.Application.Categories.Queries.GetUserCategoriesList;
using JustAnotherToDo.Application.Common.Exceptions;
using JustAnotherToDo.Application.Common.Interfaces;
using JustAnotherToDo.Application.Profiles.Queries.GetProfileDetail;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers;
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Route("api/[controller]")]
public class CategoryController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ICurrentUserService _currentUser;

    public CategoryController(IMediator mediator, ICurrentUserService currentUser)
    {
        _mediator = mediator;
        _currentUser = currentUser;
    }

    // GET
    [HttpGet]
    public async Task<ActionResult<UserCategoriesListVm>> GetUserCategories()
    {
        var categories = await _mediator.Send(new GetUserCategoriesListQuery
        {
            Username = _currentUser.UserName
        });
        return categories;
    }

    [HttpPost]
    public async Task<IActionResult> PostCategory([FromBody] CreateCategoryCommand command)
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
    public async Task<IActionResult> UpdateCategory([FromBody] UpdateCategoryCommand command)
    {

        var guid = await _mediator.Send(command);
        return Ok(guid);

    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCategory([FromRoute] Guid id)
    {

        var guid = await _mediator.Send(new DeleteCategoryCommand
        {
            Id = id
        });
        return Ok(guid);
    }
}