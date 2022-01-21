namespace JustAnotherToDo.Application.Models;

public class LoginModel : LoginInputModel
{
    public bool AllowRememberLogin { get; set; } = true;
    public bool EnableLocalLogin { get; set; } = true;

}