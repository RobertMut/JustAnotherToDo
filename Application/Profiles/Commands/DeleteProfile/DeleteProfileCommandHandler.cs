using JustAnotherToDo.Application.Common.Exceptions;
using JustAnotherToDo.Application.Common.Interfaces;
using JustAnotherToDo.Application.Models;
using JustAnotherToDo.Domain.Entities;
using MediatR;

namespace JustAnotherToDo.Application.Profiles.Commands.DeleteProfile;

public class DeleteProfileCommandHandler : IRequestHandler<DeleteProfileCommand>
{
    private readonly IUserManager _manager;

    public DeleteProfileCommandHandler(IUserManager manager)
    {
        _manager = manager;
    }

    public async Task<Unit> Handle(DeleteProfileCommand request, CancellationToken cancellationToken)
    {
        var deleted = await _manager.DeleteUserAsync(request.Id);
        return Unit.Value;
    }
}