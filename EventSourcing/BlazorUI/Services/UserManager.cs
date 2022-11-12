using HP.Shared;
using HP.Shared.Contacts;

namespace BlazorUI.Services
{
    public class UserManager : IUserManager
    {
        public HttpClient _httpClient { get; }



        public async Task<User> TrySignInAndGetUserAsync(User user)
        {
            //await Task.Delay(10000);
            Console.WriteLine("Hi from user manager.");
            return await Task.FromResult(new User());
        }
        public async Task InsertUserAsync(User user)
        {
            await Task.FromResult(true);
        }

        public Task<string> GetUserRole(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetUserByAccessTokenAsync(string accessToken)
        {
            throw new NotImplementedException();
        }

        public Task<User> RefreshTokenAsync(RefreshRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
