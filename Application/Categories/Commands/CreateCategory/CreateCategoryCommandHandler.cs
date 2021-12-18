using JustAnotherToDo.Application.Common.Interfaces;
using JustAnotherToDo.Domain.Entities;
using MediatR;

namespace JustAnotherToDo.Application.Categories.Commands.CreateCategory;

public class CreateProfileCommandHandler : IRequestHandler<CreateProfileCommand, Guid>
{
    private readonly IJustAnotherToDoDbContext _context;

    public CreateProfileCommandHandler(IJustAnotherToDoDbContext _context)
    {
        this._context = _context;
    }

    public async Task<Guid> Handle(CreateProfileCommand request, CancellationToken cancellationToken)
    {
        var entity = new Category
        {
            Name = request.Name,
            Color = request.Color,
            UserId = request.UserId
        };
        _context.Categories.Add(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return entity.Id;
    }
}