using MediatR;

namespace JustAnotherToDo.Application.Profiles.Commands.CreateProfile;

public class CreateProfileCommand : IRequest<Guid>
{
    public string Username { get; set; }
    public string Password  { get; set; }
}