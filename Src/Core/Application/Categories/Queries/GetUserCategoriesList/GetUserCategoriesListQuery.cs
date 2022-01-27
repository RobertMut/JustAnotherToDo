using AutoMapper;
using AutoMapper.QueryableExtensions;
using JustAnotherToDo.Application.Common.Exceptions;
using JustAnotherToDo.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace JustAnotherToDo.Application.Categories.Queries.GetUserCategoriesList;

public class GetUserCategoriesListQuery : IRequest<UserCategoriesListVm>
{
    public Guid UserId { get; set; }
}
public class GetUserCategoriesListQueryHandler : IRequestHandler<GetUserCategoriesListQuery, UserCategoriesListVm>
{
    private readonly IJustAnotherToDoDbContext _context;

    private readonly IMapper _mapper;

    public GetUserCategoriesListQueryHandler(IJustAnotherToDoDbContext context, IMapper mapper)
    {
        _context = context;

        _mapper = mapper;
    }

    public async Task<UserCategoriesListVm> Handle(GetUserCategoriesListQuery request, CancellationToken cancellationToken)
    {

        var categories = await _context.Categories.ProjectTo<CategoryDto>(_mapper.ConfigurationProvider)
            .Where(u => u.UserId == request.UserId).ToListAsync(cancellationToken);
        if (categories == null) throw new NotFoundException(nameof(Categories), request.UserId);
        var vm = new UserCategoriesListVm
        {
            Categories = categories

        };
        return vm;
    }
}