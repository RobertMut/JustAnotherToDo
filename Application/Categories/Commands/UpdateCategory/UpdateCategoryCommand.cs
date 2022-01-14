using MediatR;

namespace JustAnotherToDo.Application.Categories.Commands.UpdateCategory;

public class UpdateCategoryCommand : IRequest
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Color { get; set; }
}