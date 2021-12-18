using AutoMapper;
using AutoMapper.QueryableExtensions;
using JustAnotherToDo.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace JustAnotherToDo.Application.Categories.Queries.GetCategoriesList;

public class GetUserCategoriesListQueryHandler : IRequestHandler<GetCategoriesListQuery, CategoriesListVm>
{
    private readonly IJustAnotherToDoDbContext _context;
    private readonly IMapper _mapper;

    public GetUserCategoriesListQueryHandler(IJustAnotherToDoDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<CategoriesListVm> Handle(GetCategoriesListQuery request, CancellationToken cancellationToken)
    {
        var categories = await _context.Categories.ProjectTo<CategoryDto>(_mapper.ConfigurationProvider).ToListAsync();
        var vm = new CategoriesListVm
        {
            Categories = categories,
            Count = categories.Count()
        };
        return vm;
    }
}