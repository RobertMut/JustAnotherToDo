using MediatR;

namespace JustAnotherToDo.Application.Profiles.Commands.UpdateProfile;

public class UpdateTodoCommand : IRequest<Guid>, IRequest<Unit>
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Password { get; set; }
}