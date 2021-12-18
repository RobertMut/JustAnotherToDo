using JustAnotherToDo.Application.Models;

namespace JustAnotherToDo.Application.Common.Interfaces;

public interface IUserManager
{
    Task<(Result Result, string UserId)> CreateUserAsync(string userName, string password);
    Task<Result> DeleteUserAsync(string userId);
}