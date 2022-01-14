using MediatR;

namespace JustAnotherToDo.Application.Profiles.Commands.DeleteProfile;

public class DeleteProfileCommand : IRequest
{
    public Guid UserId { get; set; }
}