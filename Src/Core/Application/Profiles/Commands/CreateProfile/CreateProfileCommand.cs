using JustAnotherToDo.Application.Common.Exceptions;
using JustAnotherToDo.Application.Common.Interfaces;
using JustAnotherToDo.Domain.Enums;
using MediatR;

namespace JustAnotherToDo.Application.Profiles.Commands.CreateProfile;

public class CreateProfileCommand : IRequest<Guid>
{
    public string Username { get; set; }
    public string Password  { get; set; }
    public class CreateProfileCommandHandler : IRequestHandler<CreateProfileCommand, Guid>
    {
        private readonly IUserManager _manager;

        public CreateProfileCommandHandler(IUserManager manager)
        {
            this._manager = manager;
        }

        public async Task<Guid> Handle(CreateProfileCommand request, CancellationToken cancellationToken)
        {
            var user = await _manager.CreateUserAsync(request.Username, request.Password, AccessLevel.User, cancellationToken);
            if (user == Guid.Empty) throw new UserExistsException(nameof(CreateProfileCommand), request.Username);
            return user;
        }
    }
}