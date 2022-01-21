using MediatR;

namespace JustAnotherToDo.Application.Common.Wrappers;

public class ContextualRequest<TRequest, TResponse> : IRequest<TResponse>
    where TRequest : IRequest<TResponse>
{
    public TRequest Data { get; }
    public string Username { get; }

    public ContextualRequest(TRequest data, string username)
    {
        Data = data;
        Username = username;
    }
}