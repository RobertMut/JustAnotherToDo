using MediatR;

namespace JustAnotherToDo.Application.Profiles.Commands.UpdateProfile;

public class UpdateProfileCommand : IRequest<Guid>, IRequest<Unit>
{
    public Guid UserId { get; set; }
    public string Name { get; set; }
    public string Password { get; set; }
}