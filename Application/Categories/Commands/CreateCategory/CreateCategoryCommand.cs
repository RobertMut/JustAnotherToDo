using MediatR;

namespace JustAnotherToDo.Application.Categories.Commands.CreateCategory;

public class CreateCategoryCommand : IRequest<Guid>
{
    public string Name { get; set; }
    public string Color { get; set; }
    public Guid ProfileId  { get; set; }
}