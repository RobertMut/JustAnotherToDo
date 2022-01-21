using IdentityServer4.Models;

namespace JustAnotherToDo.Application.Models;

public class ErrorModel
{
    public ErrorModel()
    {
    }

    public ErrorModel(string error)
    {
        Error = new ErrorMessage { Error = error };
    }

    public ErrorMessage Error { get; set; }
}