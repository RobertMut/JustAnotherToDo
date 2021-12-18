using MediatR;

namespace JustAnotherToDo.Application.Categories.Commands.CreateCategory;

public class CreateProfileCommand : IRequest<Guid>
{
    public string Name { get; set; }
    public string Color { get; set; }
    public Guid UserId  { get; set; }
}