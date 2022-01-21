using JustAnotherToDo.Application.Common.Interfaces;
using JustAnotherToDo.Domain.Entities;
using MediatR;

namespace JustAnotherToDo.Application.Categories.Commands.CreateCategory;

public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, Guid>
{
    private readonly IJustAnotherToDoDbContext _context;

    public CreateCategoryCommandHandler(IJustAnotherToDoDbContext _context)
    {
        this._context = _context;
    }

    public async Task<Guid> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var entity = new Category
        {
            Name = request.Name,
            Color = request.Color,
            ProfileId = request.ProfileId
        };
        _context.Categories.Add(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return entity.Id;
    }
}