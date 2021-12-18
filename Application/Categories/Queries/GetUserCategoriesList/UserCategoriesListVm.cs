namespace JustAnotherToDo.Application.Categories.Queries.GetUserCategoriesList;

public class UserCategoriesListVm 
{
    public IList<CategoryDto> Categories { get; set; }
    public Guid ProfileId { get; set; }

}