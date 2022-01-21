using MediatR;

namespace JustAnotherToDo.Application.Categories.Queries.GetUserCategoriesList;

public class GetUserCategoriesListQuery : IRequest<UserCategoriesListVm>
{
    public string Username { get; set; }
}