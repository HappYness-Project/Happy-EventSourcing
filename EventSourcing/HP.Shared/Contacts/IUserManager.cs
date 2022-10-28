using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HP.Shared.Contacts
{
    public interface IUserManager
    {
        Task<User> TrySignInAndGetUserAsync(User user);
        Task InsertUserAsync(User user);
        Task<string> GetUserRole(string userId);
    }
}
