using IdentityServer4.Models;

namespace JustAnotherToDo.Application.System.Queries
{
    public class ErrorVm
    {
        public ErrorVm()
        {
        }

        public ErrorVm(string error)
        {
            Error = new ErrorMessage { Error = error };
        }

        public ErrorMessage Error { get; set; }
    }
}
