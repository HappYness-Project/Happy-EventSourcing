using HP.Shared;
using HP.Shared.Common;
using HP.Shared.Contacts;
using HP.Shared.Requests.Users;

namespace BlazorUI.Data
{
    public class UserManagerFake : IUserManager
    {
        public Task<bool> ChangePassword(UserChangePwdDto request)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetUserByAccessTokenAsync(string accessToken)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetUserRole(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsUserAuthenticated()
        {
            throw new NotImplementedException();
        }

        public Task<User> RefreshTokenAsync(RefreshRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<Result<string>> RequestUserCreateAsync(CreateUserDto user)
        {
            throw new NotImplementedException();
        }

        public Task<Result<string>> TrySignInAndCheckStatus(UserLoginDto user)
        {
            throw new NotImplementedException();
        }

        public Task<Result<string>> UserUpdateAsync(UserUpdateDto user)
        {
            throw new NotImplementedException();
        }
    }
}
