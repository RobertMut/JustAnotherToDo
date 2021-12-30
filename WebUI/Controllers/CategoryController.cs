﻿using JustAnotherToDo.Application.Categories.Commands.CreateCategory;
using JustAnotherToDo.Application.Categories.Commands.DeleteCategory;
using JustAnotherToDo.Application.Categories.Commands.UpdateCategory;
using JustAnotherToDo.Application.Categories.Queries.GetUserCategoriesList;
using JustAnotherToDo.Application.Common.Interfaces;
using JustAnotherToDo.Application.Profiles.Queries.GetProfileDetail;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers;
[ApiController]
[Authorize]
[Route("[controller]")]
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
        var user = await _mediator.Send(new GetProfileDetailQuery
        {
            Username = _currentUser.UserName
        });
        if (user == null) return BadRequest("User does not exist");
        var categories = await _mediator.Send(new GetUserCategoriesListQuery
        {
            ProfileId = user.Id
        });
        return categories;
    }

    [HttpPost]
    public async Task<IActionResult> PostCategory(CreateCategoryCommand command)
    {
        var guid = await _mediator.Send(command);
        return Ok(guid);
    }
    [HttpPut]
    public async Task<IActionResult> UpdateCategory(UpdateCategoryCommand command)
    {
        var guid = await _mediator.Send(command);
        return Ok(guid);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteCategory(DeleteCategoryCommand command)
    {
        var guid = await _mediator.Send(command);
        return Ok(guid);
    }
}