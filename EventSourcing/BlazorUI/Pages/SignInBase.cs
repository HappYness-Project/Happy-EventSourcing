using HP.Shared;
using HP.UserBusiness;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System.Linq.Expressions;

namespace BlazorUI.Pages
{
    public class SignInBase : SignBase
    {
        protected string Day { get; set; } = DateTime.Now.DayOfWeek.ToString();

        [Inject]
        private NavigationManager NavigationManager { get; set; }

        protected async void OnSubmit()
        {
            if(!EditContext.Validate())
            {
                return;
            }

            var userManager = new UserManager();
            var user = await userManager.TrySignInAndGetUserAsync(User);
            if(user != null)
            {
                NavigationManager.NavigateTo("items");
            }
        }
    }
}
//https://www.c-sharpcorner.com/article/5-steps-to-implement-event-call-backs-in-blazor/