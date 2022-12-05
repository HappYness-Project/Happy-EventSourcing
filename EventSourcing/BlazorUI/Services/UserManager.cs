using BlazorUI.Data;
using HP.Shared;
using HP.Shared.Contacts;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;

namespace BlazorUI.Services
{
    public class UserManager : IUserManager
    {
        public HttpClient _httpClient { get; }
        private AppSettings _appSettings { get; }
        private AuthenticationStateProvider _authenticationStateProvider { get;  }
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
            await _httpClient.GetFromJsonAsync<User>("");
            return await Task.FromResult(new User());
        }
        public async Task<User> RequestUserCreateAsync(User user)
        {
            var newUser = new User()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Password = user.Password,
            };
            var response = await _httpClient.PostAsJsonAsync("/user/create", newUser); // Get the user information with the token
            if(response.IsSuccessStatusCode)
            {
                return await Task.FromResult(newUser);
            }
            return null;
        }
        public Task<string> GetUserRole(string userId)
        {
            throw new NotImplementedException();
        }

        public async Task<User> GetUserByAccessTokenAsync(string accessToken)
        {
            var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;
            throw new NotImplementedException();
        }

        public Task<User> RefreshTokenAsync(RefreshRequest request)
        {
            throw new NotImplementedException();
        }


    }
}
