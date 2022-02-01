using System.Security.Claims;
using IdentityServer4.Extensions;
using JustAnotherToDo.Application.Common.Interfaces;
using JustAnotherToDo.Application.Profiles.Queries.GetProfileDetail;
using MediatR;

namespace WebUI.Services;

public class CurrentUserService : ICurrentUserService
{
    private readonly IMediator _mediator;
    public CurrentUserService(IHttpContextAccessor httpContextAccessor, IMediator mediator)
    {
        _mediator = mediator;
        var username = httpContextAccessor.HttpContext.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        IsAuthenticated = httpContextAccessor.HttpContext.User.IsAuthenticated();
        if (!string.IsNullOrEmpty(username) ) InitializeAsync(username).Wait();

    }

    private async Task InitializeAsync(string username)
    {
        var user = await _mediator.Send(new GetProfileDetailQuery
        {
            Username = username
        });
        User = user;
    }
    public ProfileDetailVm User { get; private set; }
    public bool IsAuthenticated { get; }
}
