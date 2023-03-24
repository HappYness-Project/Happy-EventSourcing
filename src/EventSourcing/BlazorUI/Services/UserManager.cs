using BlazorUI.Data;
using HP.Shared;
using HP.Shared.Common;
using HP.Shared.Contacts;
using HP.Shared.Requests.Users;
using MediatR;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text;
using System.Net;
using System.Net.Security;
using System.Security.Authentication;

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
        public async Task<Result<User>> TrySignInAndGetUserAsync(UserLoginDto user)
        {
            return null;
        }
        public async Task<Result<string>> RequestUserCreateAsync(CreateUser user)
        {
            var result = new Result<string>();
            var response = await _httpClient.PostAsJsonAsync("register/user", user); 
            if(response.IsSuccessStatusCode)
            {
                result.IsSuccess = true;
                result.Data = await response.Content.ReadAsStringAsync();
                return result;
            }
            result.IsSuccess = false;
            result.Msg = await response.Content.ReadAsStringAsync();
            return result;
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

        public Task<Result<string>> UserUpdateAsync(UserUpdateDto user)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ChangePassword(UserChangePwdDto request)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsUserAuthenticated()
        {
            throw new NotImplementedException();
        }
    }
}
