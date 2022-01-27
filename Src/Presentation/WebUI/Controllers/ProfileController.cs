using JustAnotherToDo.Application.Common.Exceptions;
using JustAnotherToDo.Application.Common.Interfaces;
using JustAnotherToDo.Application.Common.Wrappers;
using JustAnotherToDo.Application.Models;
using JustAnotherToDo.Application.Profiles.Commands.CreateProfile;
using JustAnotherToDo.Application.Profiles.Commands.DeleteProfile;
using JustAnotherToDo.Application.Profiles.Commands.UpdateProfile;
using JustAnotherToDo.Application.Profiles.Queries.GetProfileDetail;
using JustAnotherToDo.Application.Profiles.Queries.GetProfilesWithPagination;
using JustAnotherToDo.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebUI.Attributes;

namespace WebUI.Controllers;
[ApiController]
[Route("api/[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class ProfileController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ICurrentUserService _currentUser;

    public ProfileController(IMediator mediator, ICurrentUserService currentUser)
    {
        _mediator = mediator;
        _currentUser = currentUser;
    }

    // GET
    [HttpGet]
    public async Task<ActionResult<PaginatedList<ProfilesDto>>> GetProfileList([FromQuery] GetProfilesWithPaginationQuery query)
    {
        var userId = _currentUser.User.Id;
        var request =
            new ContextualRequest<GetProfilesWithPaginationQuery, PaginatedList<ProfilesDto>>(query,
                userId);
        var paginatedProfiles = await _mediator.Send(request);
        return paginatedProfiles;

    }

    [HttpGet("profile")]
    public async Task<ActionResult<ProfileDetailVm>> GetProfile()
    {
        return _currentUser.User;

    }
    [AllowAnonymous]
    [SecurityHeaders]
    [HttpPost]
    public async Task<IActionResult> PostProfile([FromBody] CreateProfileCommand command)
    {
        var guid = await _mediator.Send(command);
        return Ok(guid);
    }
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpPut]
    public async Task<IActionResult> UpdateProfile([FromBody] UpdateProfileCommand command)
    {

        var guid = await _mediator.Send(command);
        return Ok(guid);

    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProfile([FromRoute] Guid id)
    {
        var guid = await _mediator.Send(new DeleteProfileCommand
        {
            UserId = id
        });
        return Ok(guid);


    }
}