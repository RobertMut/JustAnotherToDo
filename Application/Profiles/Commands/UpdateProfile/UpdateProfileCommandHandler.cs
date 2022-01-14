using JustAnotherToDo.Application.Common.Exceptions;
using JustAnotherToDo.Application.Common.Interfaces;
using JustAnotherToDo.Domain.Entities;
using MediatR;

namespace JustAnotherToDo.Application.Profiles.Commands.UpdateProfile;

public class UpdateProfileCommandHandler : IRequestHandler<UpdateProfileCommand, Guid>
{
    private readonly IUserManager _manager;

    public UpdateProfileCommandHandler(IUserManager manager)
    {
        _manager = manager;
    }

    public async Task<Guid> Handle(UpdateProfileCommand request, CancellationToken cancellationToken)
    {
        var entity = await _manager.GetUserByIdAsync(request.UserId);
        if (entity == null) throw new NotFoundException(nameof(Profiles), request.UserId);
        var guid = await _manager.UpdateProfileAsync(new UserProfile
        {
            UserId = entity.UserId,
            Username = request.Username,
            Password = request.Password,
            AccessLevel = request.AccessLevel
        }, cancellationToken);
        return guid;
    }
}