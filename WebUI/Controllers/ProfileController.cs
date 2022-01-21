using JustAnotherToDo.Application.Common.Exceptions;
using JustAnotherToDo.Application.Common.Interfaces;
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
        var user = await _mediator.Send(new GetProfileDetailQuery
        {
            Username = _currentUser.UserName
        });

        if (user.AccessLevel != AccessLevel.Administrator) return StatusCode(401);
        var paginatedProfiles = await _mediator.Send(query);
        if (paginatedProfiles.Items.Count == 0) return NotFound("Empty");
        return paginatedProfiles;

    }

    [HttpGet("profile")]
    public async Task<ActionResult<ProfileDetailVm>> GetProfile([FromQuery] string? username)
    {
        var user = await _mediator.Send(new GetProfileDetailQuery
        {
            Username = _currentUser.UserName
        });
        if (user == null) return BadRequest("User does not exist");
        if (user.AccessLevel != AccessLevel.Administrator && user.Username != username) return StatusCode(401);
        try
        {
            var profile = await _mediator.Send(new GetProfileDetailQuery
            {
                Username = user.Username
            });
            return profile;
        }
        catch (NotFoundException)
        {
            return NotFound("User not found!");
        }


    }
    [AllowAnonymous]
    [SecurityHeaders]
    [HttpPost]
    public async Task<IActionResult> PostProfile(CreateProfileCommand command)
    {
        var guid = await _mediator.Send(command);
        if (guid == Guid.Empty) return BadRequest("User exists");
        return Ok(guid);
    }
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpPut]
    public async Task<IActionResult> UpdateProfile(UpdateProfileCommand command)
    {
        try
        {
            var guid = await _mediator.Send(command);
            return Ok(guid);
        }
        catch (NotFoundException)
        {
            return NotFound("Invalid id");
        }
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProfile(Guid id)
    {
        try
        {
            var guid = await _mediator.Send(new DeleteProfileCommand
            {
                UserId = id
            });
            return Ok(guid);
        }
        catch (NotFoundException)
        {
            return NotFound("Profile not found");
        }

    }
}