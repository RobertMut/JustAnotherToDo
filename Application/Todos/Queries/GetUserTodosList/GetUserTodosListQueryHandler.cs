using AutoMapper;
using AutoMapper.QueryableExtensions;
using JustAnotherToDo.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace JustAnotherToDo.Application.Todos.Queries.GetUserTodosList;

public class GetUserTodosListQueryHandler : IRequestHandler<GetUserTodosListQuery, UserTodosListVm>
{
    private readonly IJustAnotherToDoDbContext _context;
    private readonly IMapper _mapper;

    public GetUserTodosListQueryHandler(IJustAnotherToDoDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<UserTodosListVm> Handle(GetUserTodosListQuery request, CancellationToken cancellationToken)
    {
        var todos = await _context.ToDos.ProjectTo<UserTodoDto>(_mapper.ConfigurationProvider)
            .Where(u => u.ProfileId == request.ProfileId).ToListAsync();
        var vm = new UserTodosListVm
        {
            Todos = todos,
            ProfileId = request.ProfileId
        };
        return vm;
    }
}