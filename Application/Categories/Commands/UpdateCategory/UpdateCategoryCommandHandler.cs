using JustAnotherToDo.Application.Common.Exceptions;
using JustAnotherToDo.Application.Common.Interfaces;
using MediatR;

namespace JustAnotherToDo.Application.Categories.Commands.UpdateCategory;

public class UpdateProfileCommandHandler : IRequestHandler<UpdateProfileCommand>
{
    private readonly IJustAnotherToDoDbContext _context;

    public UpdateProfileCommandHandler(IJustAnotherToDoDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(UpdateProfileCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Categories.FindAsync(request.Id);
        if (entity == null) throw new NotFoundException(nameof(Categories), request.Id);
        entity.Id = request.Id;
        entity.Name = request.Name;
        entity.Color = request.Color;
        await _context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}