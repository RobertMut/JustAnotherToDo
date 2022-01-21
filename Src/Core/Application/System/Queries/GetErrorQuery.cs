using MediatR;

namespace JustAnotherToDo.Application.System.Queries
{
    public class GetErrorQuery : IRequest<ErrorVm>
    {
        public string ErrorId { get; set; }
    }
}
