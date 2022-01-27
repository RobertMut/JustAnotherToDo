using JustAnotherToDo.Application.Common.Exceptions;
using JustAnotherToDo.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace JustAnotherToDo.Application.Categories.Commands.UpdateCategory;

public class UpdateCategoryCommand : IRequest
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Color { get; set; }
    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand>
    {
        private readonly IJustAnotherToDoDbContext _context;

        public UpdateCategoryCommandHandler(IJustAnotherToDoDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Categories.SingleOrDefaultAsync(c => c.Id == request.Id, cancellationToken);
            if (entity == null) throw new NotFoundException(nameof(Categories), request.Id);
            entity.Id = request.Id;
            entity.Name = request.Name;
            entity.Color = request.Color;
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}