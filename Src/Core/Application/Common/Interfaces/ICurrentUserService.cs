using JustAnotherToDo.Application.Profiles.Queries.GetProfileDetail;

namespace JustAnotherToDo.Application.Common.Interfaces;

public interface ICurrentUserService
{
    ProfileDetailVm User { get; }
    public bool IsAuthenticated { get; }
}