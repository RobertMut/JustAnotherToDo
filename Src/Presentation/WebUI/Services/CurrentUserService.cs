using System.Security.Claims;
using IdentityServer4.Extensions;
using JustAnotherToDo.Application.Common.Interfaces;

namespace WebUI.Services;

public class CurrentUserService : ICurrentUserService
{
    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        UserName = httpContextAccessor.HttpContext.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        IsAuthenticated = httpContextAccessor.HttpContext.User.IsAuthenticated();
    }

    public string UserName { get; }
    public bool IsAuthenticated { get; }
}