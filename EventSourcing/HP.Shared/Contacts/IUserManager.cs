using HP.Shared.Common;
using HP.Shared.Requests.Users;

namespace HP.Shared.Contacts
{
    public interface IUserManager
    {
        Task<Result<User>> TrySignInAndGetUserAsync(UserLoginDto user);
        Task<Result<string>> RequestUserCreateAsync(UserCreateDto user);
        Task<Result<string>> UserUpdateAsync(UserUpdateDto user);
        Task<string> GetUserRole(string userId);
        Task<User> GetUserByAccessTokenAsync(string accessToken);
        Task<User> RefreshTokenAsync(RefreshRequest request);
        Task<bool> ChangePassword(UserChangePwdDto request);
        Task<bool> IsUserAuthenticated();
    }
    public class RefreshRequest
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
