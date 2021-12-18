using MediatR;

namespace JustAnotherToDo.Application.Categories.Commands.DeleteCategory;

public class DeleteTodoCommand : IRequest
{
    public Guid Id { get; set; }
}