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
        public Task<User> GetUserByAccessTokenAsync(string accessToken)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetUserRole(string userId)
        {
            throw new NotImplementedException();
        }

        //Use case: Use this method when the user press the button for the creating user.
        public Task InsertUserAsync(User user)
        {
            // TODO Create new user from here.
            return Task.FromResult(true);
        }

        public Task<User> RefreshTokenAsync(RefreshRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<User> TrySignInAndGetUserAsync(User user)
        {
            Console.WriteLine("Trying from the UserManasgerFake!");
            return Task.FromResult(new User());
        }
    }
}
