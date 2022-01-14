using JustAnotherToDo.Domain.Enums;
using MediatR;

namespace JustAnotherToDo.Application.Profiles.Commands.UpdateProfile;

public class UpdateProfileCommand : IRequest<Guid>
{
    public Guid UserId { get; set; }
    public string Username { get; set; }
    public string? Password { get; set; }
    public AccessLevel AccessLevel { get; set; }
}