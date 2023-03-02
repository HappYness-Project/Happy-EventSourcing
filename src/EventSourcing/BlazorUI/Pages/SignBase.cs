using HP.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System.Linq.Expressions;

namespace BlazorUI.Pages
{
    public class SignBase : ComponentBase
    {
        protected User User { get; set;} = new();
        protected EditContext EditContext { get; set; }
        protected override void OnInitialized()
        {
            base.OnInitialized();
            EditContext = new EditContext(User);
        }
        public string GetError(Expression<Func<object>> fu)
        {
            if (EditContext == null)
                return null;
            return EditContext.GetValidationMessages(fu).FirstOrDefault();
        }
    }
}
