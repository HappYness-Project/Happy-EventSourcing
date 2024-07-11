using HP.Shared;
using HP.Shared.Contacts;

namespace BlazorUI.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        public User CurrentUser { get; set; }

    }
}
