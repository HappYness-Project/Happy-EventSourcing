using HP.Shared;
using HP.Shared.Contacts;
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

        [Inject]
        private IUserManager UserManager { get; set; }

        protected override void OnInitialized()
        {
            base.OnInitialized();
            User = new User
            {
                FirstName = "x",
                LastName = "x",
                Email = "xxx@gmail.com"
            };

            EditContext = new EditContext(User);
        }


        protected async void OnSubmit()
        {
            if(!EditContext.Validate())
            {
                return;
            }

            var user = await UserManager.TrySignInAndGetUserAsync(User);
            if(user != null)
            {
                NavigationManager.NavigateTo("items");
            }
        }
    }
}
//https://www.c-sharpcorner.com/article/5-steps-to-implement-event-call-backs-in-blazor/