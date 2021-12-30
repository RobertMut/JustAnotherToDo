using JustAnotherToDo.Application.Common.Interfaces;
using JustAnotherToDo.Domain.Entities;
using MediatR;

namespace JustAnotherToDo.Application.Profiles.Commands.CreateProfile;

public class CreateProfileCommandHandler : IRequestHandler<CreateProfileCommand, Guid>
{
    private readonly IUserManager _manager;

    public CreateProfileCommandHandler(IUserManager _manager)
    {
        this._manager = _manager;
    }

    public async Task<Guid> Handle(CreateProfileCommand request, CancellationToken cancellationToken)
    {
        var user = await _manager.CreateUserAsync(request.Username, request.Password, cancellationToken);
        return user;
    }
}