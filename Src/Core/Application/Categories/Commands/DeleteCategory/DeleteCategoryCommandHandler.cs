using JustAnotherToDo.Application.Common.Exceptions;
using JustAnotherToDo.Application.Common.Interfaces;
using JustAnotherToDo.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace JustAnotherToDo.Application.Categories.Commands.DeleteCategory;

public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand>
{
    private readonly IJustAnotherToDoDbContext _context;

    public DeleteCategoryCommandHandler(IJustAnotherToDoDbContext _context)
    {
        this._context = _context;
    }

    public async Task<Unit> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Categories.SingleOrDefaultAsync(c => c.Id == request.Id);
        if (entity == null) throw new NotFoundException(nameof(Category), request.Id);
        await _context.ToDos.Where(t => t.CategoryId == request.Id).ForEachAsync(t => t.CategoryId = null);
        _context.Categories.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}