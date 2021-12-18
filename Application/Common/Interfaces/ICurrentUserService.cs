namespace JustAnotherToDo.Application.Common.Interfaces;

public interface ICurrentUserService
{
    string UserId { get; set; }
    bool IsAuthenticated { get; set; }
}