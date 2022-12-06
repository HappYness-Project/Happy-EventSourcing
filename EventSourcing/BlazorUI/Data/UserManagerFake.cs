using HP.Shared;
using HP.Shared.Common;
using HP.Shared.Contacts;
using HP.Shared.Requests.Users;

namespace BlazorUI.Data
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

        public Task<Result<string>> RequestUserCreateAsync(UserCreateDto user)
        {
            throw new NotImplementedException();
        }

        public Task<Result<User>> TrySignInAndGetUserAsync(UserLoginDto user)
        {
            throw new NotImplementedException();
        }
    }
}
