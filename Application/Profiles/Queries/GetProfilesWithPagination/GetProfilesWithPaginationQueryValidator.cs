using FluentValidation;

namespace JustAnotherToDo.Application.Profiles.Queries.GetProfilesWithPagination;

public class GetProfilesWithPaginationQueryValidator : AbstractValidator<GetProfilesWithPaginationQuery>
{
    public GetProfilesWithPaginationQueryValidator()
    {
        RuleFor(x => x.PageNumber)
            .NotEmpty().GreaterThanOrEqualTo(1).WithMessage("PageNumber should be greater or equal to 1.");
        RuleFor(x => x.PageSize)
            .NotEmpty().GreaterThanOrEqualTo(1).WithMessage("PageSize should be greater or equal to 1");
    }
}