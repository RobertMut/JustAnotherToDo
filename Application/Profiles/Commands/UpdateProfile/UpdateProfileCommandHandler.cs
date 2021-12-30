using JustAnotherToDo.Application.Common.Exceptions;
using JustAnotherToDo.Application.Common.Interfaces;
using JustAnotherToDo.Domain.Entities;
using MediatR;

namespace JustAnotherToDo.Application.Profiles.Commands.UpdateProfile;

public class UpdateProfileCommandHandler : IRequestHandler<UpdateProfileCommand>
{
    private readonly IUserManager _manager;

    public UpdateProfileCommandHandler(IUserManager manager)
    {
        _manager = manager;
    }

    public async Task<Unit> Handle(UpdateProfileCommand request, CancellationToken cancellationToken)
    {
        var entity = await _manager.GetUserByIdAsync(request.UserId);
        if (entity == null) throw new NotFoundException(nameof(Profiles), request.UserId);
        await _manager.UpdateProfileAsync(new UserProfile
        {
            UserId = entity.UserId,
            Username = request.Name,
            Password = request.Password
        }, cancellationToken);
        return Unit.Value;
    }
}