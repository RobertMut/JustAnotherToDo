namespace JustAnotherToDo.Application.Models.Auth;

public class AuthenticationOptions
{
    public TimeSpan TokenLifetime { get; set; } = TimeSpan.FromDays(1);

    public bool AllowRefresh { get; set; } = true;
}