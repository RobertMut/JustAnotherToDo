using AutoMapper;
using AutoMapper.QueryableExtensions;
using JustAnotherToDo.Application.Common.Exceptions;
using JustAnotherToDo.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace JustAnotherToDo.Application.Categories.Queries.GetUserCategoriesList;

public class GetUserCategoriesListQueryHandler : IRequestHandler<GetUserCategoriesListQuery, UserCategoriesListVm>
{
    private readonly IJustAnotherToDoDbContext _context;
    private readonly IUserManager _manager;
    private readonly IMapper _mapper;

    public GetUserCategoriesListQueryHandler(IJustAnotherToDoDbContext context,IUserManager manager, IMapper mapper)
    {
        _context = context;
        _manager = manager;
        _mapper = mapper;
    }

    public async Task<UserCategoriesListVm> Handle(GetUserCategoriesListQuery request, CancellationToken cancellationToken)
    {
        var user = await _manager.GetUserAsync(request.Username);
        var categories = await _context.Categories.ProjectTo<CategoryDto>(_mapper.ConfigurationProvider)
            .Where(u => u.UserId == user.UserId).ToListAsync(cancellationToken);
        if (categories == null) throw new NotFoundException(nameof(Categories), user.UserId);
        var vm = new UserCategoriesListVm
        {
            Categories = categories

        };
        return vm;
    }
}