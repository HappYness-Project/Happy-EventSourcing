using HP.Shared;
using Microsoft.AspNetCore.Components;

namespace BlazorUI.Pages
{
    public class SignInBase : ComponentBase
    {
        protected string Day { get; set; } = DateTime.Now.DayOfWeek.ToString();
        protected string Username { get; set; } = "Kevin Park";
        protected User User { get; set; } = new User();

        protected void HandleUserNameChanged(ChangeEventArgs eventArgs)
        {
            Username = eventArgs.Value.ToString();
        }
        protected void HandleUserNameValueChanged(string value)
        {
            Username = value;
        }
    }
}
