using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DemoClient.Pages
{
    public class LoginModel : PageModel
    {
        public IActionResult OnGet(string redirectUri)
        {
            //await HttpContext.ChallengeAsync("oidc", new AuthenticationProperties { RedirectUri = redirectUri });

            //return Challenge(new AuthenticationProperties { RedirectUri = redirectUri }, "oidc");
            return Challenge(new AuthenticationProperties
            {
                RedirectUri = !string.IsNullOrEmpty(redirectUri) ? redirectUri : "/"
            },"OpenIdConnect");
        }
    }
}
