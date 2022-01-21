using MediatR;

namespace JustAnotherToDo.Application.Profiles.Queries.GetProfileDetail;

public class GetProfileDetailQuery : IRequest<ProfileDetailVm>
{
    //public Guid Id { get; set; }
    public string Username { get; set; }
}