using JustAnotherToDo.Application.Common.Interfaces;
using JustAnotherToDo.Application.Profiles.Commands.CreateProfile;
using JustAnotherToDo.Application.Profiles.Commands.DeleteProfile;
using JustAnotherToDo.Application.Profiles.Commands.UpdateProfile;
using JustAnotherToDo.Application.Profiles.Queries.GetProfileDetail;
using JustAnotherToDo.Application.Profiles.Queries.GetProfilesList;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers;
[ApiController]
[Authorize]
[Route("[controller]")]
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
    public async Task<ActionResult<ProfilesListVm>> GetProfileList()
    {
        var user = await _mediator.Send(new GetProfileDetailQuery
        {
            Username = _currentUser.UserName
        });
        if (user == null) return BadRequest("User does not exist");
        var profiles = await _mediator.Send(new GetProfilesListQuery()
        );
        return profiles;
    }

    [HttpPost]
    public async Task<IActionResult> PostProfile(CreateProfileCommand command)
    {
        var guid = await _mediator.Send(command);
        return Ok(guid);
    }
    [HttpPut]
    public async Task<IActionResult> UpdateProfile(UpdateProfileCommand command)
    {
        var guid = await _mediator.Send(command);
        return Ok(guid);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteProfile(DeleteProfileCommand command)
    {
        var guid = await _mediator.Send(command);
        return Ok(guid);
    }
}