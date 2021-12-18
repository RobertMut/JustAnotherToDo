using MediatR;

namespace JustAnotherToDo.Application.Categories.Queries.GetUserCategoriesList;

public class GetUserCategoriesListQuery : IRequest<UserCategoriesListVm>
{
    public Guid ProfileId { get; set; }
}