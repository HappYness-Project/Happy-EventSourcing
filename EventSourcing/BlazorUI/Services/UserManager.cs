using BlazorUI.Data;
using HP.Shared;
using HP.Shared.Contacts;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;

namespace BlazorUI.Services
{
    public class UserManager : IUserManager
    {
        public HttpClient _httpClient { get; }
        private AppSettings _appSettings { get; }
        public UserManager(HttpClient httpClient, IOptions<AppSettings> appSettings)
        {
            _appSettings            = appSettings.Value;
            httpClient.BaseAddress  = new Uri(_appSettings.IdentityApiBaseAddress);
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _httpClient             = httpClient;
        }

        public async Task<User> TrySignInAndGetUserAsync(User user)
        {
            //await Task.Delay(10000);
            Console.WriteLine("Hi from user manager.");
            return await Task.FromResult(new User());
        }
        public async Task InsertUserAsync(User user)
        {
            // Todo : Api call to the Identity service.
            var newUser = new User()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Password = user.Password,
            };
            var token = await _httpClient.PostAsJsonAsync("localhost/user/create", newUser);
            // get token if it's valid. 

            if(token != null) {
                // API call to the controller.
               _httpClient.PostAsJsonAsync("localhost/user/create", newUser);

            }

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
