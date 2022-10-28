using HP.Shared;
using HP.Shared.Contacts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HP.UnitTest.UserManager
{
    public class UserManagerFake : IUserManager
    {
        public Task<string> GetUserRole(string userId)
        {
            throw new NotImplementedException();
        }

        public Task InsertUserAsync(User user)
        {
            return Task.FromResult(true);
        }

        public Task<User> TrySignInAndGetUserAsync(User user)
        {
            Console.WriteLine("Trying from the UserManasgerFake!");
            return Task.FromResult(new User());
        }
    }
}
