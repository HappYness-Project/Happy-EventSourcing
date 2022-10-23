using HP.Shared.Contacts;
using Microsoft.AspNetCore.Components;

namespace BlazorUI.Shared
{
    public partial class MainLayout : LayoutComponentBase
    {
        [Inject]
        private ICurrentUserService CurrentUserService { get; set; }

        protected void SignOut()
        {
        }

    }
}
