namespace JustAnotherToDo.Application.Common.Interfaces;

public interface ICurrentUserService
{
    string UserName { get; }
    public bool IsAuthenticated { get; }
}