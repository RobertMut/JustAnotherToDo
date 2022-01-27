using AutoMapper;
using JustAnotherToDo.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace JustAnotherToDo.Application.Todos.Queries.GetUserTodosList;

public class GetUserTodosListQuery : IRequest<UserTodosListVm>
{
    public Guid UserId { get; set; }

}
public class GetUserTodosListQueryHandler : IRequestHandler<GetUserTodosListQuery, UserTodosListVm>
{
    private readonly IJustAnotherToDoDbContext _context;


    public GetUserTodosListQueryHandler(IJustAnotherToDoDbContext context)
    {
        _context = context;

    }

    public async Task<UserTodosListVm> Handle(GetUserTodosListQuery request, CancellationToken cancellationToken)
    {

        var join = await (from t in _context.ToDos
            where t.ProfileId == request.UserId
                          select new UserTodoDto
            {
                Id = t.Id,
                Name = t.Name,
                CreationDate = t.CreationDate,
                EndDate = t.EndDate,
                CategoryId = t.CategoryId,
                Category = t.Category.Name,
                Color = t.Category.Color,
                ProfileId = t.ProfileId
            }).ToListAsync(cancellationToken);
        var vm = new UserTodosListVm
        {
            Todos = join
        };
        return vm;
    }
}