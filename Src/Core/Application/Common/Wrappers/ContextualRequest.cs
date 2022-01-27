using MediatR;

namespace JustAnotherToDo.Application.Common.Wrappers;

public class ContextualRequest<TRequest, TResponse> : IRequest<TResponse>
    where TRequest : IRequest<TResponse>
{
    public TRequest Data { get; }
    public Guid UserId { get; }

    public ContextualRequest(TRequest data, Guid userId)
    {
        Data = data;
        UserId = userId;
    }
}