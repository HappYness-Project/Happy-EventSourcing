using HP.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System.Linq.Expressions;

namespace BlazorUI.Pages
{
    public class SignInBase : ComponentBase
    {
        protected string Day { get; set; } = DateTime.Now.DayOfWeek.ToString();
        protected string Username { get; set; } = "Kevin Park";
        protected User User { get; set; } = new User();
        protected EditContext EditContext { get; set; }
        protected override void OnInitialized()
        {
            base.OnInitialized();
            EditContext = new EditContext(User);
        }
        protected void HandleUserNameChanged(ChangeEventArgs eventArgs)
        {
            Username = eventArgs.Value.ToString();
        }
        protected void HandleUserNameValueChanged(string value)
        {
            Username = value;
        }
        public string GetError(Expression<Func<object>> fu)
        {
            if(EditContext == null)
            {
                return null;
            }
            return EditContext.GetValidationMessages(fu).FirstOrDefault();
        }
    }
}
