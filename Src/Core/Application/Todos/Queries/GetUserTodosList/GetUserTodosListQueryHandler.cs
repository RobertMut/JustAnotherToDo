using AutoMapper;
using JustAnotherToDo.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace JustAnotherToDo.Application.Todos.Queries.GetUserTodosList;

public class GetUserTodosListQueryHandler : IRequestHandler<GetUserTodosListQuery, UserTodosListVm>
{
    private readonly IJustAnotherToDoDbContext _context;
    private readonly IUserManager _manager;
    private readonly IMapper _mapper;

    public GetUserTodosListQueryHandler(IJustAnotherToDoDbContext context, IUserManager manager, IMapper mapper)
    {
        _context = context;
        _manager = manager;
        _mapper = mapper;
    }

    public async Task<UserTodosListVm> Handle(GetUserTodosListQuery request, CancellationToken cancellationToken)
    {
        var user = await _manager.GetUserAsync(request.Username);
        var join = await (from t in _context.ToDos
                          where t.ProfileId == user.UserId
                          select new UserTodoDto
                          {
                              Id = t.Id,
                              Name = t.Name,
                              CreationDate = t.CreationDate,
                              EndDate = t.EndDate,
                              Category = t.Category.Name,
                              Color = t.Category.Color,
                              ProfileId = t.ProfileId
                          }).ToListAsync();
        var vm = new UserTodosListVm
        {
            Todos = join
        };
        return vm;
    }
}