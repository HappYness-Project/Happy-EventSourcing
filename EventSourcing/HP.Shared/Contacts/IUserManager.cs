using HP.Shared.Requests.Users;

namespace HP.Shared.Contacts
{
    public interface IUserManager
    {
        Task<User> TrySignInAndGetUserAsync(UserLoginDto user);
        Task<User> RequestUserCreateAsync(UserCreateDto user);
        Task<string> GetUserRole(string userId);
        Task<User> GetUserByAccessTokenAsync(string accessToken);
        Task<User> RefreshTokenAsync(RefreshRequest request);
    }
    public class RefreshRequest
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
