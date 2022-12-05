using HP.Shared;
using HP.Shared.Contacts;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
namespace BlazorUI.Pages
{
    public class SignInBase : SignBase
    {
        protected string Day { get; set; } = DateTime.Now.DayOfWeek.ToString();
        [Inject] private NavigationManager NavigationManager { get; set; }
        [Inject] private IUserManager UserManager { get; set; }
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