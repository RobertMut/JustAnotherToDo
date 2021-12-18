using FluentValidation;

namespace JustAnotherToDo.Application.Profiles.Queries.GetProfileDetail;

public class GetProfileDetailQueryValidator : AbstractValidator<GetProfileDetailQuery>
{
    public GetProfileDetailQueryValidator()
    {
        RuleFor(v => v.Id).NotEmpty();
    }
}