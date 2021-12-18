using System.ComponentModel.DataAnnotations;
using IdentityServer4.Models;
using JustAnotherToDo.Infrastructure.Identity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebUI.Areas.Identity.Account;

public class LoginModel : PageModel
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public LoginModel(UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }
    [BindProperty]
    public InputModel Input { get; set; }
    public IList<AuthenticationScheme> ExternalLogins { get; set; }
    public string ReturnUrl { get; set; }
    [TempData]
    public string Error { get; set; }

    public class InputModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }

    public async Task OnGetAsync(string returnUrl = null)
    {
        if(!string.IsNullOrEmpty(Error))
            ModelState.AddModelError(string.Empty, Error);
        returnUrl ??= Url.Content("~/");
        await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);
        ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        ReturnUrl = returnUrl;
    }

    public async Task<IActionResult> OnPostAsync(string returnUrl = null)
    {
        returnUrl ??= Url.Content("~/");
        if (ModelState.IsValid)
        {
            var result = await _signInManager.PasswordSignInAsync(Input.Email, Input.Password, Input.RememberMe,
                lockoutOnFailure: false);
            if (result.Succeeded) return LocalRedirect(returnUrl);
            if (result.RequiresTwoFactor)
                return RedirectToPage("./LoginWith2fa", new
                {
                    ReturnUrl = returnUrl,
                    RememberMe = Input.RememberMe
                });
            if (result.IsLockedOut) RedirectToPage("./Lockout");
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                
            }
        }
        return Page();
    }
}