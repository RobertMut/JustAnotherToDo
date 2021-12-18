using AutoMapper;
using JustAnotherToDo.Application.Common.Exceptions;
using JustAnotherToDo.Application.Common.Interfaces;
using JustAnotherToDo.Domain.Entities;
using MediatR;

namespace JustAnotherToDo.Application.Profiles.Queries.GetProfileDetail;

public class GetProfileDetailQueryHandler : IRequestHandler<GetProfileDetailQuery, ProfileDetailVm>
{
    private readonly IJustAnotherToDoDbContext _context;
    private readonly IMapper _mapper;

    public GetProfileDetailQueryHandler(IJustAnotherToDoDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<ProfileDetailVm> Handle(GetProfileDetailQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.Profiles.FindAsync(request.Id);
        if (entity == null) throw new NotFoundException(nameof(UserProfile), request.Id);
        return _mapper.Map<ProfileDetailVm>(entity);
    }
}