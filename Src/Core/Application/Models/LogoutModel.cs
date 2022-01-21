namespace JustAnotherToDo.Application.Models;

public class LogoutModel : LogoutInputModel
{
    public bool ShowLogoutPrompt { get; set; } = true;
}