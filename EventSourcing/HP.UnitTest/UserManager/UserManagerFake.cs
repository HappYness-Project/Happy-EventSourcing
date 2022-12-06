using HP.Shared;
using HP.Shared.Contacts;
using HP.Shared.Requests.Users;
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
        public Task<User> RefreshTokenAsync(RefreshRequest request)
        {
            throw new NotImplementedException();
        }
        public Task<User> RequestUserCreateAsync(UserCreateDto user)
        {
            throw new NotImplementedException();
        }
        public Task<User> TrySignInAndGetUserAsync(UserLoginDto user)
        {
            throw new NotImplementedException();
        }
    }
}
