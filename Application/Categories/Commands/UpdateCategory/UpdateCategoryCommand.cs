using MediatR;

namespace JustAnotherToDo.Application.Categories.Commands.UpdateCategory;

public class UpdateProfileCommand : IRequest<Guid>, IRequest<Unit>
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Color { get; set; }
}