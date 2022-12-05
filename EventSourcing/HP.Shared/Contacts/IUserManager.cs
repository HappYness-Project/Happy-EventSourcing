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
        Task<User> RequestUserCreateAsync(User user);
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
