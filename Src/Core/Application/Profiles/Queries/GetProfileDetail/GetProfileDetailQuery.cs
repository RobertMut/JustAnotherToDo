using AutoMapper;
using JustAnotherToDo.Application.Common.Exceptions;
using JustAnotherToDo.Application.Common.Interfaces;
using JustAnotherToDo.Domain.Entities;
using MediatR;

namespace JustAnotherToDo.Application.Profiles.Queries.GetProfileDetail;

public class GetProfileDetailQuery : IRequest<ProfileDetailVm>
{
    //public Guid UserId { get; set; }
    public string Username { get; set; }
}
public class GetProfileDetailQueryHandler : IRequestHandler<GetProfileDetailQuery, ProfileDetailVm>
{
    private readonly IUserManager _manager;
    private readonly IMapper _mapper;

    public GetProfileDetailQueryHandler(IUserManager manager, IMapper mapper)
    {
        _manager = manager;
        _mapper = mapper;
    }
    public async Task<ProfileDetailVm> Handle(GetProfileDetailQuery request, CancellationToken cancellationToken)
    {
        var entity = await _manager.GetUserAsync(request.Username, cancellationToken);
        if (entity == null) throw new NotFoundException(nameof(UserProfile), request.Username);
        return _mapper.Map<ProfileDetailVm>(entity);
    }
}