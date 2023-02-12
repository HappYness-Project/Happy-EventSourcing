using HP.Core.Helpers;
using HP.Shared;
using HP.Shared.Common;
using HP.Shared.Contacts;
using HP.Shared.Requests.Users;

namespace BlazorUI.Data
{
    public class UserManagerFake : IUserManager
    {
        // Assuming that we received the token info.
        public async Task<User> GetUserByAccessTokenAsync(string accessToken)
        {
            var infoDic = AuthHelper.GetTokenInfo(accessToken);
            return new User() { Id = 1, UserName = "hyunbin7303", FirstName = "Kevin", LastName = "Park", Email = "hyunbin7303@gmail.com" };
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
        public Task<Result<string>> UserUpdateAsync(UserUpdateDto user)
        {
            throw new NotImplementedException();
        }
    }
}
