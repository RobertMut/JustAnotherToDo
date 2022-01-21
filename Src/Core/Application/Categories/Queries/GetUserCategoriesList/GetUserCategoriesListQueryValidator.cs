using FluentValidation;

namespace JustAnotherToDo.Application.Categories.Queries.GetUserCategoriesList;

public class GetUserCategoriesListQueryValidator : AbstractValidator<GetUserCategoriesListQuery>
{
    public GetUserCategoriesListQueryValidator()
    {
        RuleFor(i => i.Username).NotEmpty();
    }
}